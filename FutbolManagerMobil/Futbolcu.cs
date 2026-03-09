using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace FutbolManagerMobil
{

    public abstract class Futbolcu
    {
        public string isim;
        public int boy;
        public int Guc, Hiz, Dayaniklilik;
        public int KararAlma;
        public int Vizyon;
        public int Sogukkanlilik;
        public int Agresiflik;
        public int Teknik, Pas, Bitiricilik, Topcalma, Atak, Def, Kalecilik;
        public int Moral = 100;      // Gol atınca artar, top kaptırınca düşer
        public int Kondisyon = 100;  // Dakika ilerledikçe düşer
        public bool SariKartVarMi { get; set; } = false;
        public bool KirmiziKartYediMi { get; set; } = false;
        public bool SakatMi { get; set; } = false;
         
        
        public void Yorul(int miktar)
        {
            this.Kondisyon -= miktar;
            if (this.Kondisyon < 0) this.Kondisyon = 0;
        }
        public int KartGor()
        {
            // Dönüş Değerleri: 0 = Kart Yok, 1 = Sarı Kart, 2 = 2. Sarıdan Kırmızı, 3 = Direkt Kırmızı
            Random r = new Random();
            int zar = r.Next(0, 100);

            if (zar < this.Agresiflik / 2)
            {
                if (zar < 5) // Direkt Kırmızı
                {
                    this.KirmiziKartYediMi = true;
                    SahadanSil(); // Oyuncuyu etkisiz hale getir
                    return 3;
                }
                else
                {
                    if (this.SariKartVarMi) // İkinci Sarıdan Kırmızı
                    {
                        this.KirmiziKartYediMi = true;
                        SahadanSil(); // Oyuncuyu etkisiz hale getir
                        return 2;
                    }
                    else // İlk Sarı Kart
                    {
                        this.SariKartVarMi = true;
                        return 1;
                    }
                }
            }
            return 0; // Kart çıkmadı
        }
        private void SahadanSil()
        {
            // Oyuncu kırmızı kart gördüğünde gücü 1'e düşer. 
            // Artık girdiği tüm ikili mücadeleleri ve şutları %100 kaybedecektir!
            this.Hiz = 1;
            this.Guc = 1;
            this.Def = 1;
            this.Pas = 1;
            this.Topcalma = 1;
            this.Bitiricilik = 1;
            this.KararAlma = 1;
            this.Agresiflik = 1;
            this.Sogukkanlilik = 1;
            this.Vizyon = 1;
            this.Kalecilik = 1;
        }
        public double AnlikFormCarpani()
        {
            double carpan = 1.0;

            // Moral Etkisi
            if (Moral >= 85) carpan += 0.15;      // Moralli oyuncuya %15 bonus
            else if (Moral <= 40) carpan -= 0.20; // Morali bozuksa %20 yetenek düşüşü

            // Kondisyon Etkisi
            if (Kondisyon <= 30) carpan -= 0.15;  // Çok yorulduysa %15 düşüş

            return carpan;
        }
        public bool CalimAt(Futbolcu gercekRakip)
        {
            Random rasgele = new Random();

            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = gercekRakip.AnlikFormCarpani();

            int calimKatsayi = Convert.ToInt32((this.Hiz * 0.25 + this.Teknik * 0.5 + this.Atak * 0.15 + this.Guc * 0.1) * benimFormum);
            int defansKatsayi = Convert.ToInt32((gercekRakip.Guc * 0.25 + gercekRakip.Hiz * 0.15 + gercekRakip.Def * 0.6) * rakipFormu);

            if (rasgele.Next(0, calimKatsayi + defansKatsayi) < calimKatsayi)
                return true;
            else
                return false;
        }

        public bool KisaPasAt(Futbolcu rakip)
        {
            Random r = new Random();
            int pasGucu = Convert.ToInt32((this.Pas * 0.6 + this.Sogukkanlilik * 0.4) * AnlikFormCarpani());
            int kesmeGucu = Convert.ToInt32((rakip.Def * 0.5 + rakip.KararAlma * 0.5) * rakip.AnlikFormCarpani());
             
            return r.Next(0, pasGucu + kesmeGucu + 10) < (pasGucu + 100);
        }

        public bool UzunPasAt(Futbolcu rakip)
        {
            Random r = new Random();
            int pasGucu = Convert.ToInt32((this.Guc * 0.4 + this.Pas * 0.3 + this.Vizyon * 0.3) * AnlikFormCarpani());
            int kesmeGucu = Convert.ToInt32((rakip.Guc * 0.4 + rakip.Def * 0.4 + rakip.KararAlma * 0.2) * rakip.AnlikFormCarpani());
             
            return r.Next(0, pasGucu + kesmeGucu + 10) < (pasGucu + 80);
        }
        public bool SutAt(Kaleci rakipKaleci)
        {
            Random rasgele = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double kaleciFormu = rakipKaleci.AnlikFormCarpani();

            int sutGucu = Convert.ToInt32((this.Bitiricilik * 0.5 + this.Sogukkanlilik * 0.3 + this.Guc * 0.2) * benimFormum);
            int kurtaris = Convert.ToInt32((rakipKaleci.Kalecilik * 0.6 + rakipKaleci.KararAlma * 0.2 + rakipKaleci.Hiz * 0.2) * kaleciFormu);

            return rasgele.Next(0, sutGucu + kurtaris) < (sutGucu + 30);
        }
        public bool PressYap(Futbolcu rakip)
        {
            Random r = new Random();
            double form = this.AnlikFormCarpani(); //
            double rakipForm = rakip.AnlikFormCarpani();
             
            int pressGucu = Convert.ToInt32((this.Hiz * 0.4 + this.Agresiflik * 0.4 + this.Dayaniklilik * 0.2) * form);
             
            int rakipDirenc = Convert.ToInt32((rakip.Teknik * 0.5 + rakip.Sogukkanlilik * 0.5) * rakipForm); 
            return r.Next(0, pressGucu + rakipDirenc + 10) < (pressGucu + 20);
        }
        public bool TopCalma(Futbolcu rakip)
        {
            Random r = new Random();
            double form = this.AnlikFormCarpani();  
            double rakipForm = rakip.AnlikFormCarpani();
             
            int calmaGucu = Convert.ToInt32((this.Topcalma * 0.6 + this.Hiz * 0.2 + this.KararAlma * 0.2) * form);
             
            int saklamaGucu = Convert.ToInt32((rakip.Teknik * 0.6 + rakip.Guc * 0.4) * rakipForm);

            return r.Next(0, calmaGucu + saklamaGucu) < calmaGucu;
        }
    }
}