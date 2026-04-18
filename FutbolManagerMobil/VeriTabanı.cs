using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;

namespace FutbolManagerMobil
{
    public class Veritabani
    {
        public static List<Takim> TumTakimlar { get; private set; } = new List<Takim>();

        // Uygulama açılır açılmaz JSON'u okuyan "Sihirli" Metot
        public static async Task JSONVerileriniYukleAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("teams.json");
                using var reader = new StreamReader(stream);
                var jsonIcerik = await reader.ReadToEndAsync();

                // Büyük/Küçük harf duyarlılığını kapat (Hataları önler)
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Önce JSON'u geçici "Çevirmen" sınıflarımıza (DTO) yüklüyoruz
                var jsonTakimlar = JsonSerializer.Deserialize<List<JsonTakim>>(jsonIcerik, options);

                TumTakimlar.Clear();
                if (jsonTakimlar != null)
                {
                    foreach (var jt in jsonTakimlar)
                    {
                        Takim t = new Takim(jt.Isim) { TakimID = jt.TakimID };

                        // JSON verisini, senin oyun motorunun alt sınıflarına (Kaleci, Defans vs.) çeviriyoruz
                        if (jt.Oyuncular.ContainsKey("Kaleci")) t.Kaleci = OkuKaleci(jt.Oyuncular["Kaleci"]);
                        if (jt.Oyuncular.ContainsKey("Defans")) t.Defans = OkuDefans(jt.Oyuncular["Defans"]);
                        if (jt.Oyuncular.ContainsKey("DOS")) t.DOS = OkuOrtaSaha(jt.Oyuncular["DOS"]);
                        if (jt.Oyuncular.ContainsKey("MOS")) t.MOS = OkuOrtaSaha(jt.Oyuncular["MOS"]);
                        if (jt.Oyuncular.ContainsKey("OOS")) t.OOS = OkuOrtaSaha(jt.Oyuncular["OOS"]);
                        if (jt.Oyuncular.ContainsKey("Forvet")) t.Forvet = OkuForvet(jt.Oyuncular["Forvet"]);

                        TumTakimlar.Add(t);
                    }
                }
                Console.WriteLine($"[BAŞARILI] {TumTakimlar.Count} takım JSON'dan yüklendi!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[HATA] Veritabanı Yüklenemedi: {ex.Message}");
            }
        }

        // --- ÇEVİRMEN METOTLAR ---
        private static Kaleci OkuKaleci(JsonOyuncu jo) => new Kaleci { isim = jo.isim, boy = jo.boy, Guc = jo.Guc, Hiz = jo.Hiz, Dayaniklilik = jo.Dayaniklilik, KararAlma = jo.KararAlma, Vizyon = jo.Vizyon, Sogukkanlilik = jo.Sogukkanlilik, Agresiflik = jo.Agresiflik, Teknik = jo.Teknik, Pas = jo.Pas, Bitiricilik = jo.Bitiricilik, Topcalma = jo.Topcalma, Atak = jo.Atak, Def = jo.Def, Kalecilik = jo.Kalecilik, Moral = jo.Moral, Kondisyon = jo.Kondisyon };
        private static Defans OkuDefans(JsonOyuncu jo) => new Defans { isim = jo.isim, boy = jo.boy, Guc = jo.Guc, Hiz = jo.Hiz, Dayaniklilik = jo.Dayaniklilik, KararAlma = jo.KararAlma, Vizyon = jo.Vizyon, Sogukkanlilik = jo.Sogukkanlilik, Agresiflik = jo.Agresiflik, Teknik = jo.Teknik, Pas = jo.Pas, Bitiricilik = jo.Bitiricilik, Topcalma = jo.Topcalma, Atak = jo.Atak, Def = jo.Def, Kalecilik = jo.Kalecilik, Moral = jo.Moral, Kondisyon = jo.Kondisyon };
        private static OrtaSaha OkuOrtaSaha(JsonOyuncu jo) => new OrtaSaha { isim = jo.isim, boy = jo.boy, Guc = jo.Guc, Hiz = jo.Hiz, Dayaniklilik = jo.Dayaniklilik, KararAlma = jo.KararAlma, Vizyon = jo.Vizyon, Sogukkanlilik = jo.Sogukkanlilik, Agresiflik = jo.Agresiflik, Teknik = jo.Teknik, Pas = jo.Pas, Bitiricilik = jo.Bitiricilik, Topcalma = jo.Topcalma, Atak = jo.Atak, Def = jo.Def, Kalecilik = jo.Kalecilik, Moral = jo.Moral, Kondisyon = jo.Kondisyon };
        private static Forvet OkuForvet(JsonOyuncu jo) => new Forvet { isim = jo.isim, boy = jo.boy, Guc = jo.Guc, Hiz = jo.Hiz, Dayaniklilik = jo.Dayaniklilik, KararAlma = jo.KararAlma, Vizyon = jo.Vizyon, Sogukkanlilik = jo.Sogukkanlilik, Agresiflik = jo.Agresiflik, Teknik = jo.Teknik, Pas = jo.Pas, Bitiricilik = jo.Bitiricilik, Topcalma = jo.Topcalma, Atak = jo.Atak, Def = jo.Def, Kalecilik = jo.Kalecilik, Moral = jo.Moral, Kondisyon = jo.Kondisyon };

        // Oyun motorunun çağıracağı fonksiyonlar (Eski sistemle %100 uyumlu)
        public static Takim TakimCek(int takimId, string takimIsmi = "")
        {
            var takim = TumTakimlar.FirstOrDefault(t => t.TakimID == takimId);
            if (takim != null) return takim;
            return new Takim(takimIsmi) { TakimID = takimId }; // Hata vermemesi için
        }

        public static List<Takim> TumTakimlariGetir()
        {
            return TumTakimlar;
        }
    }

    // --- JSON OKUMA (DTO) SINIFLARI ---
    public class JsonTakim
    {
        public int TakimID { get; set; }
        public string Isim { get; set; }
        public Dictionary<string, JsonOyuncu> Oyuncular { get; set; } = new Dictionary<string, JsonOyuncu>();
    }

    public class JsonOyuncu
    {
        public string isim { get; set; }
        public int boy { get; set; }
        public int Guc { get; set; }
        public int Hiz { get; set; }
        public int Dayaniklilik { get; set; }
        public int KararAlma { get; set; }
        public int Vizyon { get; set; }
        public int Sogukkanlilik { get; set; }
        public int Agresiflik { get; set; }
        public int Teknik { get; set; }
        public int Pas { get; set; }
        public int Bitiricilik { get; set; }
        public int Topcalma { get; set; }
        public int Atak { get; set; }
        public int Def { get; set; }
        public int Kalecilik { get; set; }
        public int Moral { get; set; } = 100;
        public int Kondisyon { get; set; } = 100;
    }
}