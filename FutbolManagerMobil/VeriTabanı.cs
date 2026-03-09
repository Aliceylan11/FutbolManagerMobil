using System.Collections.Generic;
using static FutbolManagerMobil.Futbolcu;

namespace FutbolManagerMobil
{
    public class Veritabani
    {
        // 1. FORM 2 İÇİN SAHTE TAKIM LİSTESİ
        public static List<Takim> TumTakimlariGetir()
        {
            return new List<Takim>
            {
                new Takim { TakimID = 1, Isim = "Galatasaray" },
                new Takim { TakimID = 2, Isim = "Fenerbahçe" },
                new Takim { TakimID = 3, Isim = "Beşiktaş" },
                new Takim { TakimID = 4, Isim = "Trabzonspor" },
                new Takim { TakimID = 5, Isim = "Gençlerbirliği" },
                new Takim { TakimID = 6, Isim = "Gaziantep" },
                new Takim { TakimID = 7, Isim = "Gümüşhanespor" },
            };
        }

        // 2. MAÇ MOTORU İÇİN SAHTE KADRO YÜKLEMESİ 
        public static Takim TakimCek(int takimId, string takimIsmi)
        {
            Takim t = new Takim(takimIsmi);
            t.TakimID = takimId;

            if (takimId == 1) // GALATASARAY KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Muslera", Kalecilik = 90, Hiz = 50, Kondisyon = 100, Moral = 100 }; 
                t.Defans = new Defans { isim = "Abdülkerim", Def = 85, Guc = 90, Kondisyon = 100, Moral = 100,  }; 
                t.DOS = new OrtaSaha { isim = "Torreira", Def = 85, Pas = 80, Kondisyon = 100, Moral = 100,   };
                t.MOS = new OrtaSaha { isim = "Sara", Pas = 85, Vizyon = 88, Kondisyon = 100, Moral = 100,   };
                t.OOS = new OrtaSaha { isim = "Mertens", Pas = 88, Teknik = 90, Kondisyon = 100, Moral = 100,  }; 
                t.Forvet = new Forvet { isim = "Osimhen", Bitiricilik = 95, Guc = 90, Kondisyon = 100, Moral = 100,  };
            }
            else if (takimId == 2) // FENERBAHÇE KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Livakovic", Kalecilik = 88, Hiz = 45, Kondisyon = 100, Moral = 100 }; 
                t.Defans = new Defans { isim = "Djiku", Def = 86, Guc = 85, Kondisyon = 100, Moral = 100,   }; 
                t.DOS = new OrtaSaha { isim = "İsmail Yüksek", Def = 84, Pas = 78, Kondisyon = 100, Moral = 100, };
                t.MOS = new OrtaSaha { isim = "Fred", Pas = 88, Vizyon = 85, Kondisyon = 100, Moral = 100, };
                t.OOS = new OrtaSaha { isim = "Szymanski", Pas = 85, Teknik = 88, Kondisyon = 100, Moral = 100,  }; 
                t.Forvet = new Forvet { isim = "En-Nesyri", Bitiricilik = 88, Guc = 85, Kondisyon = 100, Moral = 100,   };
            }
            else if (takimId == 3) // BEŞİKTAŞ KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Mert Günok", Kalecilik = 85, Hiz = 40, Kondisyon = 100, Moral = 100 };
                t.Defans = new Defans { isim = "Colley", Def = 83, Guc = 87, Kondisyon = 100, Moral = 100 };
                t.DOS = new OrtaSaha { isim = "Al Musrati", Def = 82, Pas = 81, Kondisyon = 100, Moral = 100 };
                t.MOS = new OrtaSaha { isim = "Gedson F.", Pas = 84, Vizyon = 83, Kondisyon = 100, Moral = 100 };
                t.OOS = new OrtaSaha { isim = "Rafa Silva", Pas = 87, Teknik = 89, Kondisyon = 100, Moral = 100 };
                t.Forvet = new Forvet { isim = "Immobile", Bitiricilik = 89, Guc = 84, Kondisyon = 100, Moral = 100 };
            }
            else if (takimId == 4) // TRABZONSPOR KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Uğurcan", Kalecilik = 86, Hiz = 42, Kondisyon = 100, Moral = 100 };
                t.Defans = new Defans { isim = "Denswil", Def = 81, Guc = 83, Kondisyon = 100, Moral = 100 };
                t.DOS = new OrtaSaha { isim = "Mendy", Def = 83, Pas = 79, Kondisyon = 100, Moral = 100 };
                t.MOS = new OrtaSaha { isim = "Ozan Tufan", Pas = 80, Vizyon = 78, Kondisyon = 100, Moral = 100 };
                t.OOS = new OrtaSaha { isim = "Visca", Pas = 84, Teknik = 86, Kondisyon = 100, Moral = 100 };
                t.Forvet = new Forvet { isim = "Banza", Bitiricilik = 83, Guc = 85, Kondisyon = 100, Moral = 100 };
            }
            else if (takimId == 5) // GENÇLERBİRLİĞİ KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Ertuğrul", Kalecilik = 72, Hiz = 35, Kondisyon = 100, Moral = 100 };
                t.Defans = new Defans { isim = "Yasin", Def = 70, Guc = 74, Kondisyon = 100, Moral = 100 };
                t.DOS = new OrtaSaha { isim = "Ensar", Def = 71, Pas = 68, Kondisyon = 100, Moral = 100 };
                t.MOS = new OrtaSaha { isim = "Buğra", Pas = 73, Vizyon = 70, Kondisyon = 100, Moral = 100 };
                t.OOS = new OrtaSaha { isim = "Amilton", Pas = 75, Teknik = 77, Kondisyon = 100, Moral = 100 };
                t.Forvet = new Forvet { isim = "Yatabare", Bitiricilik = 76, Guc = 80, Kondisyon = 100, Moral = 100 };
            }
            else if (takimId == 6) // GAZİANTEP FK KADROSU
            {
                t.Kaleci = new Kaleci { isim = "Dioudis", Kalecilik = 75, Hiz = 38, Kondisyon = 100, Moral = 100 };
                t.Defans = new Defans { isim = "Ertuğrul", Def = 74, Guc = 76, Kondisyon = 100, Moral = 100 };
                t.DOS = new OrtaSaha { isim = "N'Diaye", Def = 76, Pas = 72, Kondisyon = 100, Moral = 100 };
                t.MOS = new OrtaSaha { isim = "Kozlowski", Pas = 76, Vizyon = 75, Kondisyon = 100, Moral = 100 };
                t.OOS = new OrtaSaha { isim = "Maxim", Pas = 80, Teknik = 82, Kondisyon = 100, Moral = 100 };
                t.Forvet = new Forvet { isim = "Kenan Kodro", Bitiricilik = 77, Guc = 79, Kondisyon = 100, Moral = 100 };
            }
            else if (takimId == 7) // GÜMÜŞHANESPOR KADROSU  
            {
                t.Kaleci = new Kaleci { isim = "Gümüş Eldiven", Kalecilik = 85, Hiz = 50, Kondisyon = 100, Moral = 100 };
                t.Defans = new Defans { isim = "Kelkit Duvarı", Def = 88, Guc = 90, Kondisyon = 100, Moral = 100 };
                t.DOS = new OrtaSaha { isim = "Dinamik Torul", Def = 85, Pas = 82, Kondisyon = 100, Moral = 100 };
                t.MOS = new OrtaSaha { isim = "Zigana Rüzgarı", Pas = 88, Vizyon = 85, Kondisyon = 100, Moral = 100 };
                t.OOS = new OrtaSaha { isim = "Pestil Bey", Pas = 90, Teknik = 92, Kondisyon = 100, Moral = 100 };
                t.Forvet = new Forvet { isim = "Kuşburnu Fırtınası", Bitiricilik = 95, Guc = 88, Kondisyon = 100, Moral = 100 };
            }

            return t;
        }
    }
}