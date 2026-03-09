using System;
using Microsoft.Maui.Controls;

namespace FutbolManagerMobil;

public partial class StatistikPage : ContentPage
{
    public StatistikPage(string evAd, string depAd, int evSkor, int depSkor, int evSut, int depSut, int evPas, int depPas)
    {
        InitializeComponent();

        // 1. ÝSÝMLER VE SKOR
        lblEvAd.Text = evAd;
        lblDepAd.Text = depAd;
        lblSkor.Text = $"{evSkor} - {depSkor}";

        // 2. ÞUTLAR
        lblEvSut.Text = evSut.ToString();
        lblDepSut.Text = depSut.ToString();

        // 3. PASLAR
        lblEvPas.Text = evPas.ToString();
        lblDepPas.Text = depPas.ToString();

        // 4. TOPLA OYNAMA YÜZDESÝ (Pas sayýsýna göre dinamik hesaplanýr)
        double toplamPas = evPas + depPas;
        if (toplamPas > 0)
        {
            int evOran = (int)((evPas / toplamPas) * 100);
            lblEvTop.Text = $"%{evOran}";
            lblDepTop.Text = $"%{100 - evOran}";
        }
        else
        {
            // Eðer maçta hiç pas yapýlmadýysa
            lblEvTop.Text = "%50";
            lblDepTop.Text = "%50";
        }
    }

    // YENÝ MAÇ BUTONU (En baþa, takým seçme ekranýna döndürür)
    private async void OnYeniMacClicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
 
}