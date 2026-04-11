using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace FutbolManagerMobil;

public partial class SelectionPage : ContentPage
{
    List<Takim> tumTakimlar;

    private readonly Dictionary<string, OyunMotoru.OyunModu> modHaritasi = new()
    {
        { "🏆 Klasik Mod",  OyunMotoru.OyunModu.Klasik  },
        { "⚖️ Dengeli Mod", OyunMotoru.OyunModu.Dengeli },
        { "⚡ Efsane Modu", OyunMotoru.OyunModu.Efsane  },
    };

    private readonly Dictionary<OyunMotoru.OyunModu, string> modAciklamalari = new()
    {
        { OyunMotoru.OyunModu.Klasik,   "Oyuncular gerçek güçleriyle oynuyor. Güçlü takımlar avantajlı, zayıf takımlar dezavantajlı." },
        { OyunMotoru.OyunModu.Dengeli,  "Tüm özellikler 75'e sabitlenir. Taktik ve hamle seçimi her şeyi belirler." },
        { OyunMotoru.OyunModu.Efsane,   "Güçler 75'e dengelenir; senin takımından rastgele 3 oyuncuya +5 boost!" },
    };

    private readonly Dictionary<string, OyunMotoru.MacTipi> tipHaritasi = new()
    {
        { "🏆 Kupa (Uzatma / Penaltı)", OyunMotoru.MacTipi.Kupa },
        { "📋 Lig (90 Dakika)",         OyunMotoru.MacTipi.Lig  },
    };

    private readonly Dictionary<OyunMotoru.MacTipi, string> tipAciklamalari = new()
    {
        { OyunMotoru.MacTipi.Kupa, "Beraberlikte 105' ve 120'ye uzatma, ardından seri penaltı atışları!" },
        { OyunMotoru.MacTipi.Lig,  "Maç 90 dakikada biter, beraberlik geçerlidir." },
    };

    public SelectionPage()
    {
        InitializeComponent();

        tumTakimlar = Veritabani.TumTakimlariGetir();
        pckEvSahibi.ItemsSource = tumTakimlar.Select(t => t.Isim).ToList();
        pckDeplasman.ItemsSource = tumTakimlar.Select(t => t.Isim).ToList();
        pckOyunModu.ItemsSource = modHaritasi.Keys.ToList();
        pckMacTipi.ItemsSource = tipHaritasi.Keys.ToList();
    }

    // --- VALİDASYON ---
    private void ValidasyonYap()
    {
        bool hazir =
            pckEvSahibi.SelectedIndex != -1 &&
            pckDeplasman.SelectedIndex != -1 &&
            pckOyunModu.SelectedIndex != -1 &&
            pckMacTipi.SelectedIndex != -1 &&
            !string.IsNullOrWhiteSpace(entEvHoca.Text) &&
            !string.IsNullOrWhiteSpace(entDepHoca.Text);

        btnMacaBasla.IsEnabled = hazir;
        btnMacaBasla.Opacity = hazir ? 1.0 : 0.5;
    }

    private void OnPickerChanged(object sender, EventArgs e) => ValidasyonYap();
    private void OnEntryChanged(object sender, TextChangedEventArgs e) => ValidasyonYap();

    private void OnModSecildi(object sender, EventArgs e)
    {
        if (pckOyunModu.SelectedIndex == -1) return;
        var mod = modHaritasi[(string)pckOyunModu.SelectedItem];
        lblModAciklama.Text = modAciklamalari[mod];
        frmModAciklama.IsVisible = true;
        ValidasyonYap();
    }

    private void OnTipSecildi(object sender, EventArgs e)
    {
        if (pckMacTipi.SelectedIndex == -1) return;
        var tip = tipHaritasi[(string)pckMacTipi.SelectedItem];
        lblTipAciklama.Text = tipAciklamalari[tip];
        frmTipAciklama.IsVisible = true;
        ValidasyonYap();
    }

    // --- MAÇA BAŞLA ---
    private async void OnMacaBaslaClicked(object sender, EventArgs e)
    {
        if (pckEvSahibi.SelectedIndex == -1 || pckDeplasman.SelectedIndex == -1)
        { await DisplayAlert("Hata", "Lütfen iki takımı da seçin!", "Tamam"); return; }

        if (string.IsNullOrWhiteSpace(entEvHoca.Text) || string.IsNullOrWhiteSpace(entDepHoca.Text))
        { await DisplayAlert("Hata", "Lütfen her iki hoca adını da girin!", "Tamam"); return; }

        if (pckOyunModu.SelectedIndex == -1)
        { await DisplayAlert("Hata", "Lütfen bir oyun modu seçin!", "Tamam"); return; }

        if (pckMacTipi.SelectedIndex == -1)
        { await DisplayAlert("Hata", "Lütfen bir maç tipi seçin!", "Tamam"); return; }

        var secilenEv = tumTakimlar[pckEvSahibi.SelectedIndex];
        var secilenDep = tumTakimlar[pckDeplasman.SelectedIndex];
        var secilenMod = modHaritasi[(string)pckOyunModu.SelectedItem];
        var secilenTip = tipHaritasi[(string)pckMacTipi.SelectedItem];

        await Navigation.PushAsync(new MainPage(
            secilenEv.TakimID, secilenEv.Isim,
            secilenDep.TakimID, secilenDep.Isim,
            entEvHoca.Text.Trim(),
            entDepHoca.Text.Trim(),
            secilenMod,
            secilenTip
        ));
    }
}
