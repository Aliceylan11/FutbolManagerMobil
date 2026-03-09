using System;
using System.Collections.Generic;
using System.Linq; // LINQ kullanýmý için gerekli
using Microsoft.Maui.Controls;

namespace FutbolManagerMobil;

public partial class SelectionPage : ContentPage
{
    // Sýnýf seviyesinde tanýmlý, her yerden eriþilebilir liste
    List<Takim> tumTakimlar;

    public SelectionPage()
    {
        InitializeComponent();

        // Takýmlarý veritabanýndan SADECE BURADA çekip ana listemize (tumTakimlar) atýyoruz
        tumTakimlar = Veritabani.TumTakimlariGetir();

        // UI (Ekrana) Seçim kutularýný (Picker) dolduruyoruz
        pckEvSahibi.ItemsSource = tumTakimlar.Select(t => t.Isim).ToList();
        pckDeplasman.ItemsSource = tumTakimlar.Select(t => t.Isim).ToList();
    }

    private async void OnMacaBaslaClicked(object sender, EventArgs e)
    {
        // Kullanýcý takým seçmeden basarsa uyar
        if (pckEvSahibi.SelectedIndex == -1 || pckDeplasman.SelectedIndex == -1)
        {
            await DisplayAlert("Hata", "Lütfen iki takýmý da seçin!", "Tamam");
            return;
        }

        // Seçilen Takým Nesnelerini, dolu olan "tumTakimlar" listesinden alýyoruz
        var secilenEv = tumTakimlar[pckEvSahibi.SelectedIndex];
        var secilenDep = tumTakimlar[pckDeplasman.SelectedIndex];

        // DÝKKAT: MainPage'e bu bilgileri (ID ve Ýsim) sorunsuzca gönderiyoruz
        await Navigation.PushAsync(new MainPage(secilenEv.TakimID, secilenEv.Isim, secilenDep.TakimID, secilenDep.Isim));
    }
}