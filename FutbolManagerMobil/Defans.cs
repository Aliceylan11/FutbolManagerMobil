using FutbolManagerMobil;
using System;

namespace FutbolManagerMobil
{
    public class Defans : Futbolcu
    { 

        public int RiskliMudahaleEt(Futbolcu rakipHucumcu)
        {
            Random rasgele = new Random();

            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakipHucumcu.AnlikFormCarpani();

            int defansGucu = Convert.ToInt32((this.Topcalma * 0.5 + this.KararAlma * 0.3 + this.Agresiflik * 0.2) * benimFormum);
            int hucumGucu = Convert.ToInt32((rakipHucumcu.Teknik * 0.5 + rakipHucumcu.Hiz * 0.3 + rakipHucumcu.KararAlma * 0.2) * rakipFormu);
            int sonucZari = rasgele.Next(0, defansGucu + hucumGucu);
            hucumGucu += 20;
            if (sonucZari < defansGucu) return 1; // Temiz top çaldı
            else if (sonucZari > defansGucu + (hucumGucu / 2)) return -1; // Ceza sahası içi ağır faul -> PENALTI!
            else if (sonucZari > defansGucu + (hucumGucu / 4)) return -2; // Dışarıda taktik faul -> SERBEST VURUŞ!
            else return 0; // Çalım yedi, rakip geçti
        }

        public bool HavaTopuKazan(Futbolcu rakipForvet)
        {
            Random rasgele = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakipForvet.AnlikFormCarpani();

            double benimBoyAvantajim = this.boy / 100.0;
            double rakipBoyAvantaji = rakipForvet.boy / 100.0;

            int benimHavaGucum = Convert.ToInt32(((this.Guc * 0.6 + this.Agresiflik * 0.4) * benimBoyAvantajim) * benimFormum);
            int rakipHavaGucu = Convert.ToInt32(((rakipForvet.Guc * 0.6 + rakipForvet.Agresiflik * 0.4) * rakipBoyAvantaji) * rakipFormu);

            return rasgele.Next(0, benimHavaGucum + rakipHavaGucu) < (benimHavaGucum + 5);
        }
         
    }
}