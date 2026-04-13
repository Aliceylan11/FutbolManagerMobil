using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace FutbolManagerMobil
{
    // =====================================================
    //  LİG SATIRI — Her takımın puan durumu
    // =====================================================
    public class LigSatiri
    {
        public string TakimAdi { get; set; } = "";
        public int Oynadigi { get; set; } = 0;
        public int Galibiyet { get; set; } = 0;
        public int Beraberlik { get; set; } = 0;
        public int Maglubiyet { get; set; } = 0;
        public int AttigiGol { get; set; } = 0;
        public int YedigiGol { get; set; } = 0;

        // Hesaplanan alanlar
        public int AvantajFarki => AttigiGol - YedigiGol;
        public int Puan => Galibiyet * 3 + Beraberlik * 1;
    }

    // =====================================================
    //  LİG MANAGERİ — Kalıcı hafıza ile puan durumu
    // =====================================================
    public static class LigManager
    {
        private const string TERCIH_ANAHTARI = "lig_tablosu_v1";

        // -----------------------------------------------
        //  YÜKLEME
        // -----------------------------------------------
        public static List<LigSatiri> TabloYukle()
        {
            try
            {
                string json = Preferences.Get(TERCIH_ANAHTARI, "");
                if (string.IsNullOrWhiteSpace(json))
                    return new List<LigSatiri>();

                return JsonSerializer.Deserialize<List<LigSatiri>>(json)
                       ?? new List<LigSatiri>();
            }
            catch
            {
                return new List<LigSatiri>();
            }
        }

        // -----------------------------------------------
        //  KAYDETME
        // -----------------------------------------------
        private static void TabloKaydet(List<LigSatiri> tablo)
        {
            try
            {
                string json = JsonSerializer.Serialize(tablo);
                Preferences.Set(TERCIH_ANAHTARI, json);
            }
            catch { /* Kayıt hatası sessizce geçilir */ }
        }

        // -----------------------------------------------
        //  MAÇ SONUCU İŞLE
        // -----------------------------------------------
        /// <summary>
        /// Lig maçı bitince bu metodu çağır.
        /// Hem ev sahibi hem deplasman istatistikleri güncellenir.
        /// </summary>
        public static void MacSonucunuIsle(
            string evTakim, int evGol,
            string depTakim, int depGol)
        {
            var tablo = TabloYukle();

            var ev = SatiriBul(tablo, evTakim);
            var dep = SatiriBul(tablo, depTakim);

            // Ortak istatistikler
            ev.Oynadigi++;
            dep.Oynadigi++;
            ev.AttigiGol += evGol;
            ev.YedigiGol += depGol;
            dep.AttigiGol += depGol;
            dep.YedigiGol += evGol;

            if (evGol > depGol)
            {
                ev.Galibiyet++;
                dep.Maglubiyet++;
            }
            else if (evGol < depGol)
            {
                dep.Galibiyet++;
                ev.Maglubiyet++;
            }
            else
            {
                ev.Beraberlik++;
                dep.Beraberlik++;
            }

            TabloKaydet(tablo);
        }

        // -----------------------------------------------
        //  SIRALI TABLO GETİR
        // -----------------------------------------------
        /// <summary>
        /// Önce puana, sonra averaja, sonra atılan gole göre sıralar.
        /// </summary>
        public static List<LigSatiri> SiraliTabloGetir()
        {
            return TabloYukle()
                .OrderByDescending(s => s.Puan)
                .ThenByDescending(s => s.AvantajFarki)
                .ThenByDescending(s => s.AttigiGol)
                .ToList();
        }

        // -----------------------------------------------
        //  LİGİ SIFIRLA
        // -----------------------------------------------
        public static void LigiSifirla()
        {
            Preferences.Remove(TERCIH_ANAHTARI);
        }

        // -----------------------------------------------
        //  YARDIMCI: Takımı bul, yoksa oluştur
        // -----------------------------------------------
        private static LigSatiri SatiriBul(List<LigSatiri> tablo, string takimAdi)
        {
            var satir = tablo.FirstOrDefault(s => s.TakimAdi == takimAdi);
            if (satir == null)
            {
                satir = new LigSatiri { TakimAdi = takimAdi };
                tablo.Add(satir);
            }
            return satir;
        }
    }
}
