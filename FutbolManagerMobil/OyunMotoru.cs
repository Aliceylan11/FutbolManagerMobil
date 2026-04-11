using System;
using System.Collections.Generic;
using System.Linq;

namespace FutbolManagerMobil
{
    public class OyunMotoru
    {
        // =====================================================
        //  ENUM'LAR
        // =====================================================
        public enum SahaBolgesi { K, D, DOS, MOS, OOS, F, K2 }

        public enum OyunModu
        {
            Klasik,
            Dengeli,
            Efsane
        }

        public enum MacTipi
        {
            Lig,   // 90 dk, beraberlik geçerli
            Kupa   // 90 → 105 → 120 → Seri Penaltı
        }

        public enum MacDurumu
        {
            Devam,
            Bitti,
            Uzatma1Basladi,
            Uzatma2Basladi,
            PenaltiBasladi
        }

        // =====================================================
        //  OYUN DEĞİŞKENLERİ
        // =====================================================
        public Takim evSahibi { get; set; }
        public Takim deplasman { get; set; }

        public int evSahibiGol = 0;
        public int deplasmanGol = 0;
        public int dakika = 1;

        public bool topEvSahibindeMi = true;
        public SahaBolgesi topunYeri = SahaBolgesi.MOS;
        public bool macBittiMi = false;

        public string EvHocaAdi { get; set; } = "Hoca";
        public string DepHocaAdi { get; set; } = "Hoca";
        public OyunModu SecilenMod { get; set; } = OyunModu.Klasik;
        public MacTipi SecilenTip { get; set; } = MacTipi.Lig;

        public int UzatmaAsamasi { get; private set; } = 0;
        public string PenaltiKazanani { get; private set; } = "";

        public List<string> EfsaneBoostAldilar { get; private set; } = new List<string>();

        // =====================================================
        //  DÜELLO SİSTEMİ
        // =====================================================
        public bool SavunmaBekleniyorMu { get; set; } = false;
        public string SonSecilenHucumHamlesi { get; set; } = "";
        public MacDurumu SonMacDurumu { get; private set; } = MacDurumu.Devam;

        // =====================================================
        //  KOORDİNATLAR
        // =====================================================
        public int SahaGenislik { get; set; } = 800;
        public int SahaYukseklik { get; set; } = 400;
        public int topX = 400, topY = 200;

        // =====================================================
        //  EVENTLER
        // =====================================================
        public Action<string> SpikerMesajYayinla;
        public Action<bool> GolOlduEkranaBildir;

        // =====================================================
        //  DİNAMİK SÜRE
        // =====================================================
        private int HamleSuresiHesapla(string hamle)
        {
            if (hamle.Contains("GOL VURUŞU") || hamle.Contains("Şut")) return 3;
            if (hamle.Contains("Çalım") || hamle.Contains("Ara")) return 2;
            return 1;
        }

        // =====================================================
        //  MAÇ BİTİŞ KONTROLÜ
        // =====================================================
        private MacDurumu MacBitisKontrol()
        {
            bool topTehlikeliBolgede =
                topunYeri == SahaBolgesi.OOS || topunYeri == SahaBolgesi.F;

            bool beraberlik = evSahibiGol == deplasmanGol;
            bool kupaModu = SecilenTip == MacTipi.Kupa;

            // Normal süre sonu 90
            if (dakika >= 90 && UzatmaAsamasi == 0)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;
                if (kupaModu && beraberlik)
                {
                    UzatmaAsamasi = 1;
                    dakika = 91;
                    SpikerMesajYayinla?.Invoke("90 dakika! Beraberlik var — UZATMAYA GİDİYORUZ!");
                    return MacDurumu.Uzatma1Basladi;
                }
                return MacDurumu.Bitti;
            }

            // 1. Uzatma sonu 105
            if (dakika >= 105 && UzatmaAsamasi == 1)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;
                if (beraberlik)
                {
                    UzatmaAsamasi = 2;
                    dakika = 106;
                    SpikerMesajYayinla?.Invoke("105 dakika! Yine beraberlik — 2. UZATMAYA!");
                    return MacDurumu.Uzatma2Basladi;
                }
                return MacDurumu.Bitti;
            }

