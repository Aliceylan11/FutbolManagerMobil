using System;
using System.Collections.Generic;

namespace FutbolManagerMobil
{
    public class OyunMotoru
    {
        public enum SahaBolgesi { K, D, DOS, MOS, OOS, F, K2 }

        // 1. OYUN DEĞİŞKENLERİ
        public Takim evSahibi { get; set; }
        public Takim deplasman { get; set; }
        public int evSahibiGol = 0, deplasmanGol = 0;
        public int dakika = 1;
        public bool topEvSahibindeMi = true;
        public SahaBolgesi topunYeri = SahaBolgesi.MOS;
        public bool macBittiMi = false;

        // --- DÜELLO SİSTEMİ DEĞİŞKENLERİ ---
        public bool SavunmaBekleniyorMu { get; set; } = false;
        public string SonSecilenHucumHamlesi { get; set; } = "";

        // 2. İSTATİSTİKLER VE KOORDİNATLAR
        public int SahaGenislik { get; set; } = 800;
        public int SahaYukseklik { get; set; } = 400;
        public int topX = 400, topY = 200;

        // 3. EVENTLER
        public Action<string> SpikerMesajYayinla;
        public Action<bool> GolOlduEkranaBildir;

        // --- YARDIMCI METODLAR ---
        private void TopKaptir(SahaBolgesi yeniYer)
        {
            topEvSahibindeMi = !topEvSahibindeMi;
            topunYeri = yeniYer;
            SpikerMesajYayinla?.Invoke("KRİTİK HATA! Top rakibe geçti!");
            HedefleriBelirle(); // Topun yerini görsel olarak güncelle
        }

        public void GolOldu(string goluAtanIsim)
        {
            if (topEvSahibindeMi) evSahibiGol++; else deplasmanGol++;
            SpikerMesajYayinla?.Invoke($"GOOOOOOL! {goluAtanIsim} fileleri havalandırdı!");
            GolOlduEkranaBildir?.Invoke(topEvSahibindeMi);

            topunYeri = SahaBolgesi.MOS;
            topEvSahibindeMi = !topEvSahibindeMi; // Golü yiyen başlar
            HedefleriBelirle();
        }

        // --- HÜCUM HAMLESİ BAŞLATMA (SPİKERİ KONUŞTURUR) ---
        public void HucumHamlesiBaslat(string hamle)
        {
            if (macBittiMi) return;
            SonSecilenHucumHamlesi = hamle;
            SavunmaBekleniyorMu = true;

            string hTakim = topEvSahibindeMi ? evSahibi.Isim : deplasman.Isim;
            SpikerMesajYayinla?.Invoke($"{hTakim} atağı: {hamle} deniyor!");
        }

