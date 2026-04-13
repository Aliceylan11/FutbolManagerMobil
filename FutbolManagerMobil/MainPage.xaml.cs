using System.Threading.Tasks;

namespace FutbolManagerMobil;

public partial class MainPage : ContentPage
{
    OyunMotoru motor;
    SahaCizici mobilCizici;

    public MainPage(int evId, string evAd, int depId, string depAd,
                    string evHocaAdi, string depHocaAdi,
                    OyunMotoru.OyunModu secilenMod,
                    OyunMotoru.MacTipi secilenTip)
    {
        InitializeComponent();

        motor = new OyunMotoru();
        motor.evSahibi = Veritabani.TakimCek(evId, evAd);
        motor.deplasman = Veritabani.TakimCek(depId, depAd);

        motor.EvHocaAdi = evHocaAdi;
        motor.DepHocaAdi = depHocaAdi;
        motor.SecilenMod = secilenMod;
        motor.SecilenTip = secilenTip;

        motor.ModUygula();

        // Efsane boost bildirimi
        if (secilenMod == OyunMotoru.OyunModu.Efsane && motor.EfsaneBoostAldilar.Count > 0)
        {
            Dispatcher.DispatchAsync(async () =>
            {
                await Task.Delay(500);
                await DisplayAlert("⚡ Efsane Boost!",
                    $"{evAd}: {string.Join(", ", motor.EfsaneBoostAldilar)} efsane güce kavuştu!",
                    "Harika!");
            });
        }

        mobilCizici = new SahaCizici { Motor = motor };
        sahaGrafik.Drawable = mobilCizici;

        motor.topunYeri = OyunMotoru.SahaBolgesi.MOS;
        motor.topEvSahibindeMi = true;

        lblSkor.Text = $"{motor.evSahibi.Isim} 0 - 0 {motor.deplasman.Isim}";
        lblDakika.Text = "1:00";
        lblEvTakimAd.Text = motor.evSahibi.Isim.Substring(0, 3).ToUpper();
        lblDepTakimAd.Text = motor.deplasman.Isim.Substring(0, 3).ToUpper();

        motor.SpikerMesajYayinla = (mesaj) =>
            MainThread.BeginInvokeOnMainThread(() => SpikerMesajEkle(mesaj));

        motor.GolOlduEkranaBildir = (_) =>
            MainThread.BeginInvokeOnMainThread(() =>
            {
                lblSkor.Text = $"{motor.evSahibiGol} - {motor.deplasmanGol}";
                sahaGrafik.Invalidate();
            });

        ButonlariGuncelle();
    }

    // =====================================================
    //  ANA HAMLE METODU
    // =====================================================
    private async void HamleYap(object sender, EventArgs e)
    {
        if (motor.macBittiMi) return;

        var btn = (Button)sender;

        if (motor.SavunmaBekleniyorMu) motor.SavunmaHamlesiYap(btn.Text);
        else motor.HucumHamlesiBaslat(btn.Text);

        // MacDurumu'na göre aksiyon al
        switch (motor.SonMacDurumu)
        {
            case OyunMotoru.MacDurumu.Uzatma1Basladi:
                await DisplayAlert("⏱️ Uzatma!",
                    "90 dakika bitti — beraberlik!\n1. UZATMA BAŞLIYOR (90–105)", "Devam!");
                break;

            case OyunMotoru.MacDurumu.Uzatma2Basladi:
                await DisplayAlert("⏱️ 2. Uzatma!",
                    "105 dakika da yetmedi!\n2. UZATMA BAŞLIYOR (105–120)", "Devam!");
                break;

            case OyunMotoru.MacDurumu.PenaltiBasladi:
                // Penaltı logu spiker ekranında zaten gösterildi (OyunMotoru bunu yaptı)
                TumButonlariKapat();
                sahaGrafik.Invalidate();
                await DisplayAlert("⚡ Seri Penaltı Sonucu!",
                    $"🏆 Kazanan: {motor.PenaltiKazanani}",
                    "Devam");
                // Kupa modunda istatistik sayfasına git (lig tablosu güncellenmez)
                await IstatistiklereGit();
                return;

            case OyunMotoru.MacDurumu.Bitti:
                TumButonlariKapat();
                motor.macBittiMi = true;
                lblDakika.Text = $"{motor.dakika}:00";
                lblSpiker.Text = "VE MAÇ BİTTİ! Hakem son düdüğü çalıyor.";
                sahaGrafik.Invalidate();

                string kazanan = motor.evSahibiGol > motor.deplasmanGol
                    ? motor.evSahibi.Isim
                    : motor.deplasmanGol > motor.evSahibiGol
                        ? motor.deplasman.Isim
                        : "Beraberlik";

                await DisplayAlert("Maç Bitti",
                    $"Sonuç: {motor.evSahibi.Isim} {motor.evSahibiGol} – {motor.deplasmanGol} {motor.deplasman.Isim}\n🏆 {kazanan}",
                    "İlerle");

                // Lig modunda → LeaguePage (sonuç kaydedilir)
                // Kupa modunda → StatistikPage (sadece istatistikler)
                if (motor.SecilenTip == OyunMotoru.MacTipi.Lig)
                    await LigSayfasinaGit();
                else
                    await IstatistiklereGit();
                return;
        }

        // Normal hamle UI güncellemesi
        lblSkor.Text = $"{motor.evSahibiGol} - {motor.deplasmanGol}";
        lblDakika.Text = $"{motor.dakika}:00";

        motor.HedefleriBelirle();
        sahaGrafik.Invalidate();
        ButonlariGuncelle();
    }

