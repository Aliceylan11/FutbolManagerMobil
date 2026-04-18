using System.Threading.Tasks;

namespace FutbolManagerMobil;

public partial class MainPage : ContentPage
{
    OyunMotoru motor = new();
    SahaCizici? mobilCizici;

    private string _evHocaAdi = "";
    private string _depHocaAdi = "";

    // Buton → hamle string eşleşmesi (kısa ekran metni ↔ motor aksiyonu)
    // Her hamle çağrısında bu alanlar güncellenir
    private string _evBtn1Action = "";
    private string _evBtn2Action = "";
    private string _evBtn3Action = "";
    private string _depBtn1Action = "";
    private string _depBtn2Action = "";
    private string _depBtn3Action = "";

    public MainPage(int evId, string evAd, int depId, string depAd,
                    string evHocaAdi, string depHocaAdi,
                    OyunMotoru.OyunModu secilenMod,
                    OyunMotoru.MacTipi secilenTip)
    {
        InitializeComponent();

        _evHocaAdi = evHocaAdi;
        _depHocaAdi = depHocaAdi;

        motor.evSahibi = Veritabani.TakimCek(evId, evAd);
        motor.deplasman = Veritabani.TakimCek(depId, depAd);
        motor.EvHocaAdi = evHocaAdi;
        motor.DepHocaAdi = depHocaAdi;
        motor.SecilenMod = secilenMod;
        motor.SecilenTip = secilenTip;
        motor.ModUygula();

        // Efsane boost bildirimi
        if (secilenMod == OyunMotoru.OyunModu.Efsane)
        {
            Dispatcher.DispatchAsync(async () =>
            {
                await Task.Delay(600);
                if (motor.EvEfsaneBoostlar.Count > 0)
                    await DisplayAlert("⚡ Efsane Boost — Ev Sahibi",
                        $"{evAd}: {string.Join(", ", motor.EvEfsaneBoostlar)}\n+5 Hız · Teknik · Def · Bitiricilik · Güç", "Süper!");
                if (motor.DepEfsaneBoostlar.Count > 0)
                    await DisplayAlert("⚡ Efsane Boost — Deplasman",
                        $"{depAd}: {string.Join(", ", motor.DepEfsaneBoostlar)}\n+5 Hız · Teknik · Def · Bitiricilik · Güç", "Süper!");
            });
        }

        mobilCizici = new SahaCizici { Motor = motor };
        sahaGrafik.Drawable = mobilCizici;

        motor.topunYeri = OyunMotoru.SahaBolgesi.MOS;
        motor.topEvSahibindeMi = true;
        motor.IsSantra = true;

        // Sabit başlıklar: Ev daima sol, Dep daima sağ
        lblEvPanelBaslik.Text = motor.evSahibi.Isim.ToUpper();
        lblDepPanelBaslik.Text = motor.deplasman.Isim.ToUpper();
        lblEvTakimAd.Text = KisaIsim(motor.evSahibi.Isim);
        lblDepTakimAd.Text = KisaIsim(motor.deplasman.Isim);
        lblSkor.Text = "0 - 0";
        lblDakika.Text = "1'";

        motor.SpikerMesajYayinla = mesaj =>
            MainThread.BeginInvokeOnMainThread(() => SpikerMesajEkle(mesaj));

        motor.GolOlduEkranaBildir = _ =>
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblSkor.Text = $"{motor.evSahibiGol} - {motor.deplasmanGol}";
                sahaGrafik.Invalidate();
            });

        ButonlariGuncelle();
    }

    private static string KisaIsim(string s) =>
        s.Length >= 3 ? s[..3].ToUpper() : s.ToUpper();

    // ─────────────────────────────────────────────────────
    //  PUAN DURUMU
    // ─────────────────────────────────────────────────────
    private async void OnPuanDurumuClicked(object sender, EventArgs e)
        => await Navigation.PushAsync(new LeaguePage());

    // ─────────────────────────────────────────────────────
    //  PENALTİ YÖN SEÇİMİ
    // ─────────────────────────────────────────────────────
    private async void OnPenaltiYonClicked(object sender, EventArgs e)
    {
        if (!motor.PenaltiAsamasindaMi) return;
        var btn = (Button)sender;
        string yon = btn.Text.Contains("SOL") ? "SOL" : btn.Text.Contains("ORTA") ? "ORTA" : "SAĞ";
        SpikerMesajEkle($"🎯 {motor.SiradakiAtanTakim} {yon} yönüne atıyor...");
        bool bitti = motor.PenaltiYonuIsle(yon);
        lblPenaltiDurum.Text = $"Skor: {motor.PenaltiEvGol}-{motor.PenaltiDepGol}  |  " +
                               (motor.PenaltiAniOlum ? "⚠️ ANİ ÖLÜM" : $"Tur {motor.PenaltiTur + 1}/5");
        if (!bitti)
            lblPenaltiBaslik.Text = $"⚡ {motor.SiradakiAtanTakim.ToUpper()} ATIYOR";
        else
        {
            pnlPenalti.IsVisible = false;
            pnlNormalHamle.IsVisible = true;
            sahaGrafik.Invalidate();
            await DisplayAlert("⚡ Penaltı Sonucu!",
                string.IsNullOrEmpty(motor.PenaltiSonMesaj)
                    ? $"Kazanan: {motor.PenaltiKazanani}" : motor.PenaltiSonMesaj,
                "Devam");
            await IstatistiklereGit();
        }
    }

    // ─────────────────────────────────────────────────────
    //  EV SAHİBİ BUTONU — sol panel
    // ─────────────────────────────────────────────────────
    private async void HamleEvClicked(object sender, EventArgs e)
    {
        if (motor.macBittiMi) return;
        var btn = (Button)sender;

        // Hangi aksiyonu çalıştıracağız?
        string aksiyon = btn == btnEv1 ? _evBtn1Action
                       : btn == btnEv2 ? _evBtn2Action
                       : _evBtn3Action;
        if (string.IsNullOrEmpty(aksiyon)) return;

        await IsleMacHamlesi(aksiyon, evTarafHamle: true);
    }

    // ─────────────────────────────────────────────────────
    //  DEPLASMAN BUTONU — sağ panel
    // ─────────────────────────────────────────────────────
    private async void HamleDepClicked(object sender, EventArgs e)
    {
        if (motor.macBittiMi) return;
        var btn = (Button)sender;

        string aksiyon = btn == btnDep1 ? _depBtn1Action
                       : btn == btnDep2 ? _depBtn2Action
                       : _depBtn3Action;
        if (string.IsNullOrEmpty(aksiyon)) return;

        await IsleMacHamlesi(aksiyon, evTarafHamle: false);
    }

    // ─────────────────────────────────────────────────────
    //  ORTAK HAM​LE İŞLEME
    // ─────────────────────────────────────────────────────
    private async Task IsleMacHamlesi(string aksiyon, bool evTarafHamle)
    {
        if (motor.SavunmaBekleniyorMu)
        {
            // Savunma sırası: aksiyon savunma tarafındaki butona ait olmalı
            bool savunmaTarafEv = !motor.topEvSahibindeMi; // top depda → ev savunur
            if (savunmaTarafEv != evTarafHamle) return;    // yanlış panel, dikkate alma
            motor.SavunmaHamlesiYap(aksiyon);
        }
        else
        {
            // Hücum sırası: hücum yapan takımın paneline ait olmalı
            if (motor.topEvSahibindeMi != evTarafHamle) return;
            motor.HucumHamlesiBaslat(aksiyon);
        }

        await KontrolVeGuncelle();
    }

    private async Task KontrolVeGuncelle()
    {
        switch (motor.SonMacDurumu)
        {
            case OyunMotoru.MacDurumu.Uzatma1Basladi:
                await DisplayAlert("⏱️ 1. Uzatma!", "90 dk bitti — beraberlik!\n1. UZATMA (90-105)", "Devam!");
                break;

            case OyunMotoru.MacDurumu.Uzatma2Basladi:
                await DisplayAlert("⏱️ 2. Uzatma!", "105 dk yetmedi!\n2. UZATMA (105-120)", "Devam!");
                break;

            case OyunMotoru.MacDurumu.PenaltiBasladi:
                TumButonlariKapat();
                pnlNormalHamle.IsVisible = false;
                pnlPenalti.IsVisible = true;
                lblPenaltiBaslik.Text = $"⚡ {motor.SiradakiAtanTakim.ToUpper()} ATIYOR";
                lblPenaltiDurum.Text = "Skor: 0-0  |  Tur 1/5";
                return;

            case OyunMotoru.MacDurumu.Bitti:
                TumButonlariKapat();
                motor.macBittiMi = true;
                lblDakika.Text = $"{motor.dakika}'";
                sahaGrafik.Invalidate();

                string kazanan = motor.evSahibiGol > motor.deplasmanGol
                    ? motor.evSahibi.Isim
                    : motor.deplasmanGol > motor.evSahibiGol
                        ? motor.deplasman.Isim : "Beraberlik";

                await DisplayAlert("Maç Bitti",
                    $"{motor.evSahibi.Isim}  {motor.evSahibiGol} – {motor.deplasmanGol}  {motor.deplasman.Isim}\n🏆 {kazanan}",
                    "İlerle");

                if (motor.SecilenTip == OyunMotoru.MacTipi.Lig)
                    await LigSayfasinaGit();
                else
                    await IstatistiklereGit();
                return;
        }

        lblSkor.Text = $"{motor.evSahibiGol} - {motor.deplasmanGol}";
        lblDakika.Text = $"{motor.dakika}'";
        motor.HedefleriBelirle();
        sahaGrafik.Invalidate();
        ButonlariGuncelle();
    }

    // ─────────────────────────────────────────────────────
    //  NAVİGASYON
    // ─────────────────────────────────────────────────────
    private void TumButonlariKapat()
    {
        btnEv1.IsEnabled = btnEv2.IsEnabled = btnEv3.IsEnabled = false;
        btnDep1.IsEnabled = btnDep2.IsEnabled = btnDep3.IsEnabled = false;
    }

    private async Task LigSayfasinaGit() =>
        await Navigation.PushAsync(new LeaguePage(
            motor.evSahibi.Isim, motor.evSahibiGol,
            motor.deplasman.Isim, motor.deplasmanGol,
            _evHocaAdi, _depHocaAdi));

    private async Task IstatistiklereGit() =>
        await Navigation.PushAsync(new StatistikPage(
            motor.evSahibi.Isim, motor.deplasman.Isim,
            motor.evSahibiGol, motor.deplasmanGol,
            motor.evSahibi.ToplamSut, motor.deplasman.ToplamSut,
            motor.evSahibi.ToplamPas, motor.deplasman.ToplamPas));

    // ─────────────────────────────────────────────────────
    //  SPİKER
    // ─────────────────────────────────────────────────────
    private void SpikerMesajEkle(string mesaj)
    {
        slSpiker.Children.Insert(0, new Label
        {
            Text = $"• {motor.dakika}' {mesaj}",
            TextColor = Colors.White,
            FontSize = 15,
            FontAttributes = FontAttributes.Bold,
            Margin = new Thickness(0, 3),
            LineBreakMode = LineBreakMode.WordWrap
        });
        scrSpiker.ScrollToAsync(0, 0, true);
    }

    // ─────────────────────────────────────────────────────
    //  BUTON YÖNETİMİ — Ev Sahibi Sol, Deplasman Sağ
    //  SIRA TABALI GİZLEME: Sadece hamle yapacak takımın
    //  paneli görünür; diğer panel tamamen gizlenir.
    // ─────────────────────────────────────────────────────
    private void ButonlariGuncelle()
    {
        // Sıra kimin?
        //   Savunma bekleniyor → top sahibinin rakibi savunur → rakip sırası
        //   Normal hücum       → top sahibi saldırır         → top sahibi sırası
        bool evSira = motor.SavunmaBekleniyorMu
            ? !motor.topEvSahibindeMi
            : motor.topEvSahibindeMi;

        Takim hucumTakimi = motor.topEvSahibindeMi ? motor.evSahibi : motor.deplasman;
        Takim savunmaTakimi = motor.topEvSahibindeMi ? motor.deplasman : motor.evSahibi;

        Futbolcu? hucumOyuncu = null;
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: hucumOyuncu = hucumTakimi.Kaleci; break;
            case OyunMotoru.SahaBolgesi.D: hucumOyuncu = hucumTakimi.Defans; break;
            case OyunMotoru.SahaBolgesi.DOS: hucumOyuncu = hucumTakimi.DOS; break;
            case OyunMotoru.SahaBolgesi.MOS: hucumOyuncu = hucumTakimi.MOS; break;
            case OyunMotoru.SahaBolgesi.OOS: hucumOyuncu = hucumTakimi.OOS; break;
            case OyunMotoru.SahaBolgesi.F: hucumOyuncu = hucumTakimi.Forvet; break;
        }

        Futbolcu? savunmaOyuncu = null;
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: savunmaOyuncu = savunmaTakimi.Forvet; break;
            case OyunMotoru.SahaBolgesi.D: savunmaOyuncu = savunmaTakimi.OOS; break;
            case OyunMotoru.SahaBolgesi.DOS: savunmaOyuncu = savunmaTakimi.MOS; break;
            case OyunMotoru.SahaBolgesi.MOS: savunmaOyuncu = savunmaTakimi.DOS; break;
            case OyunMotoru.SahaBolgesi.OOS: savunmaOyuncu = savunmaTakimi.DOS; break;
            case OyunMotoru.SahaBolgesi.F: savunmaOyuncu = savunmaTakimi.Kaleci; break;
        }

        var (hucumBtn1, hucumBtn2, hucumBtn3) = HucumAksiyon();
        var (savBtn1, savBtn2, savBtn3) = SavunmaAksiyon();

        if (motor.topEvSahibindeMi)
        {
            AyarlaEvPanel(hucumBtn1, hucumBtn2, hucumBtn3,
                          atar: true, oyuncu: hucumOyuncu, gorunur: evSira);
            AyarlaDepPanel(savBtn1, savBtn2, savBtn3,
                           atar: false, oyuncu: savunmaOyuncu, gorunur: !evSira);
        }
        else
        {
            AyarlaEvPanel(savBtn1, savBtn2, savBtn3,
                          atar: false, oyuncu: savunmaOyuncu, gorunur: evSira);
            AyarlaDepPanel(hucumBtn1, hucumBtn2, hucumBtn3,
                           atar: true, oyuncu: hucumOyuncu, gorunur: !evSira);
        }
    }

    // ── EV PANELİ AYARLA ──
    private void AyarlaEvPanel(
        (string label, string action, Color color)? s1,
        (string label, string action, Color color)? s2,
        (string label, string action, Color color)? s3,
        bool atar, Futbolcu? oyuncu, bool gorunur)
    {
        if (!gorunur)
        {
            btnEv1.IsVisible = btnEv2.IsVisible = btnEv3.IsVisible = false;
            btnEv1.IsEnabled = btnEv2.IsEnabled = btnEv3.IsEnabled = false;
            lblEvOyuncu.Text = "⏳ Bekleniyor...";
            _evBtn1Action = _evBtn2Action = _evBtn3Action = "";
            return;
        }
        string emoji = atar ? "⚽" : "🛡️";
        lblEvOyuncu.Text = oyuncu != null ? $"{emoji} {oyuncu.isim.ToUpper()}" : $"{emoji} —";
        AyarlaBtn(btnEv1, s1, ref _evBtn1Action);
        AyarlaBtn(btnEv2, s2, ref _evBtn2Action);
        AyarlaBtn(btnEv3, s3, ref _evBtn3Action);
    }

    // ── DEP PANELİ AYARLA ──
    private void AyarlaDepPanel(
        (string label, string action, Color color)? s1,
        (string label, string action, Color color)? s2,
        (string label, string action, Color color)? s3,
        bool atar, Futbolcu? oyuncu, bool gorunur)
    {
        if (!gorunur)
        {
            btnDep1.IsVisible = btnDep2.IsVisible = btnDep3.IsVisible = false;
            btnDep1.IsEnabled = btnDep2.IsEnabled = btnDep3.IsEnabled = false;
            lblDepOyuncu.Text = "⏳ Bekleniyor...";
            _depBtn1Action = _depBtn2Action = _depBtn3Action = "";
            return;
        }
        string emoji = atar ? "⚽" : "🛡️";
        lblDepOyuncu.Text = oyuncu != null ? $"{emoji} {oyuncu.isim.ToUpper()}" : $"{emoji} —";
        AyarlaBtn(btnDep1, s1, ref _depBtn1Action);
        AyarlaBtn(btnDep2, s2, ref _depBtn2Action);
        AyarlaBtn(btnDep3, s3, ref _depBtn3Action);
    }

    private static void AyarlaBtn(Button btn,
        (string label, string action, Color color)? slot, ref string actionField)
    {
        if (slot is null)
        {
            btn.IsVisible = btn.IsEnabled = false;
            actionField = "";
            return;
        }
        btn.Text = slot.Value.label;
        btn.BackgroundColor = slot.Value.color;
        btn.IsVisible = true;
        btn.IsEnabled = true;
        actionField = slot.Value.action;
    }


    // ── HÜCUM AKSİYONLARI (topunYeri'ne göre) ──
    private (
        (string, string, Color)?,
        (string, string, Color)?,
        (string, string, Color)?
    ) HucumAksiyon()
    {
        var G = Color.FromArgb("#2A5A2A"); // Yeşil — pas
        var B = Color.FromArgb("#1A3A5A"); // Mavi  — ara pas / özel
        var R = Color.FromArgb("#8B1A1A"); // Kırmızı — şut / çalım

        // Santra kontrolü
        if (motor.IsSantra && motor.topunYeri == OyunMotoru.SahaBolgesi.MOS)
            return (("Kısa Pas", "Kısa Pas (DOS)", G),
                    ("Geri Pas", "Geri Pas (Defans)", B),
                    null);

        return motor.topunYeri switch
        {
            OyunMotoru.SahaBolgesi.K =>
                (("Kısa Pas", "Kısa Pas (Defans)", G),
                 ("Degaj", "Degaj (DOS)", B),
                 null),

            OyunMotoru.SahaBolgesi.D =>
                (("Kısa Pas", "Kısa Pas (DOS)", G),
                 ("İleri Pas", "İleri Pas (MOS)", B),
                 ("Geri Pas", "Geri Pas (Kaleci)", Color.FromArgb("#555555"))),

            OyunMotoru.SahaBolgesi.DOS =>
                (("Geri Pas", "Geri Pas (Defans)", B),
                 ("Kısa Pas", "Kısa Pas (OOS)", G),
                 ("Çalım At", "Çalım At", R)),

            OyunMotoru.SahaBolgesi.MOS =>
                (("Dikine", "Dikine Pas (OOS)", G),
                 ("Geri Pas", "Kısa Pas (DOS)", B),
                 null),

            OyunMotoru.SahaBolgesi.OOS =>
                (("Geri Pas", "Geri Pas (MOS)", B),
                 ("Ara Pası", "Ara Pası (Forvet)", G),
                 ("Şut Çek", "Şut Çek", R)),

            OyunMotoru.SahaBolgesi.F =>
                (("Geri Çıkar", "Ara Pası (OOS)", B),
                 ("Çalım At", "Çalım At", Color.FromArgb("#6A4A1A")),
                 ("Gol Vuruşu", "Gol Vuruşu", R)),

            _ => (null, null, null)
        };
    }

    // ── SAVUNMA AKSİYONLARI (topunYeri'ne göre) ──
    private (
        (string, string, Color)?,
        (string, string, Color)?,
        (string, string, Color)?
    ) SavunmaAksiyon()
    {
        var GRI = Color.FromArgb("#2A2A3A");
        var GRI2 = Color.FromArgb("#1A2A2A");
        var GRI3 = Color.FromArgb("#2A1A2A");

        return motor.topunYeri switch
        {
            OyunMotoru.SahaBolgesi.K =>
                (("Engelle", "Şutu Engelle", GRI), null, null),

            OyunMotoru.SahaBolgesi.D =>
                (("Müdahale", "Müdahale Et", GRI),
                 ("Topa Kay", "Topa Kay", GRI2),
                 ("Topu Çal", "Topu Çal", GRI3)),

            OyunMotoru.SahaBolgesi.F =>
                (("Engelle", "Şutu Engelle (Kaleci)", GRI),
                 ("Müdahale", "Müdahale Et", GRI2),
                 ("Topa Kay", "Topa Kay", GRI3)),

            _ =>
                (("Markaj", "Markaj Yap", GRI),
                 ("Pas Arası", "Pas Arası Yap", GRI2),
                 null)
        };
    }
}