namespace FutbolManagerMobil;

public partial class LoadingPage : ContentPage
{
    public LoadingPage()
    {
        InitializeComponent();
        StartLoading();
    }

    async void StartLoading()
    {
        // 1. Logoyu yavaşça büyüt ve görünür yap (Scale ve Fade)
        // Task.WhenAll kullanarak ikisini aynı anda başlatıyoruz
        await Task.WhenAll(
            imgLogo.FadeTo(1, 1500, Easing.CubicIn),      // 1.5 saniyede görünür ol
            imgLogo.ScaleTo(1.1, 1500, Easing.CubicOut)   // 1.5 saniyede %110 boyuta çık
        );

        // 2. Logo oturduktan sonra loader ve yazıyı göster
        await Task.WhenAll(
            loader.FadeTo(1, 500),
            lblDurum.FadeTo(1, 500)
        );

        // 3. Toplam 3 saniye dolunca SelectionPage'e uçalım
        await Task.Delay(1000);

        // Geçiş efektiyle sayfayı değiştir
        Application.Current.MainPage = new NavigationPage(new SelectionPage());
    }
}