    // =====================================================
    //  NAVİGASYON YARDIMCILARI
    // =====================================================
    private void TumButonlariKapat()
    {
        btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = false;
        btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = false;
    }

    /// <summary>
    /// Lig modunda LeaguePage'e git — maç sonucu otomatik işlenir.
    /// </summary>
    private async Task LigSayfasinaGit()
    {
        await Navigation.PushAsync(new LeaguePage(
            motor.evSahibi.Isim, motor.evSahibiGol,
            motor.deplasman.Isim, motor.deplasmanGol
        ));
    }

    /// <summary>
    /// Kupa modunda veya penaltıdan sonra istatistik sayfasına git.
    /// </summary>
    private async Task IstatistiklereGit()
    {
        await Navigation.PushAsync(new StatistikPage(
            motor.evSahibi.Isim, motor.deplasman.Isim,
            motor.evSahibiGol, motor.deplasmanGol,
            motor.evSahibi.ToplamSut, motor.deplasman.ToplamSut,
            motor.evSahibi.ToplamPas, motor.deplasman.ToplamPas
        ));
    }

    // =====================================================
    //  SPİKER MESAJI
    // =====================================================
    private void SpikerMesajEkle(string mesaj)
    {
        var yeniMesaj = new Label
        {
            Text = $"• {motor.dakika}' {mesaj}",
            TextColor = Colors.DarkSlateGray,
            FontSize = 14,
            Margin = new Thickness(0, 5),
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.WordWrap
        };
        slSpiker.Children.Insert(0, yeniMesaj);
        scrSpiker.ScrollToAsync(0, 0, true);
    }

    // =====================================================
    //  BUTON YÖNETİMİ
    // =====================================================
    private void ButonlariGuncelle()
    {
        Takim hucumTakimi = motor.topEvSahibindeMi ? motor.evSahibi : motor.deplasman;
        Takim savunmaTakimi = motor.topEvSahibindeMi ? motor.deplasman : motor.evSahibi;

        Futbolcu aktif = null;
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: aktif = hucumTakimi.Kaleci; break;
            case OyunMotoru.SahaBolgesi.D: aktif = hucumTakimi.Defans; break;
            case OyunMotoru.SahaBolgesi.DOS: aktif = hucumTakimi.DOS; break;
            case OyunMotoru.SahaBolgesi.MOS: aktif = hucumTakimi.MOS; break;
            case OyunMotoru.SahaBolgesi.OOS: aktif = hucumTakimi.OOS; break;
            case OyunMotoru.SahaBolgesi.F: aktif = hucumTakimi.Forvet; break;
        }

        lblHucumBaslik.Text = $"{hucumTakimi.Isim.ToUpper()} ATAĞI";
        lblSavunmaBaslik.Text = $"{savunmaTakimi.Isim.ToUpper()} SAVUNMASI";
        lblAktifOyuncu.Text = aktif != null ? $"⚽ TOP: {aktif.isim.ToUpper()}" : "⚽ TOP BOŞTA";

        btnKisaPas.IsVisible = btnKisaPas.IsEnabled = false;
        btnAraPasi.IsVisible = btnAraPasi.IsEnabled = false;
        btnSutCek.IsVisible = btnSutCek.IsEnabled = false;
        btnPress.IsVisible = btnPress.IsEnabled = false;
        btnTopaKay.IsVisible = btnTopaKay.IsEnabled = false;
        btnMarkaj.IsVisible = btnMarkaj.IsEnabled = false;

        if (motor.SavunmaBekleniyorMu) SavunmaButonlariniHazirla();
        else HucumButonlariniHazirla();
    }

    private void HucumButonlariniHazirla()
    {
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K:
                btnKisaPas.Text = "Kısa Pas (Defans)"; btnAraPasi.Text = "Degaj (DOS)";
                btnKisaPas.IsVisible = btnKisaPas.IsEnabled = true;
                btnAraPasi.IsVisible = btnAraPasi.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.D:
                btnKisaPas.Text = "Kısa Pas (DOS)"; btnAraPasi.Text = "İleri Pas (MOS)"; btnSutCek.Text = "Geri Pas (Kaleci)";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.DOS:
                btnKisaPas.Text = "Geri Pas (Defans)"; btnAraPasi.Text = "Kısa Pas (OOS)"; btnSutCek.Text = "Çalım At";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.MOS:
                btnKisaPas.Text = "Dikine Pas (OOS)"; btnAraPasi.Text = "Kısa Pas (DOS)";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.OOS:
                btnKisaPas.Text = "Geri Pas (MOS)"; btnAraPasi.Text = "Ara Pası (Forvet)"; btnSutCek.Text = "Şut Çek";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.F:
                btnKisaPas.Text = "Ara Pası (OOS)"; btnAraPasi.Text = "Çalım At"; btnSutCek.Text = "GOL VURUŞU";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
        }
    }

    private void SavunmaButonlariniHazirla()
    {
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K:
                btnPress.Text = "Şutu Engelle";
                btnPress.IsVisible = btnPress.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.D:
                btnPress.Text = "Müdahale Et"; btnTopaKay.Text = "Topa Kay"; btnMarkaj.Text = "Topu Çal";
                btnPress.IsVisible = btnTopaKay.IsVisible = btnMarkaj.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.F:
                btnPress.Text = "Şutu Engelle (Kaleci)"; btnTopaKay.Text = "Müdahale Et"; btnMarkaj.Text = "Topa Kay";
                btnPress.IsVisible = btnTopaKay.IsVisible = btnMarkaj.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = true;
                break;
            default:
                btnPress.Text = "Markaj Yap"; btnTopaKay.Text = "Pas Arası Yap";
                btnPress.IsVisible = btnTopaKay.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = true;
                break;
        }
    }
}
