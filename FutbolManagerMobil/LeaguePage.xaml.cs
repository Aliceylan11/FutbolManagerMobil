using System;
using Microsoft.Maui.Controls;

namespace FutbolManagerMobil;

public partial class LeaguePage : ContentPage
{
    // Sayfayı açarken son maçın sonucunu da alıp tabloya işliyoruz
    public LeaguePage(string evTakim, int evGol, string depTakim, int depGol)
    {
        InitializeComponent();

        // 1. Maç sonucunu kalıcı tabloya işle
        LigManager.MacSonucunuIsle(evTakim, evGol, depTakim, depGol);

        // 2. Güncel tabloyu ekrana çiz
        TabloYenile();
    }

    // Sıfırla ekranından gelirken sadece tabloyu göster (sonuç işleme)
    public LeaguePage()
    {
        InitializeComponent();
        TabloYenile();
    }

    // -----------------------------------------------
    //  TABLO SATIRI ÇİZ
    // -----------------------------------------------
    private void TabloYenile()
    {
        slTablo.Children.Clear();

        var siraliTablo = LigManager.SiraliTabloGetir();

        if (siraliTablo.Count == 0)
        {
            slTablo.Children.Add(new Label
            {
                Text = "Henüz lig maçı oynanmadı.",
                TextColor = Colors.Gray,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 20)
            });
            return;
        }

        for (int i = 0; i < siraliTablo.Count; i++)
        {
            var s = siraliTablo[i];

            // Sıra rengini belirle
            Color satırRengi = i switch
            {
                0 => Color.FromArgb("#1A2E1A"),  // Lider: yeşilimsi
                1 => Color.FromArgb("#1A1A2E"),  // 2. sıra: mavimsi
                2 => Color.FromArgb("#2E1A1A"),  // 3. sıra: kırmızımsı
                _ => Color.FromArgb("#181818"),  // Diğerleri
            };

            string siraEmoji = i switch
            {
                0 => "🥇",
                1 => "🥈",
                2 => "🥉",
                _ => $"{i + 1}. "
            };

            // Avantaj rengi
            Color avRenk = s.AvantajFarki > 0 ? Colors.LightGreen
                         : s.AvantajFarki < 0 ? Color.FromArgb("#FF6666")
                         : Colors.Gray;

            var satir = new Border
            {
                BackgroundColor = satırRengi,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 8 },
                Stroke = Color.FromArgb("#2A2A2A"),
                Padding = new Thickness(10, 9),
                Margin = new Thickness(0, 0),
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) },
                },
                ColumnSpacing = 2
            };

            // Takım adı
            grid.Add(new Label
            {
                Text = $"{siraEmoji} {s.TakimAdi}",
                TextColor = Colors.White,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.Center
            }, 0);

            // O G B M AG YG AV
            void EkleHucre(int kolon, string deger, Color renk = default)
            {
                grid.Add(new Label
                {
                    Text = deger,
                    TextColor = renk == default ? Colors.LightGray : renk,
                    FontSize = 12,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, kolon);
            }

            EkleHucre(1, s.Oynadigi.ToString());
            EkleHucre(2, s.Galibiyet.ToString(), Colors.LightGreen);
            EkleHucre(3, s.Beraberlik.ToString(), Color.FromArgb("#FFAA00"));
            EkleHucre(4, s.Maglubiyet.ToString(), Color.FromArgb("#FF6666"));
            EkleHucre(5, s.AttigiGol.ToString());
            EkleHucre(6, s.YedigiGol.ToString());

            // Avantaj farkı (+/-)
            grid.Add(new Label
            {
                Text = (s.AvantajFarki >= 0 ? "+" : "") + s.AvantajFarki,
                TextColor = avRenk,
                FontSize = 12,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 7);

            // Puan — büyük ve vurgulu
            grid.Add(new Label
            {
                Text = s.Puan.ToString(),
                TextColor = Colors.Gold,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            }, 8);

            satir.Content = grid;
            slTablo.Children.Add(satir);
        }
    }

    // -----------------------------------------------
    //  LİGİ SIFIRLA
    // -----------------------------------------------
    private async void OnLigiSifirlaClicked(object sender, EventArgs e)
    {
        bool onay = await DisplayAlert(
            "Ligi Sıfırla",
            "Tüm puan durumu silinecek. Emin misin?",
            "Evet, Sıfırla", "İptal");

        if (!onay) return;

        LigManager.LigiSifirla();
        TabloYenile();
        await DisplayAlert("Tamam", "Lig tablosu sıfırlandı!", "Harika");
    }

    // -----------------------------------------------
    //  ANA MENÜYE DÖN
    // -----------------------------------------------
    private async void OnAnaMenuyeDonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
