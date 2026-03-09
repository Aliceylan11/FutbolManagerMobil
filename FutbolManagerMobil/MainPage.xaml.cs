using System.Threading.Tasks;

namespace FutbolManagerMobil;

public partial class MainPage : ContentPage
{
    OyunMotoru motor;
    SahaCizici mobilCizici;

    public MainPage(int evId, string evAd, int depId, string depAd)
    {
        InitializeComponent();

        motor = new OyunMotoru();

        // 1. Kadroları ve Verileri Yükle
        motor.evSahibi = Veritabani.TakimCek(evId, evAd);
        motor.deplasman = Veritabani.TakimCek(depId, depAd); 

        // 2. Görsel Çiziciyi Hazırla
        mobilCizici = new SahaCizici { Motor = motor };
        sahaGrafik.Drawable = mobilCizici;

        motor.topunYeri = OyunMotoru.SahaBolgesi.MOS;
        motor.topEvSahibindeMi = true;
        // 3. Başlangıç Değerleri
        lblSkor.Text = $"{motor.evSahibi.Isim} 0 - 0 {motor.deplasman.Isim}";
        lblDakika.Text = "1:00";
        lblEvTakimAd.Text = motor.evSahibi.Isim.Substring(0, 3).ToUpper();
        lblDepTakimAd.Text = motor.deplasman.Isim.Substring(0, 3).ToUpper();
        motor.SpikerMesajYayinla = (mesaj) =>
        {
            // UI güncellemelerini ana thread üzerinde yapmalıyız
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SpikerMesajEkle(mesaj);
            });
        };
        ButonlariGuncelle();
    }

    /// <summary>
    /// XAML'daki tüm butonların (Hücum ve Savunma) bağlı olduğu ana metot.
    /// </summary>
    // "Task" yerine "void" yazdık, hata kökünden çözüldü!
    private async void HamleYap(object sender, EventArgs e)
    {
        if (motor.macBittiMi) return; // Maç bittiyse butona basılmasın

        var btn = (Button)sender;

        if (motor.SavunmaBekleniyorMu) motor.SavunmaHamlesiYap(btn.Text);
        else motor.HucumHamlesiBaslat(btn.Text);

        // --- MAÇ BİTİŞ KONTROLÜ ---
        if (motor.dakika >= 90)
        {
            motor.macBittiMi = true;
            lblDakika.Text = "90:00";
            lblSpiker.Text = "VE MAÇ BİTTİ! Hakem son düdüğü çalıyor.";

            // Tüm butonları kapat (btnMarkaj da eklendi tam kapansın diye)
            btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = false;
            btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = false;

            await DisplayAlert("Maç Bitti", $"Maç Sonucu: {motor.evSahibiGol} - {motor.deplasmanGol}", "İstatistiklere Git");

            // Motorun içindeki gerçek verileri 3. sayfaya gönderiyoruz
            await Navigation.PushAsync(new StatistikPage(
                motor.evSahibi.Isim,
                motor.deplasman.Isim,
                motor.evSahibiGol,
                motor.deplasmanGol,
                motor.evSahibi.ToplamSut,
                motor.deplasman.ToplamSut,
                motor.evSahibi.ToplamPas,
                motor.deplasman.ToplamPas
            ));

            return; // Maç bittiyse aşağıdaki kodları okumayı bırakır
        }

        // Tabelayı ve Görseli Güncelle
        // DÜZELTME: Takım isimleri zaten köşelerde var, ortaya sadece net skoru yazıyoruz
        lblSkor.Text = $"{motor.evSahibiGol} - {motor.deplasmanGol}";
        lblDakika.Text = $"{motor.dakika}:00";

        motor.HedefleriBelirle(); // Topun koordinatlarını yeni bölgeye göre güncelle
        sahaGrafik.Invalidate(); // Sahayı tekrar çizdir
        ButonlariGuncelle();
    }

    private void SpikerMesajEkle(string mesaj)
    {
        // 1. Önce mesaj etiketini (Label) oluşturuyoruz
        var yeniMesaj = new Label
        {
            Text = $"• {motor.dakika}' {mesaj}",
            TextColor = Colors.DarkSlateGray,
            FontSize = 14, // Spiker daraldığı için yazıyı 16'dan 14'e indirmekte fayda var
            Margin = new Thickness(0, 5),
            FontAttributes = FontAttributes.Bold,
            LineBreakMode = LineBreakMode.WordWrap // YENİ EKLENEN HAYAT KURTARAN SATIR
        };

        // 2. Mesajı listenin en başına (0. indekse) ekliyoruz
        // Not: Sadece bir kez eklemelisin, yoksa uygulama hata verir.
        slSpiker.Children.Insert(0, yeniMesaj);

        // 3. Otomatik olarak en yukarı kaydır (en güncel mesaj görünsün)
        scrSpiker.ScrollToAsync(0, 0, true);
    }

    private void ButonlariGuncelle()
    {
        // 1. Atak ve Savunma Takımlarını Belirle
        Takim hucumTakimi = motor.topEvSahibindeMi ? motor.evSahibi : motor.deplasman;
        Takim savunmaTakimi = motor.topEvSahibindeMi ? motor.deplasman : motor.evSahibi;

        Futbolcu aktif = null;

        // 6-6 Sistemine göre topun olduğu bölgedeki oyuncuyu bul
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: aktif = hucumTakimi.Kaleci; break;
            case OyunMotoru.SahaBolgesi.D: aktif = hucumTakimi.Defans; break;
            case OyunMotoru.SahaBolgesi.DOS: aktif = hucumTakimi.DOS; break;
            case OyunMotoru.SahaBolgesi.MOS: aktif = hucumTakimi.MOS; break;
            case OyunMotoru.SahaBolgesi.OOS: aktif = hucumTakimi.OOS; break;
            case OyunMotoru.SahaBolgesi.F: aktif = hucumTakimi.Forvet; break;
        }

        // 2. Etiketleri Güncelle (Tek Seferde)
        lblHucumBaslik.Text = $"{hucumTakimi.Isim.ToUpper()} ATAĞI";
        lblSavunmaBaslik.Text = $"{savunmaTakimi.Isim.ToUpper()} SAVUNMASI";

        if (aktif != null)
            lblAktifOyuncu.Text = $"⚽ TOP: {aktif.isim.ToUpper()}";
        else
            lblAktifOyuncu.Text = "⚽ TOP BOŞTA";

        // 3. Tüm Butonları Başlangıçta Kapat (Temizleme)
        // Bu sayede her seferinde tek tek kapatmakla uğraşmayız
        btnKisaPas.IsVisible = btnKisaPas.IsEnabled = false;
        btnAraPasi.IsVisible = btnAraPasi.IsEnabled = false;
        btnSutCek.IsVisible = btnSutCek.IsEnabled = false;
        btnPress.IsVisible = btnPress.IsEnabled = false;
        btnTopaKay.IsVisible = btnTopaKay.IsEnabled = false;
        btnMarkaj.IsVisible = btnMarkaj.IsEnabled = false;

        // 4. Sıra Kimdeyse Sadece O Paneli Hazırla
        if (motor.SavunmaBekleniyorMu)
        {
            SavunmaButonlariniHazirla();
        }
        else
        {
            HucumButonlariniHazirla();
        }
    }

    private void HucumButonlariniHazirla()
    {
        // Senin verdiğin tabloya göre Hücum butonlarını isimlendiriyoruz
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: // KALECİ
                btnKisaPas.Text = "Kısa Pas (Defans)";
                btnAraPasi.Text = "Degaj (DOS)";
                btnKisaPas.IsVisible = btnKisaPas.IsEnabled = true;
                btnAraPasi.IsVisible = btnAraPasi.IsEnabled = true;
                break;

            case OyunMotoru.SahaBolgesi.D: // DEFANS
                btnKisaPas.Text = "Kısa Pas (DOS)";
                btnAraPasi.Text = "İleri Pas (MOS)";
                btnSutCek.Text = "Geri Pas (Kaleci)"; // %5 Riskli!
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;

            case OyunMotoru.SahaBolgesi.DOS: // DOS
                btnKisaPas.Text = "Geri Pas (Defans)";
                btnAraPasi.Text = "Kısa Pas (OOS)";
                btnSutCek.Text = "Çalım At";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.MOS:
                btnKisaPas.Text = "Dikine Pas (OOS)";
                btnAraPasi.Text = "Kısa Pas (DOS)";
                //btnSutCek.Text = "Oyun Kur";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
            case OyunMotoru.SahaBolgesi.OOS: // OOS
                btnKisaPas.Text = "Geri Pas (MOS)";
                btnAraPasi.Text = "Ara Pası (Forvet)";
                btnSutCek.Text = "Şut Çek"; // Veya Çalım At
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;

            case OyunMotoru.SahaBolgesi.F: // FORVET
                btnKisaPas.Text = "Ara Pası (OOS)";
                btnAraPasi.Text = "Çalım At";
                btnSutCek.Text = "GOL VURUŞU";
                btnKisaPas.IsVisible = btnAraPasi.IsVisible = btnSutCek.IsVisible = true;
                btnKisaPas.IsEnabled = btnAraPasi.IsEnabled = btnSutCek.IsEnabled = true;
                break;
        }
    }

    private void SavunmaButonlariniHazirla()
    {
        // Rakibin topu olduğu yere göre savunma butonlarını hazırlıyoruz
        switch (motor.topunYeri)
        {
            case OyunMotoru.SahaBolgesi.K: // Kaleci hücum yaparken
                if (motor.topunYeri == OyunMotoru.SahaBolgesi.K2)
                {
                    // PENALTI DURUMU: Sadece "Penaltıyı Kurtar" aktif olur
                    btnPress.IsVisible = btnPress.IsEnabled = false; // Şutu Engelle gizlenir

                    btnTopaKay.Text = "Penaltıyı Kurtar";
                    btnTopaKay.IsVisible = btnTopaKay.IsEnabled = true;
                }
                else
                {
                    // NORMAL ŞUT DURUMU: Sadece "Şutu Engelle" aktif olur
                    btnPress.Text = "Şutu Engelle";
                    btnPress.IsVisible = btnPress.IsEnabled = true;

                    btnTopaKay.IsVisible = btnTopaKay.IsEnabled = false; // Penaltı butonu gizlenir
                }
                break;

            case OyunMotoru.SahaBolgesi.D: // Rakip defanstayken
                btnPress.Text = "Müdahale Et";
                btnTopaKay.Text = "Topa Kay";
                btnMarkaj.Text = "Topu Çal";
                btnPress.IsVisible = btnTopaKay.IsVisible = btnMarkaj.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = true;
                break;

            case OyunMotoru.SahaBolgesi.F: // Rakip forvetteyken
                btnPress.Text = "Şutu Engelle (Kaleci)"; // Kaleci KurtarisYap() tetikler
                btnTopaKay.Text = "Müdahale Et";
                btnMarkaj.Text = "Topa Kay";
                btnPress.IsVisible = btnTopaKay.IsVisible = btnMarkaj.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = btnMarkaj.IsEnabled = true;
                break;

            default: // Genel Orta Saha Savunması
                btnPress.Text = "Markaj Yap";
                btnTopaKay.Text = "Pas Arası Yap";
                btnPress.IsVisible = btnTopaKay.IsVisible = true;
                btnPress.IsEnabled = btnTopaKay.IsEnabled = true;
                break;
        }
    }
}