            // 2. Uzatma sonu 120
            if (dakika >= 120 && UzatmaAsamasi == 2)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;
                if (beraberlik)
                {
                    UzatmaAsamasi = 3;
                    macBittiMi = true;
                    SeriPenaltilaraGec();
                    return MacDurumu.PenaltiBasladi;
                }
                return MacDurumu.Bitti;
            }

            return MacDurumu.Devam;
        }

        // =====================================================
        //  SERİ PENALTI
        // =====================================================
        public void SeriPenaltilaraGec()
        {
            SpikerMesajYayinla?.Invoke("⚡ SERİ PENALTI ATIŞLARI BAŞLADI! ⚡");

            Random rnd = new Random();
            int evGol = 0;
            int depGol = 0;
            int tur = 0;

            Futbolcu evAtic = evSahibi.Forvet ?? (Futbolcu)evSahibi.OOS;
            Futbolcu depAtic = deplasman.Forvet ?? (Futbolcu)deplasman.OOS;
            Kaleci evKal = evSahibi.Kaleci;
            Kaleci depKal = deplasman.Kaleci;

            while (true)
            {
                tur++;

                bool evAttı = !depKal.PenaltiKurtar(evAtic);
                bool depAttı = !evKal.PenaltiKurtar(depAtic);
                if (evAttı) evGol++;
                if (depAttı) depGol++;

                SpikerMesajYayinla?.Invoke(
                    $"  Tur {tur}: {evSahibi.Isim} {evGol} – {depGol} {deplasman.Isim}");

                // En az 5 tur, sonra biri önde çıkınca bitir (sudden death mantığı dahil)
                if (tur >= 5 && evGol != depGol) break;
            }

            string kazanan = evGol > depGol ? evSahibi.Isim : deplasman.Isim;
            PenaltiKazanani = kazanan;
            SpikerMesajYayinla?.Invoke(
                $"🏆 PENALTI KAZANANI: {kazanan}! ({evGol}-{depGol})");
        }

        // =====================================================
        //  MOD UYGULAMA
        // =====================================================
        public void ModUygula()
        {
            EfsaneBoostAldilar.Clear();
            switch (SecilenMod)
            {
                case OyunModu.Klasik: break;
                case OyunModu.Dengeli:
                    DengelemeUygula(evSahibi);
                    DengelemeUygula(deplasman);
                    break;
                case OyunModu.Efsane:
                    DengelemeUygula(evSahibi);
                    DengelemeUygula(deplasman);
                    EfsaneBoostUygula(evSahibi);
                    break;
            }
        }

        private const int DENGE_DEGERI = 75;

        private static void DengelemeUygula(Takim takim)
        {
            foreach (var o in TakimOyunculari(takim)) DengeleFutbolcu(o);
        }

        private static void DengeleFutbolcu(Futbolcu f)
        {
            f.Hiz = f.Guc = f.Dayaniklilik = f.KararAlma = f.Vizyon =
            f.Sogukkanlilik = f.Agresiflik = f.Teknik = f.Pas =
            f.Bitiricilik = f.Topcalma = f.Atak = f.Def = f.Kalecilik = DENGE_DEGERI;
        }

        private void EfsaneBoostUygula(Takim takim)
        {
            const int BOOST = 5;
            var secilenler = TakimOyunculari(takim)
                .OrderBy(_ => new Random().Next()).Take(3).ToList();
            foreach (var o in secilenler)
            {
                o.Hiz += BOOST; o.Teknik += BOOST;
                o.Def += BOOST; o.Bitiricilik += BOOST; o.Guc += BOOST;
                EfsaneBoostAldilar.Add(o.isim);
            }
        }

        public static IEnumerable<Futbolcu> TakimOyunculari(Takim takim)
        {
            if (takim.Kaleci != null) yield return takim.Kaleci;
            if (takim.Defans != null) yield return takim.Defans;
            if (takim.DOS != null) yield return takim.DOS;
            if (takim.MOS != null) yield return takim.MOS;
            if (takim.OOS != null) yield return takim.OOS;
            if (takim.Forvet != null) yield return takim.Forvet;
        }

        // =====================================================
        //  YARDIMCI METODLAR
        // =====================================================
        private void TopKaptir(SahaBolgesi yeniYer)
        {
            topEvSahibindeMi = !topEvSahibindeMi;
            topunYeri = yeniYer;
            SpikerMesajYayinla?.Invoke("KRİTİK HATA! Top rakibe geçti!");
            HedefleriBelirle();
        }

        public void GolOldu(string goluAtanIsim)
        {
            if (topEvSahibindeMi) evSahibiGol++; else deplasmanGol++;
            SpikerMesajYayinla?.Invoke($"GOOOOOOL! {goluAtanIsim} fileleri havalandırdı!");
            GolOlduEkranaBildir?.Invoke(topEvSahibindeMi);
            topunYeri = SahaBolgesi.MOS;
            topEvSahibindeMi = !topEvSahibindeMi;
            HedefleriBelirle();
        }

        public void HucumHamlesiBaslat(string hamle)
        {
            if (macBittiMi) return;
            SonSecilenHucumHamlesi = hamle;
            SavunmaBekleniyorMu = true;
            string hTakim = topEvSahibindeMi ? evSahibi.Isim : deplasman.Isim;
            SpikerMesajYayinla?.Invoke($"{hTakim} atağı: {hamle} deniyor!");
        }

        // =====================================================
        //  SAVUNMA HAMLESİ (OYUNUN KALBİ)
        // =====================================================
        public void SavunmaHamlesiYap(string sHamle)
        {
            if (!SavunmaBekleniyorMu) return;
            Random rnd = new Random();

            dakika += HamleSuresiHesapla(SonSecilenHucumHamlesi);

            Takim hT = topEvSahibindeMi ? evSahibi : deplasman;
            Takim sT = topEvSahibindeMi ? deplasman : evSahibi;

            if (SonSecilenHucumHamlesi.Contains("Pas") || SonSecilenHucumHamlesi.Contains("Ara"))
                hT.ToplamPas++;
            else if (SonSecilenHucumHamlesi.Contains("Şut") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                hT.ToplamSut++;

            bool basarili = false;
            Futbolcu hcu = null, sav = null;

            switch (topunYeri)
            {
                case SahaBolgesi.K:
                    basarili = SonSecilenHucumHamlesi.Contains("Kısa Pas")
                        ? hT.Kaleci.KisaPasAt(sT.Forvet)
                        : hT.Kaleci.UzunPasAt(sT.OOS);
                    if (basarili)
                    {
                        topunYeri = SonSecilenHucumHamlesi.Contains("Degaj") ? SahaBolgesi.DOS : SahaBolgesi.D;
                        SpikerMesajYayinla?.Invoke($"{hT.Kaleci.isim} oyunu geriden kurdu.");
                    }
                    else TopKaptir(SahaBolgesi.F);
                    break;

                case SahaBolgesi.D:
                    if (SonSecilenHucumHamlesi.Contains("Geri Pas"))
                    {
                        if (rnd.Next(1, 101) <= 5) TopKaptir(SahaBolgesi.F);
                        else { topunYeri = SahaBolgesi.K; SpikerMesajYayinla?.Invoke("Kaleciye güvenli dönüş."); }
                    }
                    else
                    {
                        basarili = hT.Defans.KisaPasAt(sT.MOS);
                        if (basarili) { topunYeri = SahaBolgesi.DOS; SpikerMesajYayinla?.Invoke($"{hT.Defans.isim} topu orta sahaya çıkardı."); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;

                case SahaBolgesi.DOS:
                    hcu = hT.DOS; sav = sT.OOS;
                    if (SonSecilenHucumHamlesi.Contains("Kısa Pas (OOS)"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} topu OOS'a aktardı!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        basarili = hcu.CalimAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.MOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} çalımla orta sahaya!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Geri Pas"))
                    {
                        topunYeri = SahaBolgesi.D;
                        SpikerMesajYayinla?.Invoke($"{hcu.isim} risk almadı.");
                    }
                    break;

                case SahaBolgesi.MOS:
                    hcu = hT.MOS; sav = sT.MOS;
                    if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                    {
                        SpikerMesajYayinla?.Invoke($"{hcu.isim} o mesafeden şut, etkisiz!");
                        TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} tehlikeli ara pası!"); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} dikine pasla hücum!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;

                case SahaBolgesi.OOS:
                    hcu = hT.OOS; sav = sT.DOS;
                    if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu.KisaPasAt(sav);
                        if (basarili) { topunYeri = SahaBolgesi.F; SpikerMesajYayinla?.Invoke($"{hcu.isim} şahane ara pası!"); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                    {
                        if (((OrtaSaha)hcu).UzaktanSutAt(sT.Kaleci))
                            GolOldu(hcu.isim);
                        else
                        {
                            topunYeri = SahaBolgesi.K;
                            topEvSahibindeMi = !topEvSahibindeMi;
                            SpikerMesajYayinla?.Invoke("Müthiş şut ama kaleci kurtardı! Top karşı takımda.");
                        }
                    }
                    break;

                case SahaBolgesi.F:
                    hcu = hT.Forvet; sav = sT.Kaleci;
                    if (SonSecilenHucumHamlesi.Contains("GOL VURUŞU") || SonSecilenHucumHamlesi.Contains("Şut Çek"))
                    {
                        if (sT.Kaleci.KurtarisYap(hT.Forvet)) { TopKaptir(SahaBolgesi.K); SpikerMesajYayinla?.Invoke($"{sT.Kaleci.isim} devleşti!"); }
                        else GolOldu(hcu.isim);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        if (hcu.CalimAt(sav)) { SpikerMesajYayinla?.Invoke($"{hcu.isim} kaleciyi geçti!"); GolOldu(hcu.isim); }
                        else TopKaptir(SahaBolgesi.K);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        if (hcu.KisaPasAt(sT.Defans)) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} geri çıkardı."); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    break;
            }

            SavunmaBekleniyorMu = false;
            SonMacDurumu = MacBitisKontrol();
            if (SonMacDurumu == MacDurumu.Bitti)
                macBittiMi = true;

            HedefleriBelirle();
        }

        // =====================================================
        //  KOORDİNAT SİSTEMİ
        // =====================================================
        public void HedefleriBelirle()
        {
            int w = SahaGenislik, h = SahaYukseklik;
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
