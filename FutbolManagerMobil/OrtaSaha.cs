using FutbolManagerMobil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutbolManagerMobil
{
    public class OrtaSaha : Futbolcu
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
        public bool DikinePas(Futbolcu rakipBaskisi)
        {
            Random r = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakipBaskisi.AnlikFormCarpani();

            int tasimaGucu = Convert.ToInt32((this.Teknik * 0.4 + this.Dayaniklilik * 0.4 + this.KararAlma * 0.2) * benimFormum);
            int baskiGucu = Convert.ToInt32((rakipBaskisi.Def * 0.5 + rakipBaskisi.Agresiflik * 0.3 + rakipBaskisi.Hiz * 0.2) * rakipFormu);

            return r.Next(0, tasimaGucu + baskiGucu) < tasimaGucu;
        }

        /*public bool OyunKur(Futbolcu rakip)
        {
            Random r = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakip.AnlikFormCarpani();

            int oyunKurmaGucu = Convert.ToInt32((this.Vizyon * 0.4 + this.Pas * 0.4 + this.Teknik * 0.2) * benimFormum);
            int kesmeGucu = Convert.ToInt32((rakip.KararAlma * 0.4 + rakip.Def * 0.4 + rakip.Hiz * 0.2) * rakipFormu);

            return r.Next(0, oyunKurmaGucu + kesmeGucu) < oyunKurmaGucu;
        }*/

        public bool FrikikKullan(Kaleci rakipKaleci)
        {
            Random r = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double kaleciFormu = rakipKaleci.AnlikFormCarpani();

            int frikikGucu = Convert.ToInt32((this.Teknik * 0.6 + this.Bitiricilik * 0.2 + this.Sogukkanlilik * 0.2) * benimFormum);
            int kurtaris = Convert.ToInt32((rakipKaleci.Kalecilik * 0.6 + rakipKaleci.KararAlma * 0.2 + rakipKaleci.Hiz * 0.2) * kaleciFormu);

            return r.Next(0, frikikGucu + kurtaris) < frikikGucu;
        }
 
        public bool AraPasiAt(Futbolcu rakip)
        {
            Random rasgele = new Random();
            double benimFormum = this.AnlikFormCarpani();
            double rakipFormu = rakip.AnlikFormCarpani();

            int pasKatsayi = Convert.ToInt32((this.Vizyon * 0.5 + this.Pas * 0.3 + this.Teknik * 0.2) * benimFormum);

            int kesmeKatsayi = Convert.ToInt32((rakip.KararAlma * 0.4 + rakip.Def * 0.3 + rakip.Hiz * 0.3) * rakipFormu);

            return rasgele.Next(0, pasKatsayi + kesmeKatsayi + 20) < (pasKatsayi + 20);
        }
         
    }
}