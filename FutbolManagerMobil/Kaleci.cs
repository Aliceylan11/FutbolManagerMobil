using FutbolManagerMobil;
using System;

namespace FutbolManagerMobil
{
    public class Kaleci : Futbolcu
    {
        // 1. SAVUNMA: Normal şutları karşılar
        public bool KurtarisYap(Futbolcu rakipVurucu)
        {
            Random rasgele = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakipVurucu.AnlikFormCarpani();

            int engKatsayi = Convert.ToInt32((this.Kalecilik * 0.6 + this.KararAlma * 0.2 + this.Hiz * 0.2) * benimFormum);
            int golKatsayi = Convert.ToInt32((rakipVurucu.Bitiricilik * 0.5 + rakipVurucu.Sogukkanlilik * 0.3 + rakipVurucu.Guc * 0.2) * rakipFormu);

            // Eğer rastgele sayı gol katsayısından küçükse gol olur, değilse kaleci kurtarır
            return rasgele.Next(0, golKatsayi + engKatsayi) >= golKatsayi;
        }

        // 2. SAVUNMA: Penaltı pozisyonları için özel metot
        public bool PenaltiKurtar(Futbolcu rakipVurucu)
        {
            Random rasgele = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakipVurucu.AnlikFormCarpani();

            int kurtaris = Convert.ToInt32((this.Kalecilik * 0.5 + this.Sogukkanlilik * 0.3 + this.KararAlma * 0.2) * benimFormum);
            int sutGucu = Convert.ToInt32((rakipVurucu.Bitiricilik * 0.5 + rakipVurucu.Sogukkanlilik * 0.5) * rakipFormu);

            return rasgele.Next(0, kurtaris + sutGucu + 20) < kurtaris;
        }

      
    }
}