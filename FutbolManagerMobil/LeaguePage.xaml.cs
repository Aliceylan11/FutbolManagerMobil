using System;
using Microsoft.Maui.Controls;

namespace FutbolManagerMobil;

public partial class LeaguePage : ContentPage
{
    // Maç sonucu ile açılış — tabloya işler ve fikstüre ekler
    public LeaguePage(string evTakim, int evGol, string depTakim, int depGol,
                      string evHoca = "", string depHoca = "")
    {
        InitializeComponent();
        LigManager.MacSonucunuIsle(evTakim, evGol, depTakim, depGol, evHoca, depHoca);
        TabloYenile();
        FiksturYenile();
    }

    // Sadece görüntüle (📊 butonundan gelindiğinde)
    public LeaguePage()
    {
        InitializeComponent();
        TabloYenile();
        FiksturYenile();
    }

    // ── PUAN TABLOSU ──
    private void TabloYenile()
    {
        slTablo.Children.Clear();
        var tablo = LigManager.SiraliTabloGetir();

        if (tablo.Count == 0)
        {
            slTablo.Children.Add(new Label
            {
                Text = "Henüz lig maçı oynanmadı.",
                TextColor = Colors.Gray,
                FontSize = 14,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 12)
            });
            return;
        }

        for (int i = 0; i < tablo.Count; i++)
        {
            var s = tablo[i];

            Color bgRenk = i switch
            {
                0 => Color.FromArgb("#1A2E1A"),
                1 => Color.FromArgb("#1A1A2E"),
                2 => Color.FromArgb("#2E1A1A"),
                _ => Color.FromArgb("#181818"),
            };

            string emoji = i switch { 0 => "🥇", 1 => "🥈", 2 => "🥉", _ => $"{i + 1}." };

            Color avRenk = s.AvantajFarki > 0 ? Colors.LightGreen
                         : s.AvantajFarki < 0 ? Color.FromArgb("#FF6666")
                         : Colors.Gray;

            var satir = new Border
            {
                BackgroundColor = bgRenk,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 8 },
                Stroke = Color.FromArgb("#2A2A2A"),
                Padding = new Thickness(10, 8),
            };

            var grid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection(
                    new ColumnDefinition(new GridLength(3, GridUnitType.Star)),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(new GridLength(1.5, GridUnitType.Star))
                ),
                ColumnSpacing = 2
            };

            void Ekle(int col, string text, Color? renk = null, bool bold = false)
            {
                grid.Add(new Label
                {
                    Text = text,
                    TextColor = renk ?? Colors.LightGray,
                    FontSize = 12,
                    FontAttributes = bold ? FontAttributes.Bold : FontAttributes.None,
                    HorizontalOptions = col == 0 ? LayoutOptions.Start : LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }, col);
            }

            Ekle(0, $"{emoji} {s.TakimAdi}", Colors.White, true);
            Ekle(1, s.Oynadigi.ToString());
            Ekle(2, s.Galibiyet.ToString(), Colors.LightGreen);
            Ekle(3, s.Beraberlik.ToString(), Color.FromArgb("#FFAA00"));
            Ekle(4, s.Maglubiyet.ToString(), Color.FromArgb("#FF6666"));
            Ekle(5, s.AttigiGol.ToString());
            Ekle(6, s.YedigiGol.ToString());
            Ekle(7, (s.AvantajFarki >= 0 ? "+" : "") + s.AvantajFarki, avRenk);
            Ekle(8, s.Puan.ToString(), Colors.Gold, true);

            satir.Content = grid;
            slTablo.Children.Add(satir);
        }
    }

    // ── FİKSTÜR / SONUÇLAR ──
    private void FiksturYenile()
    {
        slFikstur.Children.Clear();
        var liste = LigManager.FiksturYukle();

        if (liste.Count == 0)
        {
            slFikstur.Children.Add(new Label
            {
                Text = "Henüz oynanmış maç yok.",
                TextColor = Colors.Gray,
                FontSize = 13,
                HorizontalOptions = LayoutOptions.Center
            });
            return;
        }

        foreach (var mac in liste)
        {
            // Sonuç rengi
            Color sonucRenk = mac.EvGol > mac.DepGol ? Color.FromArgb("#1A3A1A")
                            : mac.EvGol < mac.DepGol ? Color.FromArgb("#3A1A1A")
                            : Color.FromArgb("#252525");

            var kart = new Border
            {
                BackgroundColor = sonucRenk,
                StrokeShape = new Microsoft.Maui.Controls.Shapes.RoundRectangle { CornerRadius = 8 },
                Stroke = Color.FromArgb("#333333"),
                Padding = new Thickness(10, 8),
            };

            var icerik = new VerticalStackLayout { Spacing = 2 };

            // Skor satırı
            icerik.Children.Add(new Label
            {
                Text = $"{mac.EvTakim}   {mac.EvGol} – {mac.DepGol}   {mac.DepTakim}",
                TextColor = Colors.White,
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.Center
            });

            // Hoca satırı (varsa)
            if (!string.IsNullOrWhiteSpace(mac.EvHoca) || !string.IsNullOrWhiteSpace(mac.DepHoca))
            {
                icerik.Children.Add(new Label
                {
                    Text = $"Hocalar: {mac.EvHoca} vs {mac.DepHoca}",
                    TextColor = Color.FromArgb("#AAAAAA"),
                    FontSize = 11,
                    HorizontalOptions = LayoutOptions.Center
                });
            }

            // Tarih
            if (!string.IsNullOrWhiteSpace(mac.Tarih))
            {
                icerik.Children.Add(new Label
                {
                    Text = mac.Tarih,
                    TextColor = Color.FromArgb("#666666"),
                    FontSize = 10,
                    HorizontalOptions = LayoutOptions.Center
                });
            }

            kart.Content = icerik;
            slFikstur.Children.Add(kart);
        }
    }

    // ── LİGİ SIFIRLA ──
    private async void OnLigiSifirlaClicked(object sender, EventArgs e)
    {
        bool onay = await DisplayAlert("Ligi Sıfırla",
            "Puan durumu ve tüm maç sonuçları silinecek. Emin misin?",
            "Evet, Sıfırla", "İptal");
        if (!onay) return;

        LigManager.LigiSifirla();
        TabloYenile();
        FiksturYenile();
        await DisplayAlert("Tamam", "Lig tablosu sıfırlandı!", "Harika");
    }

    // ── ANA MENÜ ──
    private async void OnAnaMenuyeDonClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}
