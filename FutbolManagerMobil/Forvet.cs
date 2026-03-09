using FutbolManagerMobil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolManagerMobil
{
    public class Forvet : Futbolcu
    {
        public bool UzaktanSutAt(Kaleci rakipKaleci)
        {
            Random r = new Random();
            double benimFormum = this.AnlikFormCarpani();  
            double kaleciFormu = rakipKaleci.AnlikFormCarpani();    

            // Uzaktan şutta Teknik ve Güç ön plandadır
            int sutGucu = Convert.ToInt32((this.Teknik * 0.4 + this.Guc * 0.4 + this.Sogukkanlilik * 0.2) * benimFormum);

            // Kalecinin uzaktan şutu kurtarması için Kalecilik ve Karar Alma (pozisyon alma) önemlidir
            int kurtarisGucu = Convert.ToInt32((rakipKaleci.Kalecilik * 0.6 + rakipKaleci.KararAlma * 0.3 + rakipKaleci.Hiz * 0.1) * kaleciFormu);

            // Uzaktan şut olduğu için kaleciye küçük bir avantaj (+15) verdik, mesafe uzaklığı nedeniyle
            return r.Next(0, sutGucu + kurtarisGucu + 15) < sutGucu;
        }


    }
}