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
        public enum OyunModu { Klasik, Dengeli, Efsane }
        public enum MacTipi { Lig, Kupa }

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
        public Takim evSahibi { get; set; } = new Takim();
        public Takim deplasman { get; set; } = new Takim();

        public int evSahibiGol = 0;
        public int deplasmanGol = 0;
        public int dakika = 1;

        public bool topEvSahibindeMi = true;
        public SahaBolgesi topunYeri = SahaBolgesi.MOS;
        public bool macBittiMi = false;

        // Santra kontrolü — gol/başlangıç sonrası MOS'ta özel mod
        public bool IsSantra { get; set; } = true;

        public string EvHocaAdi { get; set; } = "Hoca";
        public string DepHocaAdi { get; set; } = "Hoca";
        public OyunModu SecilenMod { get; set; } = OyunModu.Klasik;
        public MacTipi SecilenTip { get; set; } = MacTipi.Lig;

        public int UzatmaAsamasi { get; private set; } = 0;

        // =====================================================
        //  EFSANE BOOST — her iki takım için ayrı liste
        // =====================================================
        public List<string> EvEfsaneBoostlar { get; private set; } = new();
        public List<string> DepEfsaneBoostlar { get; private set; } = new();

        // =====================================================
        //  DÜELLO SİSTEMİ
        // =====================================================
        public bool SavunmaBekleniyorMu { get; set; } = false;
        public string SonSecilenHucumHamlesi { get; set; } = "";
        public MacDurumu SonMacDurumu { get; private set; } = MacDurumu.Devam;

        // =====================================================
        //  İNTERAKTİF PENALTİ SİSTEMİ
        // =====================================================
        public bool PenaltiAsamasindaMi { get; private set; } = false;
        public bool PenaltiEvSirasindaMi { get; private set; } = true;  // true=ev atar, false=dep atar
        public int PenaltiEvGol { get; private set; } = 0;
        public int PenaltiDepGol { get; private set; } = 0;
        public int PenaltiTur { get; private set; } = 0;      // tamamlanan tur
        public int PenaltiHamle { get; private set; } = 0;      // tur içi hamle (0=ev, 1=dep)
        public bool PenaltiAniOlum { get; private set; } = false;
        public string PenaltiKazanani { get; private set; } = "";
        public string PenaltiSonMesaj { get; private set; } = "";

        // =====================================================
        //  KOORDİNATLAR
        // =====================================================
        public int SahaGenislik { get; set; } = 800;
        public int SahaYukseklik { get; set; } = 400;
        public int topX = 400, topY = 200;

        // =====================================================
        //  EVENTLER
        // =====================================================
        public Action<string>? SpikerMesajYayinla;
        public Action<bool>? GolOlduEkranaBildir;

        // =====================================================
        //  DİNAMİK SÜRE
        // =====================================================
        private int HamleSuresiHesapla(string hamle)
        {
            Random _r = new();
            if (hamle.Contains("GOL VURUŞU") || hamle.Contains("Şut")) return _r.Next(4, 7);
            if (hamle.Contains("Çalım") || hamle.Contains("Ara")) return _r.Next(3, 5);
            return _r.Next(2, 4);
        }

        // =====================================================
        //  MAÇ BİTİŞ KONTROLÜ
        // =====================================================
        private MacDurumu MacBitisKontrol()
        {
            bool topTehlikeliBolgede = topunYeri == SahaBolgesi.OOS || topunYeri == SahaBolgesi.F;

            bool beraberlik = evSahibiGol == deplasmanGol;
            bool kupaModu = SecilenTip == MacTipi.Kupa;

            // --- 90. Dakika Kontrolü ---
            if (dakika >= 90 && UzatmaAsamasi == 0)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;

                if (kupaModu && beraberlik)
                {
                    UzatmaAsamasi = 1;
                    dakika = 91;
                    SpikerMesajYayinla?.Invoke("90 dakika bitti! Kazanan çıkmadı, UZATMALARA GİDİYORUZ!");
                    return MacDurumu.Uzatma1Basladi;
                }
                return MacDurumu.Bitti;
            }

            // --- 105. Dakika Kontrolü ---
            if (dakika >= 105 && UzatmaAsamasi == 1)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;

                // Uzatmalar iki devredir, skor ne olursa olsun 120'ye kadar oynanır.
                UzatmaAsamasi = 2;
                dakika = 106;
                SpikerMesajYayinla?.Invoke("Uzatmanın ilk devresi bitti! heyecan devam ediyor.");
                return MacDurumu.Uzatma2Basladi;
            }

            // --- 120. Dakika Kontrolü ---
            if (dakika >= 120 && UzatmaAsamasi == 2)
            {
                if (topTehlikeliBolgede) return MacDurumu.Devam;

                if (beraberlik)
                {
                    // Eğer 120 sonunda hala beraberlik varsa penaltılar başlar 
                    UzatmaAsamasi = 3;
                    // DİKKAT: macBittiMi = true; satırını buradan sildim. 
                    // Çünkü penaltılar interaktif ise HamleYap metodunun başında takılırsın.
                    PenaltiBaslat();
                    return MacDurumu.PenaltiBasladi;
                }

                // Beraberlik yoksa maç burada biter
                return MacDurumu.Bitti;
            }

            return MacDurumu.Devam;
        }

        // =====================================================
        //  İNTERAKTİF PENALTİ — BAŞLAT
        // =====================================================
        public void PenaltiBaslat()
        {
            PenaltiAsamasindaMi = true;
            PenaltiEvSirasindaMi = true;   // Her turda önce ev sahibi atar
            PenaltiEvGol = 0;
            PenaltiDepGol = 0;
            PenaltiTur = 0;
            PenaltiHamle = 0;
            PenaltiAniOlum = false;
            SpikerMesajYayinla?.Invoke("⚡ SERİ PENALTİ ATIŞLARI BAŞLADI!");
            SpikerMesajYayinla?.Invoke($"Tur 1 — {evSahibi.Isim} başlıyor. Yön seçin!");
        }

        // =====================================================
        //  İNTERAKTİF PENALTİ — ATIŞ İŞLE
        //  yon: "SOL" | "ORTA" | "SAĞ"
        //  Geri dönüş: true = penaltı serisi bitti, false = devam
        // =====================================================
        public bool PenaltiYonuIsle(string yon)
        {
            string[] yonler = { "SOL", "ORTA", "SAĞ" };
            Random rnd = new();

            // Kaleci rastgele yön seçer
            string kaleci_yon = yonler[rnd.Next(3)];

            bool gol;
            string atanTakim, kaleden;

            if (PenaltiEvSirasindaMi)
            {
                // Ev sahibi atıyor, deplasman kalecisi kurtarıyor
                var atici = evSahibi.Forvet ?? (Futbolcu?)evSahibi.OOS;
                var kaleci = deplasman.Kaleci;

                // Yön tutarsa (%30) kurtarır, tutmazsa (%70) gol
                // Ama kalecinin Kalecilik değeri de devreye girer
                int kurtarmaBonus = (yon == kaleci_yon) ? 30 : 0;
                int kurtarisGucu = (int)((kaleci?.Kalecilik ?? 75) * 0.6) + kurtarmaBonus;
                int sutGucu = (int)((atici?.Bitiricilik ?? 75) * 0.7);
                gol = rnd.Next(sutGucu + kurtarisGucu) < sutGucu;

                atanTakim = evSahibi.Isim;
                kaleden = kaleci?.isim ?? "Kaleci";
                if (gol) PenaltiEvGol++;
                SpikerMesajYayinla?.Invoke(
                    $"  {atanTakim}: {yon} → Kaleci: {kaleci_yon} → {(gol ? "⚽ GOL!" : "🧤 Kurtarıldı!")}");

                PenaltiEvSirasindaMi = false; // Sıra deplasmanına geçer
            }
            else
            {
                // Deplasman atıyor, ev sahibi kalecisi kurtarıyor
                var atici = deplasman.Forvet ?? (Futbolcu?)deplasman.OOS;
                var kaleci = evSahibi.Kaleci;

                int kurtarmaBonus = (yon == kaleci_yon) ? 30 : 0;
                int kurtarisGucu = (int)((kaleci?.Kalecilik ?? 75) * 0.6) + kurtarmaBonus;
                int sutGucu = (int)((atici?.Bitiricilik ?? 75) * 0.7);
                gol = rnd.Next(sutGucu + kurtarisGucu) < sutGucu;

                atanTakim = deplasman.Isim;
                kaleden = kaleci?.isim ?? "Kaleci";
                if (gol) PenaltiDepGol++;
                SpikerMesajYayinla?.Invoke(
                    $"  {atanTakim}: {yon} → Kaleci: {kaleci_yon} → {(gol ? "⚽ GOL!" : "🧤 Kurtarıldı!")}");

                // Her iki takım attıktan sonra tur tamamlandı
                PenaltiTur++;
                PenaltiEvSirasindaMi = true;

                // Seri bitti mi kontrol et
                bool bitti = PenaltiBittiMiKontrol();
                if (bitti) return true;

                // Bir sonraki tura hazırla
                string turBilgi = PenaltiAniOlum
                    ? $"⚠️ ANİ ÖLÜM — Tur {PenaltiTur + 1}! {evSahibi.Isim} başlıyor."
                    : $"Tur {PenaltiTur + 1} / 5 — Skor: {PenaltiEvGol}-{PenaltiDepGol} — {evSahibi.Isim} başlıyor!";
                SpikerMesajYayinla?.Invoke(turBilgi);
            }

            return false; // Henüz bitmedi
        }

        private bool PentiBitecek => PenaltiTur >= 5 && PenaltiEvGol != PenaltiDepGol;

        private bool PenaltiBittiMiKontrol()
        {
            const int MIN_TUR = 5;

            if (PenaltiTur >= MIN_TUR)
            {
                if (PenaltiEvGol != PenaltiDepGol)
                {
                    // Kazanan belli
                    PenaltiKazanani = PenaltiEvGol > PenaltiDepGol ? evSahibi.Isim : deplasman.Isim;
                    PenaltiSonMesaj = $"🏆 {PenaltiKazanani} penaltıları kazandı! ({PenaltiEvGol}-{PenaltiDepGol})";
                    SpikerMesajYayinla?.Invoke(PenaltiSonMesaj);
                    PenaltiAsamasindaMi = false;
                    return true;
                }
                else if (!PenaltiAniOlum)
                {
                    // Ani ölüm başlıyor
                    PenaltiAniOlum = true;
                    SpikerMesajYayinla?.Invoke("⚠️ 5 turda eşitlik! ANİ ÖLÜM başlıyor...");
                }
                else
                {
                    // Ani ölümde de biri önde — bitti
                    // (Bu kontrol zaten yukarıda evGol != depGol koşulunda yakalanır)
                }
            }
            return false;
        }

        // Mevcut atıcı takım adını döndürür (UI için)
        public string SiradakiAtanTakim =>
            PenaltiEvSirasindaMi ? evSahibi.Isim : deplasman.Isim;

        // =====================================================
        //  MOD UYGULAMA — FIX: her iki takıma da boost
        // =====================================================
        public void ModUygula()
        {
            EvEfsaneBoostlar.Clear();
            DepEfsaneBoostlar.Clear();

            switch (SecilenMod)
            {
                case OyunModu.Klasik:
                    break;

                case OyunModu.Dengeli:
                    DengelemeUygula(evSahibi);
                    DengelemeUygula(deplasman);
                    break;

                case OyunModu.Efsane:
                    DengelemeUygula(evSahibi);
                    DengelemeUygula(deplasman);
                    // Her iki takıma da rastgele 3 oyuncu boost
                    EfsaneBoostUygula(evSahibi, EvEfsaneBoostlar);
                    EfsaneBoostUygula(deplasman, DepEfsaneBoostlar);
                    break;
            }
        }

        private const int DENGE_DEGERI = 75;

        private static void DengelemeUygula(Takim t)
        {
            foreach (var o in TakimOyunculari(t)) DengeleFutbolcu(o);
        }

        private static void DengeleFutbolcu(Futbolcu f)
        {
            f.Hiz = f.Guc = f.Dayaniklilik = f.KararAlma = f.Vizyon =
            f.Sogukkanlilik = f.Agresiflik = f.Teknik = f.Pas =
            f.Bitiricilik = f.Topcalma = f.Atak = f.Def = f.Kalecilik = DENGE_DEGERI;
        }

        private static void EfsaneBoostUygula(Takim t, List<string> kayit)
        {
            const int BOOST = 5;
            var secilenler = TakimOyunculari(t)
                .OrderBy(_ => new Random().Next()).Take(3).ToList();
            foreach (var o in secilenler)
            {
                o.Hiz += BOOST; o.Teknik += BOOST;
                o.Def += BOOST; o.Bitiricilik += BOOST; o.Guc += BOOST;
                kayit.Add(o.isim);
            }
        }

        public static IEnumerable<Futbolcu> TakimOyunculari(Takim t)
        {
            if (t.Kaleci != null) yield return t.Kaleci;
            if (t.Defans != null) yield return t.Defans;
            if (t.DOS != null) yield return t.DOS;
            if (t.MOS != null) yield return t.MOS;
            if (t.OOS != null) yield return t.OOS;
            if (t.Forvet != null) yield return t.Forvet;
        }

        // =====================================================
        //  YARDIMCI METODLAR
        // =====================================================
        private void TopKaptir(SahaBolgesi yeniYer)
        {
            topEvSahibindeMi = !topEvSahibindeMi;
            topunYeri = yeniYer;
            IsSantra = false;
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
            IsSantra = true;   // Gol sonrası santra
            HedefleriBelirle();
        }

        public void HucumHamlesiBaslat(string hamle)
        {
            if (macBittiMi) return;
            SonSecilenHucumHamlesi = hamle;
            SavunmaBekleniyorMu = true;
            IsSantra = false; // Santra bozuldu, oyun başladı
            string hTakim = topEvSahibindeMi ? evSahibi.Isim : deplasman.Isim;
            SpikerMesajYayinla?.Invoke($"{hTakim} atağı: {hamle} deniyor!");
        }

        // =====================================================
        //  SAVUNMA HAMLESİ (OYUNUN KALBİ)
        // =====================================================
        public void SavunmaHamlesiYap(string sHamle)
        {
            if (!SavunmaBekleniyorMu) return;
            Random rnd = new();

            dakika += HamleSuresiHesapla(SonSecilenHucumHamlesi);

            Takim hT = topEvSahibindeMi ? evSahibi : deplasman;
            Takim sT = topEvSahibindeMi ? deplasman : evSahibi;

            if (SonSecilenHucumHamlesi.Contains("Pas") || SonSecilenHucumHamlesi.Contains("Ara"))
                hT.ToplamPas++;
            else if (SonSecilenHucumHamlesi.Contains("Şut") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                hT.ToplamSut++;

            bool basarili = false;
            Futbolcu? hcu = null, sav = null;

            switch (topunYeri)
            {
                case SahaBolgesi.K:
                    basarili = SonSecilenHucumHamlesi.Contains("Kısa Pas")
                        ? hT.Kaleci!.KisaPasAt(sT.Forvet!)
                        : hT.Kaleci!.UzunPasAt(sT.OOS!);
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
                        basarili = hT.Defans!.KisaPasAt(sT.MOS!);
                        if (basarili) { topunYeri = SahaBolgesi.DOS; SpikerMesajYayinla?.Invoke($"{hT.Defans.isim} topu orta sahaya çıkardı."); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;

                case SahaBolgesi.DOS:
                    hcu = hT.DOS; sav = sT.OOS;
                    if (SonSecilenHucumHamlesi.Contains("Kısa Pas (OOS)"))
                    {
                        basarili = hcu!.KisaPasAt(sav!);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} OOS'a aktardı!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        basarili = hcu!.CalimAt(sav!);
                        if (basarili) { topunYeri = SahaBolgesi.MOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} çalımla orta sahaya!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Geri Pas"))
                    {
                        topunYeri = SahaBolgesi.D;
                        SpikerMesajYayinla?.Invoke($"{hcu?.isim} risk almadı.");
                    }
                    break;

                case SahaBolgesi.MOS:
                    hcu = hT.MOS; sav = sT.MOS;
                    if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("Gol Vuruşu"))
                    {
                        SpikerMesajYayinla?.Invoke($"{hcu?.isim} o mesafeden şut — etkisiz!");
                        TopKaptir(SahaBolgesi.MOS);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu!.KisaPasAt(sav!);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} tehlikeli ara pası!"); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else
                    {
                        basarili = hcu!.KisaPasAt(sav!);
                        if (basarili) { topunYeri = SahaBolgesi.OOS; SpikerMesajYayinla?.Invoke($"{hcu.isim} dikine pasla hücum!"); }
                        else TopKaptir(SahaBolgesi.MOS);
                    }
                    break;

                case SahaBolgesi.OOS:
                    hcu = hT.OOS; sav = sT.DOS;
                    if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        basarili = hcu!.KisaPasAt(sav!);
                        if (basarili) { topunYeri = SahaBolgesi.F; SpikerMesajYayinla?.Invoke($"{hcu.isim} şahane ara pası!"); }
                        else TopKaptir(SahaBolgesi.D);
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Şut Çek") || SonSecilenHucumHamlesi.Contains("GOL VURUŞU"))
                    {
                        if (((OrtaSaha)hcu!).UzaktanSutAt(sT.Kaleci!))
                            GolOldu(hcu.isim);
                        else
                        {
                            topunYeri = SahaBolgesi.K;
                            topEvSahibindeMi = !topEvSahibindeMi;
                            SpikerMesajYayinla?.Invoke("Müthiş şut ama kaleci kurtardı!");
                        }
                    }
                    break;

                case SahaBolgesi.F:
                    hcu = hT.Forvet;
                    sav = sT.Kaleci;
                    Random rnd2 = new Random();
                    int zar = rnd2.Next(1, 101); // 1-100 arası şans faktörü

                    if (SonSecilenHucumHamlesi.Contains("Gol Vuruşu") || SonSecilenHucumHamlesi.Contains("Şut Çek"))
                    {
                        if (sT.Kaleci!.KurtarisYap(hT.Forvet!))
                        {
                            // --- ŞUT KURTARILDI: İHTİMALLER BAŞLIYOR ---
                            if (zar <= 40)
                            {
                                // SENARYO A: Kaleci topu kontrol etti (%40)
                                TopKaptir(SahaBolgesi.K);
                                SpikerMesajYayinla?.Invoke($"{sT.Kaleci.isim} topu iki hamlede kontrol etti.");
                            }
                            else if (zar <= 70)
                            {
                                // SENARYO B: Savunma uzaklaştırdı (%30)
                                // Topu senin istediğin gibi Defans (D) bölgesine atıyoruz
                                TopKaptir(SahaBolgesi.D);
                                SpikerMesajYayinla?.Invoke("Savunma topu tehlike bölgesinin dışına vurdu!");
                            }
                            else
                            {
                                // SENARYO C: Dönen Top / Rebound (%30)
                                // Top sende kalıyor ama bir tık geriye (OOS) sekiyor
                                topunYeri = SahaBolgesi.OOS;
                                SpikerMesajYayinla?.Invoke("Kaleciden seken top! Tehlike hala geçmedi!");
                            }
                        }
                        else
                        {
                            GolOldu(hcu!.isim);
                        }
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Çalım At"))
                    {
                        if (hcu!.CalimAt(sav!))
                        {
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} kaleciyi geçti!");
                            GolOldu(hcu.isim);
                        }
                        else
                        {
                            // Çalım başarısızsa kaleci topu alır
                            TopKaptir(SahaBolgesi.K);
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} kaleciyi geçemedi, top kalecide.");
                        }
                    }
                    else if (SonSecilenHucumHamlesi.Contains("Ara Pası"))
                    {
                        if (hcu!.KisaPasAt(sT.Defans!))
                        {
                            topunYeri = SahaBolgesi.OOS;
                            SpikerMesajYayinla?.Invoke($"{hcu.isim} arkadaşını gördü, hücum tazeleniyor.");
                        }
                        else
                        {
                            TopKaptir(SahaBolgesi.D);
                            SpikerMesajYayinla?.Invoke("Pas arası! Savunma topu kazandı.");
                        }
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
        //  KOORDİNAT SİSTEMİ — Yüzdelik, ekran bağımsız
        // =====================================================
        public void HedefleriBelirle()
        {
            // topX ve topY artık 0.0–1.0 aralığında oran olarak saklanır
            // SahaCizici bunu gerçek piksel koordinatına çevirir
            Random rnd = new();
            double xOran = topunYeri switch
            {
                SahaBolgesi.K => topEvSahibindeMi ? 0.05 : 0.95,
                SahaBolgesi.D => topEvSahibindeMi ? 0.20 : 0.80,
                SahaBolgesi.DOS => topEvSahibindeMi ? 0.35 : 0.65,
                SahaBolgesi.MOS => 0.50,
                SahaBolgesi.OOS => topEvSahibindeMi ? 0.65 : 0.35,
                SahaBolgesi.F => topEvSahibindeMi ? 0.85 : 0.15,
                _ => 0.50
            };
            // topX ve topY'yi artık oran olarak tut (0–1000 scale)
            topX = (int)(xOran * 1000);
            topY = 500 + rnd.Next(-100, 100); // Y de oransal (0–1000)
        }
    }
}