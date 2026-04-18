using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.Maui.Storage;

namespace FutbolManagerMobil
{
    // ── LİG SATIRI ──
    public class LigSatiri
    {
        public string TakimAdi { get; set; } = "";
        public int Oynadigi { get; set; } = 0;
        public int Galibiyet { get; set; } = 0;
        public int Beraberlik { get; set; } = 0;
        public int Maglubiyet { get; set; } = 0;
        public int AttigiGol { get; set; } = 0;
        public int YedigiGol { get; set; } = 0;

        public int AvantajFarki => AttigiGol - YedigiGol;
        public int Puan => Galibiyet * 3 + Beraberlik;
    }

    // ── MAÇ SONUCU MODELİ (fikstür listesi için) ──
    public class MacSonucu
    {
        public string EvTakim { get; set; } = "";
        public int EvGol { get; set; } = 0;
        public string DepTakim { get; set; } = "";
        public int DepGol { get; set; } = 0;
        public string EvHoca { get; set; } = "";
        public string DepHoca { get; set; } = "";
        public string Tarih { get; set; } = "";
    }

    // ── LİG MANAGERİ ──
    public static class LigManager
    {
        private const string TABLO_ANAHTAR = "lig_tablosu_v1";
        private const string FIKSTÜR_ANAHTAR = "lig_fikstür_v1";

        // ── TABLO YÜKLE / KAYDET ──
        public static List<LigSatiri> TabloYukle()
        {
            try
            {
                string json = Preferences.Get(TABLO_ANAHTAR, "");
                if (string.IsNullOrWhiteSpace(json)) return new();
                return JsonSerializer.Deserialize<List<LigSatiri>>(json) ?? new();
            }
            catch { return new(); }
        }

        private static void TabloKaydet(List<LigSatiri> tablo)
        {
            try { Preferences.Set(TABLO_ANAHTAR, JsonSerializer.Serialize(tablo)); }
            catch { }
        }

        // ── FİKSTÜR YÜKLE / KAYDET ──
        public static List<MacSonucu> FiksturYukle()
        {
            try
            {
                string json = Preferences.Get(FIKSTÜR_ANAHTAR, "");
                if (string.IsNullOrWhiteSpace(json)) return new();
                return JsonSerializer.Deserialize<List<MacSonucu>>(json) ?? new();
            }
            catch { return new(); }
        }

        private static void FiksturKaydet(List<MacSonucu> liste)
        {
            try { Preferences.Set(FIKSTÜR_ANAHTAR, JsonSerializer.Serialize(liste)); }
            catch { }
        }

        // ── MAÇ SONUCU İŞLE ──
        public static void MacSonucunuIsle(
            string evTakim, int evGol,
            string depTakim, int depGol,
            string evHoca = "", string depHoca = "")
        {
            // Puan tablosu güncelle
            var tablo = TabloYukle();
            var ev = SatiriBul(tablo, evTakim);
            var dep = SatiriBul(tablo, depTakim);

            ev.Oynadigi++; dep.Oynadigi++;
            ev.AttigiGol += evGol; ev.YedigiGol += depGol;
            dep.AttigiGol += depGol; dep.YedigiGol += evGol;

            if (evGol > depGol) { ev.Galibiyet++; dep.Maglubiyet++; }
            else if (evGol < depGol) { dep.Galibiyet++; ev.Maglubiyet++; }
            else { ev.Beraberlik++; dep.Beraberlik++; }

            TabloKaydet(tablo);

            // Fikstür listesine ekle
            var fikstür = FiksturYukle();
            fikstür.Insert(0, new MacSonucu
            {
                EvTakim = evTakim,
                EvGol = evGol,
                DepTakim = depTakim,
                DepGol = depGol,
                EvHoca = evHoca,
                DepHoca = depHoca,
                Tarih = DateTime.Now.ToString("dd.MM.yyyy HH:mm")
            });
            FiksturKaydet(fikstür);
        }

        // ── SIRALI TABLO ──
        public static List<LigSatiri> SiraliTabloGetir() =>
            TabloYukle()
                .OrderByDescending(s => s.Puan)
                .ThenByDescending(s => s.AvantajFarki)
                .ThenByDescending(s => s.AttigiGol)
                .ToList();

        // ── LİGİ SIFIRLA ──
        public static void LigiSifirla()
        {
            Preferences.Remove(TABLO_ANAHTAR);
            Preferences.Remove(FIKSTÜR_ANAHTAR);
        }

        private static LigSatiri SatiriBul(List<LigSatiri> tablo, string takim)
        {
            var s = tablo.FirstOrDefault(x => x.TakimAdi == takim);
            if (s == null) { s = new LigSatiri { TakimAdi = takim }; tablo.Add(s); }
            return s;
        }
    }
}