        // --- SAVUNMA VE DÜELLO (OYUNUN KALBİ) ---
        public void SavunmaHamlesiYap(string sHamle)
        {
            if (!SavunmaBekleniyorMu) return;
            Random rnd = new Random();
            dakika += rnd.Next(1, 4);

            Takim hT = topEvSahibindeMi ? evSahibi : deplasman;
            Takim sT = topEvSahibindeMi ? deplasman : evSahibi;

            if (SonSecilenHucumHamlesi.Contains("Pas") || SonSecilenHucumHamlesi.Contains("Ara"))
            {
                hT.ToplamPas++;
            }
            else if (SonSecilenHucumHamlesi.Contains("Şut") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
            {
                hT.ToplamSut++;
            }
            bool basarili = false;
            Futbolcu hcu = null;
            Futbolcu sav = null;

            switch (topunYeri)
            {
                case SahaBolgesi.K: // KALECİ HAREKATI
                    if (SonSecilenHucumHamlesi.Contains("Kısa Pas"))
                        basarili = hT.Kaleci.KisaPasAt(sT.Forvet);
                    else
                        basarili = hT.Kaleci.UzunPasAt(sT.OOS);

                    if (basarili)
                    {
                        topunYeri = SonSecilenHucumHamlesi.Contains("Degaj") ? SahaBolgesi.DOS : SahaBolgesi.D;
                        SpikerMesajYayinla?.Invoke($"{hT.Kaleci.isim} oyunu geriden kurdu.");
                    }
                    else TopKaptir(SahaBolgesi.F);
                    break;

                case SahaBolgesi.D: // DEFANS HAREKATI
                    if (SonSecilenHucumHamlesi.Contains("Geri Pas"))
                    {
                        if (rnd.Next(1, 101) <= 5) TopKaptir(SahaBolgesi.F);
                        else { topunYeri = SahaBolgesi.K; SpikerMesajYayinla?.Invoke("Kritik baskıda kaleciye güvenli dönüş."); }
                    }
                    else
                    {
                        basarili = hT.Defans.KisaPasAt(sT.MOS);
                        if (basarili) { topunYeri = SahaBolgesi.DOS; SpikerMesajYayinla?.Invoke($"{hT.Defans.isim} topu orta sahaya çıkardı."); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;
                case SahaBolgesi.DOS: // DEFANSİF ORTA SAHA
                    hcu = hT.DOS; sav = sT.OOS; // Rakip 10 numara basıyor
                    if (SonSecilenHucumHamlesi.Contains("Kısa Pas (OOS)"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili)
                        {
                            topunYeri = SahaBolgesi.OOS;
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} baskıdan sıyrıldı, topu OOS'a aktardı!");
                        }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        basarili = hcu.CalimAt(sav);
                        if (basarili)
                        {
                            topunYeri = SahaBolgesi.MOS;
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} zarif bir çalımla topu orta sahaya taşıdı!");
                        }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Geri Pas"))
                    {
                        topunYeri = SahaBolgesi.D;
                        SpikerMesajYayinla?.Invoke($"{hcu.isim} risk almadı, savunmasına döndü.");
                    }
                    break;



                case SahaBolgesi.MOS: // MERKEZ ORTA SAHA (Santra sonrası)
                    hcu = hT.MOS;
                    sav = sT.MOS;

                    // MOS bölgesinde Şut çekilemez, sadece pas atılabilir veya oyun kurulabilir.
                    // Eğer butondan yanlışlıkla Şut geldiyse bile bunu pas denemesi olarak algıla.
                    if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                    {
                        SpikerMesajYayinla?.Invoke($"{hcu.isim} o mesafeden şut denedi ama çok etkisiz! Top savunmaya geçti.");
                        TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili)
                        {
                            topunYeri = SahaBolgesi.OOS;
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} tehlikeli bir ara pasıyla forvet arkasına oynadı!");
                        }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else // Normal Kısa Pas veya Oyun Kurma durumu
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili)
                        {
                            topunYeri = SahaBolgesi.OOS;
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} dikine kısa pasla hücumu başlattı!");
                        }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;

                case SahaBolgesi.OOS: // OFANSİF ORTA SAHA
                    hcu = hT.OOS; sav = sT.DOS;
                    if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.F; SpikerMesajYayinla?.Invoke($"{hcu.isim} forvete şahane bir ara pası!"); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                    {
                        // Şutun gol olup olmadığını kontrol et
                        if (((OrtaSaha)hcu).UzaktanSutAt(sT.Kaleci))
                        {
                            GolOldu(hcu.isim);
                        }
                        else
                        {
                            // --- HATA BURADAYDI, DÜZELTİLDİ ---
                            topunYeri = SahaBolgesi.K; // Top rakip kaleciye gider
                            topEvSahibindeMi = !topEvSahibindeMi; // TOP RAKİBE GEÇER (Önemli!)

                            SpikerMesajYayinla?.Invoke("Müthiş şut ama kaleci başarılı! Top şimdi karşı takımda.");
                        }
                    }
                    break;

                case SahaBolgesi.F: // FORVET HAREKATI
                    hcu = hT.Forvet; sav = sT.Kaleci;

                    // DÜZELTME: Buton metni "GOL VURUŞU" olduğu için kontrolü güncelledik
                    if (SonSecilenHucumHamlesi.Contains("GOL VURUŞU") || SonSecilenHucumHamlesi.Contains("Şut Çek"))
                    {
                        if (sT.Kaleci.KurtarisYap(hT.Forvet))
                        {
                            // Kaleci kurtardıysa top artık onun mülkiyetine geçer
                            TopKaptir(SahaBolgesi.K);
                            SpikerMesajYayinla?.Invoke($"{sT.Kaleci.isim} devleşti! Topun yeni sahibi kaleci.");
                        }
                        else
                        {
                            GolOldu(hcu.isim); // Gol olduysa zaten santra yapılacak
                        }
                    }
                    // Forvet için diğer buton seçeneklerini de ekleyelim ki onlar da takılmasın
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        if (hcu.CalimAt(sav))
                        {
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} kaleciyi de geçti ve boş kaleye bıraktı!");
                            GolOldu(hcu.isim);
                        }
                        else TopKaptir(SahaBolgesi.K);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        if (hcu.KisaPasAt(sT.Defans))
                        {
                            topunYeri = SahaBolgesi.OOS; // Topu geriye çıkardı
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} bencil davranmadı, topu geriye çıkardı.");
                        }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    break;
            }

            SavunmaBekleniyorMu = false;
            HedefleriBelirle(); // TOPU GÖRSEL OLARAK İLERLET
        }

        public void HedefleriBelirle() // KOORDİNAT SİSTEMİ
        {
            int w = SahaGenislik; int h = SahaYukseklik;
            Random rnd = new Random();
            switch (topunYeri)
            {
                case SahaBolgesi.K: topX = topEvSahibindeMi ? (int)(w * 0.05) : (int)(w * 0.95); break;
                case SahaBolgesi.D: topX = topEvSahibindeMi ? (int)(w * 0.20) : (int)(w * 0.80); break;
                case SahaBolgesi.DOS: topX = topEvSahibindeMi ? (int)(w * 0.35) : (int)(w * 0.65); break;
                case SahaBolgesi.MOS: topX = w / 2; break;
                case SahaBolgesi.OOS: topX = topEvSahibindeMi ? (int)(w * 0.65) : (int)(w * 0.35); break;
                case SahaBolgesi.F: topX = topEvSahibindeMi ? (int)(w * 0.85) : (int)(w * 0.15); break;
            }
            topY = (h / 2) + rnd.Next(-20, 20);
        }
    }
}