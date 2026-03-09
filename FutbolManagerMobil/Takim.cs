using System.Collections.Generic;

namespace FutbolManagerMobil
{
    public class Takim
    {
        public int TakimID { get; set; }
        public string Isim { get; set; } 

        public Takim(string takimIsmi)
        {
            this.Isim = takimIsmi;
        }
        public Takim() { }
        public int Gol { get; set; } = 0;
        public int ToplamPas { get; set; } = 0;
        public int IsabetliPas { get; set; } = 0;
        public int ToplamSut { get; set; } = 0;
        public int IsabetliSut { get; set; } = 0;
        public List<string> GolAtanlar { get; set; } = new List<string>();
         
            // 11 oyuncu yerine sadece senin 6 bölge oyuncun
            public Kaleci Kaleci { get; set; }      // (K)
            public Defans Defans { get; set; }      // (D)
            public OrtaSaha DOS { get; set; }       // (DOS)
            public OrtaSaha MOS { get; set; }       // (MOS)
            public OrtaSaha OOS { get; set; }       // (OOS)
            public Forvet Forvet { get; set; }      // (F)
         
         
        public bool KadroIceriyorMu(Futbolcu arananOyuncu)
        {
            if (arananOyuncu == null) return false;

            if (this.Kaleci == arananOyuncu || this.Defans == arananOyuncu || this.DOS == arananOyuncu || 
                this.MOS == arananOyuncu || this.OOS == arananOyuncu || this.Forvet == arananOyuncu)
            {
                return true;
            }

            return false;
        }
    }
}