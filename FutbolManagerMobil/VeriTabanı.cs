using System.Collections.Generic;
using static FutbolManagerMobil.Futbolcu;

namespace FutbolManagerMobil
{
    public class Veritabani
    {
        public static List<Takim> TumTakimlariGetir()
        {
            return new List<Takim>
            {
                // TÜRK KULÜPLERİ
                new Takim { TakimID =  1, Isim = "Galatasaray"      },
                new Takim { TakimID =  2, Isim = "Fenerbahçe"       },
                new Takim { TakimID =  3, Isim = "Beşiktaş"         },
                new Takim { TakimID =  4, Isim = "Trabzonspor"       },
                new Takim { TakimID =  5, Isim = "Başakşehir"        },
                new Takim { TakimID =  6, Isim = "Sivasspor"         },
                new Takim { TakimID =  7, Isim = "Kasımpaşa"         },
                new Takim { TakimID =  8, Isim = "Konyaspor"         },
                new Takim { TakimID =  9, Isim = "Rizespor"          },
                new Takim { TakimID = 10, Isim = "Ankaragücü"        },
                new Takim { TakimID = 11, Isim = "Gençlerbirliği"    },
                new Takim { TakimID = 12, Isim = "Gaziantep FK"      },
                new Takim { TakimID = 13, Isim = "Gümüşhanespor"     },
                // AVRUPA KULÜPLERİ
                new Takim { TakimID = 14, Isim = "Real Madrid"       },
                new Takim { TakimID = 15, Isim = "Barcelona"         },
                new Takim { TakimID = 16, Isim = "Man. City"         },
                new Takim { TakimID = 17, Isim = "Liverpool"         },
                new Takim { TakimID = 18, Isim = "Bayern München"    },
                new Takim { TakimID = 19, Isim = "PSG"               },
                new Takim { TakimID = 20, Isim = "Arsenal"           },
                new Takim { TakimID = 21, Isim = "Chelsea"           },
                new Takim { TakimID = 22, Isim = "Juventus"          },
                new Takim { TakimID = 23, Isim = "Inter Milan"       },
                new Takim { TakimID = 24, Isim = "AC Milan"          },
                new Takim { TakimID = 25, Isim = "Atletico Madrid"   },
                new Takim { TakimID = 26, Isim = "Dortmund"          },
                new Takim { TakimID = 27, Isim = "Ajax"              },
                // MİLLİ TAKIMLAR
                new Takim { TakimID = 28, Isim = "Türkiye"           },
                new Takim { TakimID = 29, Isim = "Fransa"            },
                new Takim { TakimID = 30, Isim = "İngiltere"         },
                new Takim { TakimID = 31, Isim = "Almanya"           },
                new Takim { TakimID = 32, Isim = "İspanya"           },
                new Takim { TakimID = 33, Isim = "Brezilya"          },
                new Takim { TakimID = 34, Isim = "Arjantin"          },
                new Takim { TakimID = 35, Isim = "Portekiz"          },
                new Takim { TakimID = 36, Isim = "Kolombiya"         },
            };
        }

        public static Takim TakimCek(int takimId, string takimIsmi)
        {
            Takim t = new Takim(takimIsmi) { TakimID = takimId };

            switch (takimId)
            {
                // ── 1. GALATASARAY ──────────────────────────────────────────
                case 1:
                    t.Kaleci = new Kaleci  { isim="Uğurcan Çakır",    Kalecilik=90,Hiz=52,Guc=75,Dayaniklilik=82,KararAlma=87,Vizyon=68,Sogukkanlilik=86,Agresiflik=45,Teknik=60,Pas=62,Bitiricilik=10,Topcalma=40,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Abdülkerim E.",   Def=88,Guc=87,Dayaniklilik=85,Hiz=74,KararAlma=83,Vizyon=70,Sogukkanlilik=80,Agresiflik=78,Teknik=70,Pas=72,Bitiricilik=42,Topcalma=86,Atak=55,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Torreira",        Def=87,Pas=82,Dayaniklilik=88,Hiz=75,KararAlma=85,Vizyon=80,Sogukkanlilik=82,Agresiflik=82,Teknik=80,Bitiricilik=62,Topcalma=85,Atak=68,Guc=78,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Sara",            Pas=86,Vizyon=88,Dayaniklilik=84,Hiz=80,KararAlma=87,Sogukkanlilik=84,Agresiflik=68,Teknik=85,Def=72,Bitiricilik=70,Topcalma=75,Atak=78,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Barış",         Teknik=90,Pas=89,Dayaniklilik=80,Hiz=82,KararAlma=88,Vizyon=90,Sogukkanlilik=88,Agresiflik=60,Def=62,Bitiricilik=86,Topcalma=72,Atak=88,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Osimhen",         Bitiricilik=95,Guc=90,Dayaniklilik=88,Hiz=92,KararAlma=86,Vizyon=78,Sogukkanlilik=88,Agresiflik=75,Teknik=84,Pas=72,Def=38,Topcalma=65,Atak=94,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 2. FENERBAHÇE ────────────────────────────────────────────
                case 2:
                    t.Kaleci = new Kaleci  { isim="Ederson",       Kalecilik=89,Hiz=48,Guc=78,Dayaniklilik=82,KararAlma=87,Vizyon=70,Sogukkanlilik=88,Agresiflik=44,Teknik=58,Pas=60,Bitiricilik=10,Topcalma=40,Atak=8, Def=70,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Milan Škriniar",           Def=87,Guc=88,Dayaniklilik=86,Hiz=76,KararAlma=84,Vizyon=70,Sogukkanlilik=80,Agresiflik=80,Teknik=68,Pas=70,Bitiricilik=40,Topcalma=85,Atak=52,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="İsmail Yüksek",   Def=84,Pas=80,Dayaniklilik=86,Hiz=78,KararAlma=83,Vizyon=80,Sogukkanlilik=80,Agresiflik=78,Teknik=78,Bitiricilik=65,Topcalma=82,Atak=70,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Mattéo Guendouzi",            Pas=88,Vizyon=86,Dayaniklilik=87,Hiz=78,KararAlma=86,Sogukkanlilik=84,Agresiflik=80,Teknik=84,Def=80,Bitiricilik=68,Topcalma=82,Atak=74,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Asensio",       Teknik=86,Pas=84,Dayaniklilik=84,Hiz=82,KararAlma=86,Vizyon=88,Sogukkanlilik=84,Agresiflik=64,Def=64,Bitiricilik=80,Topcalma=70,Atak=84,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Talisca",       Bitiricilik=90,Guc=88,Dayaniklilik=86,Hiz=85,KararAlma=83,Vizyon=76,Sogukkanlilik=86,Agresiflik=72,Teknik=80,Pas=68,Def=36,Topcalma=60,Atak=90,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 3. BEŞİKTAŞ ─────────────────────────────────────────────
                case 3:
                    t.Kaleci = new Kaleci  { isim="Mert Günok",      Kalecilik=86,Hiz=44,Guc=74,Dayaniklilik=80,KararAlma=84,Vizyon=66,Sogukkanlilik=84,Agresiflik=42,Teknik=55,Pas=58,Bitiricilik=10,Topcalma=38,Atak=8, Def=68,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Colley",          Def=84,Guc=88,Dayaniklilik=84,Hiz=74,KararAlma=80,Vizyon=68,Sogukkanlilik=78,Agresiflik=82,Teknik=65,Pas=65,Bitiricilik=38,Topcalma=84,Atak=48,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Al-Musrati",      Def=83,Pas=82,Dayaniklilik=84,Hiz=76,KararAlma=82,Vizyon=78,Sogukkanlilik=80,Agresiflik=76,Teknik=78,Bitiricilik=62,Topcalma=80,Atak=68,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Gedson F.",       Pas=85,Vizyon=84,Dayaniklilik=84,Hiz=80,KararAlma=84,Sogukkanlilik=82,Agresiflik=72,Teknik=83,Def=74,Bitiricilik=70,Topcalma=76,Atak=76,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Rafa Silva",      Teknik=88,Pas=86,Dayaniklilik=82,Hiz=84,KararAlma=85,Vizyon=88,Sogukkanlilik=84,Agresiflik=60,Def=60,Bitiricilik=82,Topcalma=68,Atak=86,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Immobile",        Bitiricilik=88,Guc=82,Dayaniklilik=80,Hiz=78,KararAlma=85,Vizyon=78,Sogukkanlilik=89,Agresiflik=68,Teknik=80,Pas=70,Def=34,Topcalma=55,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 4. TRABZONSPOR ───────────────────────────────────────────
                case 4:
                    t.Kaleci = new Kaleci  { isim="Ertuğrul Çetin",  Kalecilik=84,Hiz=46,Guc=74,Dayaniklilik=80,KararAlma=82,Vizyon=65,Sogukkanlilik=82,Agresiflik=40,Teknik=54,Pas=56,Bitiricilik=10,Topcalma=36,Atak=8, Def=66,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Cornelius",       Def=82,Guc=85,Dayaniklilik=83,Hiz=74,KararAlma=80,Vizyon=68,Sogukkanlilik=76,Agresiflik=80,Teknik=64,Pas=64,Bitiricilik=36,Topcalma=82,Atak=46,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Hamsik",          Def=80,Pas=82,Dayaniklilik=82,Hiz=76,KararAlma=82,Vizyon=80,Sogukkanlilik=80,Agresiflik=72,Teknik=80,Bitiricilik=65,Topcalma=78,Atak=70,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Ozan Tufan",      Pas=82,Vizyon=80,Dayaniklilik=82,Hiz=78,KararAlma=82,Sogukkanlilik=80,Agresiflik=74,Teknik=80,Def=72,Bitiricilik=68,Topcalma=74,Atak=74,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Visca",           Teknik=86,Pas=82,Dayaniklilik=80,Hiz=84,KararAlma=84,Vizyon=86,Sogukkanlilik=82,Agresiflik=58,Def=58,Bitiricilik=80,Topcalma=66,Atak=84,Guc=66,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Banza",           Bitiricilik=84,Guc=86,Dayaniklilik=84,Hiz=84,KararAlma=80,Vizyon=74,Sogukkanlilik=82,Agresiflik=70,Teknik=78,Pas=66,Def=34,Topcalma=58,Atak=86,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 5. BAŞAKŞEHİR ───────────────────────────────────────────
                case 5:
                    t.Kaleci = new Kaleci  { isim="Volkan Babacan",  Kalecilik=80,Hiz=42,Guc=70,Dayaniklilik=78,KararAlma=80,Vizyon=62,Sogukkanlilik=80,Agresiflik=38,Teknik=50,Pas=52,Bitiricilik=10,Topcalma=32,Atak=8, Def=62,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Bolingoli",       Def=78,Guc=80,Dayaniklilik=80,Hiz=76,KararAlma=76,Vizyon=66,Sogukkanlilik=74,Agresiflik=76,Teknik=62,Pas=62,Bitiricilik=34,Topcalma=78,Atak=42,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Grohe",           Def=78,Pas=78,Dayaniklilik=80,Hiz=74,KararAlma=78,Vizyon=76,Sogukkanlilik=78,Agresiflik=70,Teknik=76,Bitiricilik=60,Topcalma=76,Atak=64,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Tekdemir",        Pas=80,Vizyon=78,Dayaniklilik=80,Hiz=76,KararAlma=80,Sogukkanlilik=78,Agresiflik=70,Teknik=78,Def=70,Bitiricilik=64,Topcalma=72,Atak=70,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Chadli",          Teknik=82,Pas=80,Dayaniklilik=78,Hiz=78,KararAlma=80,Vizyon=82,Sogukkanlilik=80,Agresiflik=56,Def=56,Bitiricilik=76,Topcalma=62,Atak=80,Guc=64,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Gulbrandsen",     Bitiricilik=80,Guc=80,Dayaniklilik=80,Hiz=80,KararAlma=78,Vizyon=72,Sogukkanlilik=80,Agresiflik=66,Teknik=74,Pas=64,Def=30,Topcalma=54,Atak=82,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 6. SİVASSPOR ────────────────────────────────────────────
                case 6:
                    t.Kaleci = new Kaleci  { isim="Mamardashvili",   Kalecilik=82,Hiz=44,Guc=72,Dayaniklilik=78,KararAlma=80,Vizyon=62,Sogukkanlilik=80,Agresiflik=38,Teknik=50,Pas=52,Bitiricilik=10,Topcalma=32,Atak=8, Def=62,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Goutas",          Def=78,Guc=78,Dayaniklilik=78,Hiz=72,KararAlma=74,Vizyon=64,Sogukkanlilik=72,Agresiflik=74,Teknik=60,Pas=60,Bitiricilik=32,Topcalma=76,Atak=40,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Cofie",           Def=76,Pas=74,Dayaniklilik=78,Hiz=72,KararAlma=74,Vizyon=72,Sogukkanlilik=74,Agresiflik=68,Teknik=72,Bitiricilik=56,Topcalma=74,Atak=60,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Kayode",          Pas=76,Vizyon=74,Dayaniklilik=76,Hiz=76,KararAlma=76,Sogukkanlilik=74,Agresiflik=66,Teknik=74,Def=66,Bitiricilik=60,Topcalma=68,Atak=66,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Mooy",            Teknik=78,Pas=78,Dayaniklilik=76,Hiz=74,KararAlma=78,Vizyon=78,Sogukkanlilik=76,Agresiflik=54,Def=52,Bitiricilik=70,Topcalma=58,Atak=76,Guc=62,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Larin",           Bitiricilik=78,Guc=78,Dayaniklilik=78,Hiz=78,KararAlma=74,Vizyon=70,Sogukkanlilik=76,Agresiflik=64,Teknik=70,Pas=60,Def=28,Topcalma=50,Atak=80,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 7. KASIMPAŞA ────────────────────────────────────────────
                case 7:
                    t.Kaleci = new Kaleci  { isim="Yassine Bounou",  Kalecilik=80,Hiz=42,Guc=70,Dayaniklilik=76,KararAlma=78,Vizyon=60,Sogukkanlilik=78,Agresiflik=36,Teknik=48,Pas=50,Bitiricilik=10,Topcalma=30,Atak=8, Def=60,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Balanta",         Def=76,Guc=78,Dayaniklilik=76,Hiz=70,KararAlma=72,Vizyon=62,Sogukkanlilik=70,Agresiflik=72,Teknik=58,Pas=58,Bitiricilik=30,Topcalma=74,Atak=38,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Coulibaly",       Def=74,Pas=72,Dayaniklilik=76,Hiz=72,KararAlma=72,Vizyon=70,Sogukkanlilik=72,Agresiflik=66,Teknik=70,Bitiricilik=54,Topcalma=72,Atak=58,Guc=66,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Bensebaini",      Pas=74,Vizyon=72,Dayaniklilik=74,Hiz=72,KararAlma=74,Sogukkanlilik=72,Agresiflik=64,Teknik=72,Def=64,Bitiricilik=58,Topcalma=66,Atak=64,Guc=66,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Ghilas",          Teknik=76,Pas=74,Dayaniklilik=74,Hiz=76,KararAlma=74,Vizyon=76,Sogukkanlilik=74,Agresiflik=52,Def=50,Bitiricilik=68,Topcalma=56,Atak=74,Guc=60,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Sissoko",         Bitiricilik=76,Guc=76,Dayaniklilik=76,Hiz=78,KararAlma=72,Vizyon=68,Sogukkanlilik=74,Agresiflik=62,Teknik=68,Pas=58,Def=26,Topcalma=48,Atak=78,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 8. KONYASPOR ─────────────────────────────────────────────
                case 8:
                    t.Kaleci = new Kaleci  { isim="Serkan Kırıntılı",Kalecilik=76,Hiz=38,Guc=68,Dayaniklilik=74,KararAlma=74,Vizyon=58,Sogukkanlilik=74,Agresiflik=34,Teknik=44,Pas=46,Bitiricilik=10,Topcalma=28,Atak=8, Def=56,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Sıfal",           Def=73,Guc=76,Dayaniklilik=74,Hiz=68,KararAlma=70,Vizyon=60,Sogukkanlilik=68,Agresiflik=70,Teknik=56,Pas=56,Bitiricilik=28,Topcalma=72,Atak=36,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Jonsson",         Def=72,Pas=70,Dayaniklilik=74,Hiz=70,KararAlma=70,Vizyon=68,Sogukkanlilik=70,Agresiflik=64,Teknik=68,Bitiricilik=52,Topcalma=70,Atak=56,Guc=64,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Palaversa",       Pas=72,Vizyon=70,Dayaniklilik=72,Hiz=70,KararAlma=72,Sogukkanlilik=70,Agresiflik=62,Teknik=70,Def=62,Bitiricilik=56,Topcalma=64,Atak=62,Guc=64,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Helstad",         Teknik=74,Pas=72,Dayaniklilik=72,Hiz=74,KararAlma=72,Vizyon=74,Sogukkanlilik=72,Agresiflik=50,Def=48,Bitiricilik=66,Topcalma=54,Atak=72,Guc=58,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Musa",            Bitiricilik=74,Guc=74,Dayaniklilik=74,Hiz=76,KararAlma=70,Vizyon=66,Sogukkanlilik=72,Agresiflik=60,Teknik=66,Pas=56,Def=24,Topcalma=46,Atak=76,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 9. RİZESPOR ──────────────────────────────────────────────
                case 9:
                    t.Kaleci = new Kaleci  { isim="Gökhan Akkan",    Kalecilik=75,Hiz=38,Guc=67,Dayaniklilik=73,KararAlma=73,Vizyon=56,Sogukkanlilik=73,Agresiflik=33,Teknik=43,Pas=44,Bitiricilik=10,Topcalma=28,Atak=8, Def=55,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Medjani",         Def=72,Guc=75,Dayaniklilik=73,Hiz=67,KararAlma=69,Vizyon=59,Sogukkanlilik=67,Agresiflik=69,Teknik=55,Pas=55,Bitiricilik=27,Topcalma=71,Atak=35,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Bakasetas",       Def=70,Pas=69,Dayaniklilik=73,Hiz=70,KararAlma=69,Vizyon=67,Sogukkanlilik=69,Agresiflik=63,Teknik=67,Bitiricilik=51,Topcalma=69,Atak=55,Guc=63,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Nikolaou",        Pas=71,Vizyon=69,Dayaniklilik=71,Hiz=69,KararAlma=71,Sogukkanlilik=69,Agresiflik=61,Teknik=69,Def=61,Bitiricilik=55,Topcalma=63,Atak=61,Guc=63,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Sokolis",         Teknik=73,Pas=71,Dayaniklilik=71,Hiz=73,KararAlma=71,Vizyon=73,Sogukkanlilik=71,Agresiflik=49,Def=47,Bitiricilik=65,Topcalma=53,Atak=71,Guc=57,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Gözüküçük",       Bitiricilik=73,Guc=73,Dayaniklilik=73,Hiz=75,KararAlma=69,Vizyon=65,Sogukkanlilik=71,Agresiflik=59,Teknik=65,Pas=55,Def=23,Topcalma=45,Atak=75,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 10. ANKARAGÜCÜ ───────────────────────────────────────────
                case 10:
                    t.Kaleci = new Kaleci  { isim="Ömer Şişmanoğlu", Kalecilik=74,Hiz=37,Guc=66,Dayaniklilik=72,KararAlma=72,Vizyon=55,Sogukkanlilik=72,Agresiflik=32,Teknik=42,Pas=43,Bitiricilik=10,Topcalma=27,Atak=8, Def=54,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Lacroix",         Def=71,Guc=74,Dayaniklilik=72,Hiz=66,KararAlma=68,Vizyon=58,Sogukkanlilik=66,Agresiflik=68,Teknik=54,Pas=54,Bitiricilik=26,Topcalma=70,Atak=34,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Vural",           Def=69,Pas=68,Dayaniklilik=72,Hiz=69,KararAlma=68,Vizyon=66,Sogukkanlilik=68,Agresiflik=62,Teknik=66,Bitiricilik=50,Topcalma=68,Atak=54,Guc=62,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Zulj",            Pas=70,Vizyon=68,Dayaniklilik=70,Hiz=68,KararAlma=70,Sogukkanlilik=68,Agresiflik=60,Teknik=68,Def=60,Bitiricilik=54,Topcalma=62,Atak=60,Guc=62,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Hachadi",         Teknik=72,Pas=70,Dayaniklilik=70,Hiz=72,KararAlma=70,Vizyon=72,Sogukkanlilik=70,Agresiflik=48,Def=46,Bitiricilik=64,Topcalma=52,Atak=70,Guc=56,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Teyit",           Bitiricilik=72,Guc=72,Dayaniklilik=72,Hiz=74,KararAlma=68,Vizyon=64,Sogukkanlilik=70,Agresiflik=58,Teknik=64,Pas=54,Def=22,Topcalma=44,Atak=74,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 11. GENÇLERBİRLİĞİ ──────────────────────────────────────
                case 11:
                    t.Kaleci = new Kaleci  { isim="Ertuğrul Taşkın", Kalecilik=72,Hiz=36,Guc=65,Dayaniklilik=71,KararAlma=70,Vizyon=54,Sogukkanlilik=70,Agresiflik=30,Teknik=40,Pas=42,Bitiricilik=10,Topcalma=26,Atak=8, Def=52,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Yasin Öztekin",   Def=70,Guc=73,Dayaniklilik=71,Hiz=65,KararAlma=67,Vizyon=57,Sogukkanlilik=65,Agresiflik=67,Teknik=53,Pas=53,Bitiricilik=25,Topcalma=69,Atak=33,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Ensar Kesim",     Def=68,Pas=67,Dayaniklilik=71,Hiz=68,KararAlma=67,Vizyon=65,Sogukkanlilik=67,Agresiflik=61,Teknik=65,Bitiricilik=49,Topcalma=67,Atak=53,Guc=61,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Buğra Gündüz",   Pas=69,Vizyon=67,Dayaniklilik=69,Hiz=67,KararAlma=69,Sogukkanlilik=67,Agresiflik=59,Teknik=67,Def=59,Bitiricilik=53,Topcalma=61,Atak=59,Guc=61,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Amilton",         Teknik=71,Pas=69,Dayaniklilik=69,Hiz=71,KararAlma=69,Vizyon=71,Sogukkanlilik=69,Agresiflik=47,Def=45,Bitiricilik=63,Topcalma=51,Atak=69,Guc=55,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Yatabare",        Bitiricilik=71,Guc=71,Dayaniklilik=71,Hiz=73,KararAlma=67,Vizyon=63,Sogukkanlilik=69,Agresiflik=57,Teknik=63,Pas=53,Def=21,Topcalma=43,Atak=73,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 12. GAZİANTEP FK ─────────────────────────────────────────
                case 12:
                    t.Kaleci = new Kaleci  { isim="Dioudis",         Kalecilik=76,Hiz=38,Guc=68,Dayaniklilik=74,KararAlma=74,Vizyon=56,Sogukkanlilik=74,Agresiflik=34,Teknik=44,Pas=46,Bitiricilik=10,Topcalma=28,Atak=8, Def=56,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Ertuğrul Arslan", Def=74,Guc=76,Dayaniklilik=74,Hiz=68,KararAlma=70,Vizyon=60,Sogukkanlilik=68,Agresiflik=70,Teknik=56,Pas=56,Bitiricilik=28,Topcalma=72,Atak=36,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="N'Diaye",         Def=74,Pas=72,Dayaniklilik=74,Hiz=72,KararAlma=72,Vizyon=70,Sogukkanlilik=72,Agresiflik=66,Teknik=70,Bitiricilik=54,Topcalma=72,Atak=58,Guc=66,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Kozlowski",       Pas=76,Vizyon=74,Dayaniklilik=74,Hiz=72,KararAlma=74,Sogukkanlilik=74,Agresiflik=62,Teknik=74,Def=64,Bitiricilik=60,Topcalma=66,Atak=66,Guc=66,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Maxim",           Teknik=80,Pas=78,Dayaniklilik=74,Hiz=78,KararAlma=76,Vizyon=80,Sogukkanlilik=76,Agresiflik=52,Def=50,Bitiricilik=72,Topcalma=58,Atak=78,Guc=62,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Kenan Kodro",     Bitiricilik=77,Guc=79,Dayaniklilik=77,Hiz=79,KararAlma=73,Vizyon=69,Sogukkanlilik=75,Agresiflik=63,Teknik=71,Pas=61,Def=27,Topcalma=49,Atak=79,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 13. GÜMÜŞHANESPOR (Efsane Kurgu Takım) ───────────────────
                case 13:
                    t.Kaleci = new Kaleci  { isim="Gümüş Eldiven",   Kalecilik=88,Hiz=52,Guc=78,Dayaniklilik=85,KararAlma=86,Vizyon=70,Sogukkanlilik=88,Agresiflik=46,Teknik=62,Pas=64,Bitiricilik=10,Topcalma=42,Atak=8, Def=74,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Kelkit Duvarı",   Def=91,Guc=93,Dayaniklilik=88,Hiz=76,KararAlma=85,Vizyon=72,Sogukkanlilik=82,Agresiflik=82,Teknik=72,Pas=74,Bitiricilik=44,Topcalma=89,Atak=58,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Dinamik Torul",   Def=88,Pas=84,Dayaniklilik=88,Hiz=78,KararAlma=87,Vizyon=82,Sogukkanlilik=84,Agresiflik=84,Teknik=82,Bitiricilik=66,Topcalma=87,Atak=72,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Zigana Rüzgarı",  Pas=90,Vizyon=88,Dayaniklilik=86,Hiz=82,KararAlma=89,Sogukkanlilik=86,Agresiflik=70,Teknik=87,Def=74,Bitiricilik=72,Topcalma=77,Atak=80,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Pestil Bey",      Teknik=93,Pas=91,Dayaniklilik=84,Hiz=86,KararAlma=90,Vizyon=92,Sogukkanlilik=90,Agresiflik=62,Def=64,Bitiricilik=88,Topcalma=74,Atak=90,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Kuşburnu F.",     Bitiricilik=96,Guc=90,Dayaniklilik=90,Hiz=93,KararAlma=88,Vizyon=80,Sogukkanlilik=90,Agresiflik=77,Teknik=86,Pas=74,Def=40,Topcalma=67,Atak=95,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 14. REAL MADRID ──────────────────────────────────────────
                case 14:
                    t.Kaleci = new Kaleci  { isim="Courtois",        Kalecilik=94,Hiz=52,Guc=82,Dayaniklilik=84,KararAlma=92,Vizyon=76,Sogukkanlilik=94,Agresiflik=44,Teknik=64,Pas=68,Bitiricilik=10,Topcalma=44,Atak=8, Def=76,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Militao",         Def=92,Guc=90,Dayaniklilik=88,Hiz=84,KararAlma=90,Vizyon=76,Sogukkanlilik=86,Agresiflik=82,Teknik=76,Pas=78,Bitiricilik=44,Topcalma=90,Atak=60,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Valvarde",        Def=90,Pas=86,Dayaniklilik=90,Hiz=82,KararAlma=88,Vizyon=84,Sogukkanlilik=86,Agresiflik=84,Teknik=84,Bitiricilik=68,Topcalma=88,Atak=74,Guc=86,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Arda",            Pas=92,Vizyon=93,Dayaniklilik=92,Hiz=87,KararAlma=93,Sogukkanlilik=91,Agresiflik=78,Teknik=92,Def=78,Bitiricilik=88,Topcalma=82,Atak=90,Guc=84,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Vinicius Jr.",    Teknik=94,Pas=88,Dayaniklilik=88,Hiz=96,KararAlma=88,Vizyon=90,Sogukkanlilik=86,Agresiflik=68,Def=60,Bitiricilik=90,Topcalma=78,Atak=94,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Mbappé",          Bitiricilik=97,Guc=86,Dayaniklilik=92,Hiz=98,KararAlma=93,Vizyon=92,Sogukkanlilik=95,Agresiflik=74,Teknik=94,Pas=86,Def=40,Topcalma=68,Atak=97,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 15. BARCELONA ────────────────────────────────────────────
                case 15:
                    t.Kaleci = new Kaleci  { isim="Szczęsny",        Kalecilik=90,Hiz=46,Guc=78,Dayaniklilik=82,KararAlma=90,Vizyon=72,Sogukkanlilik=90,Agresiflik=42,Teknik=60,Pas=64,Bitiricilik=10,Topcalma=42,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Araujo",          Def=91,Guc=91,Dayaniklilik=88,Hiz=86,KararAlma=88,Vizyon=74,Sogukkanlilik=84,Agresiflik=84,Teknik=74,Pas=76,Bitiricilik=42,Topcalma=89,Atak=58,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="De Jong",         Def=86,Pas=90,Dayaniklilik=90,Hiz=82,KararAlma=90,Vizyon=88,Sogukkanlilik=88,Agresiflik=74,Teknik=90,Bitiricilik=70,Topcalma=84,Atak=76,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Pedri",           Pas=93,Vizyon=93,Dayaniklilik=88,Hiz=84,KararAlma=93,Sogukkanlilik=90,Agresiflik=70,Teknik=94,Def=72,Bitiricilik=80,Topcalma=78,Atak=84,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Lamine Yamal",    Teknik=95,Pas=89,Dayaniklilik=86,Hiz=92,KararAlma=89,Vizyon=92,Sogukkanlilik=88,Agresiflik=62,Def=58,Bitiricilik=88,Topcalma=76,Atak=92,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Lewandowski",     Bitiricilik=93,Guc=84,Dayaniklilik=88,Hiz=82,KararAlma=93,Vizyon=86,Sogukkanlilik=93,Agresiflik=70,Teknik=88,Pas=80,Def=36,Topcalma=62,Atak=92,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 16. MANCHESTER CITY ──────────────────────────────────────
                case 16:
                    t.Kaleci = new Kaleci  { isim="Ederson",         Kalecilik=91,Hiz=50,Guc=80,Dayaniklilik=84,KararAlma=90,Vizyon=78,Sogukkanlilik=90,Agresiflik=42,Teknik=64,Pas=74,Bitiricilik=10,Topcalma=42,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Dias",            Def=92,Guc=88,Dayaniklilik=88,Hiz=82,KararAlma=90,Vizyon=76,Sogukkanlilik=88,Agresiflik=80,Teknik=76,Pas=80,Bitiricilik=42,Topcalma=90,Atak=58,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Rodri",           Def=90,Pas=93,Dayaniklilik=92,Hiz=80,KararAlma=95,Vizyon=92,Sogukkanlilik=90,Agresiflik=76,Teknik=90,Bitiricilik=72,Topcalma=88,Atak=76,Guc=82,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="De Bruyne",       Pas=97,Vizyon=97,Dayaniklilik=88,Hiz=84,KararAlma=95,Sogukkanlilik=92,Agresiflik=72,Teknik=94,Def=72,Bitiricilik=86,Topcalma=78,Atak=88,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Bernardo Silva",  Teknik=93,Pas=91,Dayaniklilik=90,Hiz=88,KararAlma=91,Vizyon=91,Sogukkanlilik=88,Agresiflik=64,Def=64,Bitiricilik=84,Topcalma=74,Atak=88,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Haaland",         Bitiricilik=98,Guc=92,Dayaniklilik=92,Hiz=93,KararAlma=90,Vizyon=82,Sogukkanlilik=93,Agresiflik=78,Teknik=84,Pas=74,Def=36,Topcalma=62,Atak=97,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 17. LIVERPOOL ────────────────────────────────────────────
                case 17:
                    t.Kaleci = new Kaleci  { isim="Alisson",         Kalecilik=93,Hiz=48,Guc=78,Dayaniklilik=82,KararAlma=91,Vizyon=76,Sogukkanlilik=92,Agresiflik=42,Teknik=62,Pas=70,Bitiricilik=10,Topcalma=42,Atak=8, Def=74,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Van Dijk",        Def=93,Guc=91,Dayaniklilik=88,Hiz=82,KararAlma=91,Vizyon=76,Sogukkanlilik=88,Agresiflik=80,Teknik=76,Pas=78,Bitiricilik=42,Topcalma=91,Atak=58,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Mac Allister",    Def=84,Pas=88,Dayaniklilik=88,Hiz=80,KararAlma=88,Vizyon=86,Sogukkanlilik=86,Agresiflik=74,Teknik=86,Bitiricilik=72,Topcalma=82,Atak=76,Guc=78,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Szoboszlai",      Pas=88,Vizyon=88,Dayaniklilik=88,Hiz=86,KararAlma=87,Sogukkanlilik=85,Agresiflik=76,Teknik=88,Def=72,Bitiricilik=82,Topcalma=76,Atak=86,Guc=78,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Salah",           Teknik=92,Pas=86,Dayaniklilik=90,Hiz=94,KararAlma=90,Vizyon=89,Sogukkanlilik=90,Agresiflik=66,Def=58,Bitiricilik=93,Topcalma=76,Atak=93,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Núñez",           Bitiricilik=88,Guc=86,Dayaniklilik=88,Hiz=90,KararAlma=82,Vizyon=78,Sogukkanlilik=84,Agresiflik=74,Teknik=82,Pas=72,Def=34,Topcalma=60,Atak=90,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 18. BAYERN MÜNCHEN ───────────────────────────────────────
                case 18:
                    t.Kaleci = new Kaleci  { isim="Neuer",           Kalecilik=92,Hiz=48,Guc=80,Dayaniklilik=82,KararAlma=92,Vizyon=82,Sogukkanlilik=92,Agresiflik=42,Teknik=66,Pas=72,Bitiricilik=10,Topcalma=44,Atak=8, Def=76,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Kim Min-jae",     Def=91,Guc=90,Dayaniklilik=88,Hiz=82,KararAlma=88,Vizyon=74,Sogukkanlilik=86,Agresiflik=82,Teknik=74,Pas=76,Bitiricilik=42,Topcalma=89,Atak=56,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Kimmich",         Def=88,Pas=93,Dayaniklilik=90,Hiz=82,KararAlma=93,Vizyon=91,Sogukkanlilik=90,Agresiflik=78,Teknik=90,Bitiricilik=74,Topcalma=86,Atak=78,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Goretzka",        Pas=86,Vizyon=86,Dayaniklilik=90,Hiz=84,KararAlma=87,Sogukkanlilik=84,Agresiflik=80,Teknik=86,Def=78,Bitiricilik=80,Topcalma=80,Atak=82,Guc=88,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Müller",          Teknik=88,Pas=88,Dayaniklilik=88,Hiz=82,KararAlma=92,Vizyon=94,Sogukkanlilik=88,Agresiflik=66,Def=62,Bitiricilik=86,Topcalma=74,Atak=88,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Kane",            Bitiricilik=94,Guc=86,Dayaniklilik=90,Hiz=84,KararAlma=94,Vizyon=90,Sogukkanlilik=94,Agresiflik=72,Teknik=88,Pas=86,Def=36,Topcalma=62,Atak=94,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 19. PSG ──────────────────────────────────────────────────
                case 19:
                    t.Kaleci = new Kaleci  { isim="Donnarumma",      Kalecilik=92,Hiz=50,Guc=82,Dayaniklilik=84,KararAlma=90,Vizyon=74,Sogukkanlilik=90,Agresiflik=44,Teknik=62,Pas=66,Bitiricilik=10,Topcalma=44,Atak=8, Def=76,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Marquinhos",      Def=90,Guc=86,Dayaniklilik=86,Hiz=80,KararAlma=90,Vizyon=76,Sogukkanlilik=88,Agresiflik=78,Teknik=76,Pas=78,Bitiricilik=40,Topcalma=88,Atak=56,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Vitinha",         Def=82,Pas=90,Dayaniklilik=88,Hiz=84,KararAlma=90,Vizyon=88,Sogukkanlilik=88,Agresiflik=72,Teknik=90,Bitiricilik=72,Topcalma=80,Atak=78,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Neves",           Pas=89,Vizyon=88,Dayaniklilik=88,Hiz=80,KararAlma=88,Sogukkanlilik=86,Agresiflik=74,Teknik=88,Def=76,Bitiricilik=76,Topcalma=78,Atak=78,Guc=78,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Dembélé",         Teknik=92,Pas=84,Dayaniklilik=84,Hiz=95,KararAlma=84,Vizyon=86,Sogukkanlilik=82,Agresiflik=66,Def=58,Bitiricilik=88,Topcalma=76,Atak=92,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Asensio",         Bitiricilik=86,Guc=80,Dayaniklilik=84,Hiz=86,KararAlma=86,Vizyon=86,Sogukkanlilik=88,Agresiflik=66,Teknik=88,Pas=82,Def=34,Topcalma=60,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 20. ARSENAL ──────────────────────────────────────────────
                case 20:
                    t.Kaleci = new Kaleci  { isim="Raya",            Kalecilik=88,Hiz=46,Guc=76,Dayaniklilik=82,KararAlma=88,Vizyon=74,Sogukkanlilik=88,Agresiflik=40,Teknik=60,Pas=68,Bitiricilik=10,Topcalma=40,Atak=8, Def=70,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Saliba",          Def=90,Guc=88,Dayaniklilik=88,Hiz=84,KararAlma=88,Vizyon=76,Sogukkanlilik=86,Agresiflik=78,Teknik=76,Pas=78,Bitiricilik=40,Topcalma=88,Atak=56,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Thomas Partey",   Def=88,Pas=83,Dayaniklilik=88,Hiz=80,KararAlma=86,Vizyon=82,Sogukkanlilik=82,Agresiflik=82,Teknik=82,Bitiricilik=64,Topcalma=86,Atak=70,Guc=84,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Ødegaard",        Pas=93,Vizyon=94,Dayaniklilik=86,Hiz=84,KararAlma=93,Sogukkanlilik=90,Agresiflik=68,Teknik=93,Def=68,Bitiricilik=82,Topcalma=76,Atak=86,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Saka",            Teknik=91,Pas=87,Dayaniklilik=88,Hiz=90,KararAlma=88,Vizyon=88,Sogukkanlilik=87,Agresiflik=64,Def=60,Bitiricilik=88,Topcalma=74,Atak=90,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Havertz",         Bitiricilik=86,Guc=84,Dayaniklilik=86,Hiz=82,KararAlma=87,Vizyon=84,Sogukkanlilik=87,Agresiflik=70,Teknik=87,Pas=80,Def=36,Topcalma=62,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 21. CHELSEA ──────────────────────────────────────────────
                case 21:
                    t.Kaleci = new Kaleci  { isim="Sánchez",         Kalecilik=85,Hiz=44,Guc=74,Dayaniklilik=80,KararAlma=84,Vizyon=70,Sogukkanlilik=84,Agresiflik=40,Teknik=58,Pas=62,Bitiricilik=10,Topcalma=40,Atak=8, Def=68,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Chalobah",        Def=82,Guc=84,Dayaniklilik=84,Hiz=78,KararAlma=80,Vizyon=70,Sogukkanlilik=78,Agresiflik=76,Teknik=70,Pas=70,Bitiricilik=36,Topcalma=82,Atak=48,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Caicedo",         Def=86,Pas=83,Dayaniklilik=88,Hiz=82,KararAlma=84,Vizyon=80,Sogukkanlilik=82,Agresiflik=80,Teknik=80,Bitiricilik=64,Topcalma=84,Atak=70,Guc=82,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Enzo F.",         Pas=86,Vizyon=86,Dayaniklilik=84,Hiz=80,KararAlma=86,Sogukkanlilik=84,Agresiflik=72,Teknik=86,Def=72,Bitiricilik=74,Topcalma=74,Atak=78,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Palmer",          Teknik=91,Pas=87,Dayaniklilik=84,Hiz=87,KararAlma=88,Vizyon=90,Sogukkanlilik=88,Agresiflik=62,Def=60,Bitiricilik=89,Topcalma=72,Atak=89,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Nicolas Jackson", Bitiricilik=84,Guc=82,Dayaniklilik=84,Hiz=88,KararAlma=80,Vizyon=76,Sogukkanlilik=80,Agresiflik=70,Teknik=80,Pas=68,Def=32,Topcalma=56,Atak=86,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 22. JUVENTUS ─────────────────────────────────────────────
                case 22:
                    t.Kaleci = new Kaleci  { isim="Di Gregorio",     Kalecilik=84,Hiz=44,Guc=74,Dayaniklilik=80,KararAlma=83,Vizyon=68,Sogukkanlilik=84,Agresiflik=40,Teknik=56,Pas=60,Bitiricilik=10,Topcalma=40,Atak=8, Def=66,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Bremer",          Def=88,Guc=88,Dayaniklilik=86,Hiz=78,KararAlma=84,Vizyon=70,Sogukkanlilik=82,Agresiflik=80,Teknik=70,Pas=72,Bitiricilik=38,Topcalma=86,Atak=50,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Locatelli",       Def=82,Pas=84,Dayaniklilik=84,Hiz=76,KararAlma=84,Vizyon=82,Sogukkanlilik=82,Agresiflik=72,Teknik=82,Bitiricilik=64,Topcalma=80,Atak=68,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Rabiot",          Pas=84,Vizyon=83,Dayaniklilik=86,Hiz=78,KararAlma=83,Sogukkanlilik=80,Agresiflik=74,Teknik=83,Def=74,Bitiricilik=72,Topcalma=76,Atak=74,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Yıldız",          Teknik=89,Pas=83,Dayaniklilik=82,Hiz=86,KararAlma=83,Vizyon=86,Sogukkanlilik=84,Agresiflik=62,Def=58,Bitiricilik=84,Topcalma=70,Atak=86,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Vlahovic",        Bitiricilik=89,Guc=86,Dayaniklilik=84,Hiz=80,KararAlma=84,Vizyon=76,Sogukkanlilik=86,Agresiflik=72,Teknik=80,Pas=68,Def=34,Topcalma=56,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 23. INTER MILAN ──────────────────────────────────────────
                case 23:
                    t.Kaleci = new Kaleci  { isim="Sommer",          Kalecilik=89,Hiz=44,Guc=76,Dayaniklilik=82,KararAlma=89,Vizyon=72,Sogukkanlilik=89,Agresiflik=40,Teknik=60,Pas=64,Bitiricilik=10,Topcalma=42,Atak=8, Def=70,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Bastoni",         Def=89,Guc=84,Dayaniklilik=86,Hiz=80,KararAlma=89,Vizyon=80,Sogukkanlilik=87,Agresiflik=76,Teknik=80,Pas=84,Bitiricilik=42,Topcalma=87,Atak=60,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Barella",         Def=84,Pas=89,Dayaniklilik=90,Hiz=84,KararAlma=89,Vizyon=88,Sogukkanlilik=86,Agresiflik=78,Teknik=88,Bitiricilik=76,Topcalma=82,Atak=80,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Calhanoglu",      Pas=91,Vizyon=90,Dayaniklilik=86,Hiz=78,KararAlma=90,Sogukkanlilik=88,Agresiflik=72,Teknik=88,Def=72,Bitiricilik=80,Topcalma=74,Atak=82,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Mkhitaryan",      Teknik=87,Pas=87,Dayaniklilik=84,Hiz=80,KararAlma=88,Vizyon=88,Sogukkanlilik=86,Agresiflik=64,Def=62,Bitiricilik=82,Topcalma=70,Atak=84,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Lautaro M.",      Bitiricilik=92,Guc=86,Dayaniklilik=88,Hiz=84,KararAlma=91,Vizyon=82,Sogukkanlilik=90,Agresiflik=74,Teknik=86,Pas=76,Def=36,Topcalma=62,Atak=92,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 24. AC MILAN ─────────────────────────────────────────────
                case 24:
                    t.Kaleci = new Kaleci  { isim="Maignan",         Kalecilik=90,Hiz=50,Guc=78,Dayaniklilik=82,KararAlma=89,Vizyon=74,Sogukkanlilik=90,Agresiflik=42,Teknik=62,Pas=66,Bitiricilik=10,Topcalma=42,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Tomori",          Def=87,Guc=86,Dayaniklilik=86,Hiz=84,KararAlma=84,Vizyon=70,Sogukkanlilik=82,Agresiflik=78,Teknik=72,Pas=72,Bitiricilik=40,Topcalma=86,Atak=52,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Reijnders",       Def=80,Pas=87,Dayaniklilik=88,Hiz=82,KararAlma=87,Vizyon=86,Sogukkanlilik=85,Agresiflik=72,Teknik=87,Bitiricilik=74,Topcalma=78,Atak=78,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Bennacer",        Pas=84,Vizyon=82,Dayaniklilik=84,Hiz=80,KararAlma=84,Sogukkanlilik=82,Agresiflik=74,Teknik=84,Def=76,Bitiricilik=68,Topcalma=78,Atak=72,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Pulisic",         Teknik=88,Pas=83,Dayaniklilik=84,Hiz=88,KararAlma=84,Vizyon=84,Sogukkanlilik=84,Agresiflik=62,Def=60,Bitiricilik=86,Topcalma=72,Atak=88,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Morata",          Bitiricilik=84,Guc=82,Dayaniklilik=84,Hiz=82,KararAlma=84,Vizyon=80,Sogukkanlilik=83,Agresiflik=70,Teknik=82,Pas=74,Def=34,Topcalma=58,Atak=84,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 25. ATLETİCO MADRİD ──────────────────────────────────────
                case 25:
                    t.Kaleci = new Kaleci  { isim="Oblak",           Kalecilik=93,Hiz=46,Guc=78,Dayaniklilik=84,KararAlma=92,Vizyon=74,Sogukkanlilik=93,Agresiflik=42,Teknik=62,Pas=64,Bitiricilik=10,Topcalma=44,Atak=8, Def=74,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Giménez",         Def=91,Guc=88,Dayaniklilik=86,Hiz=78,KararAlma=88,Vizyon=72,Sogukkanlilik=84,Agresiflik=84,Teknik=72,Pas=72,Bitiricilik=40,Topcalma=89,Atak=52,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Koke",            Def=84,Pas=88,Dayaniklilik=88,Hiz=78,KararAlma=90,Vizyon=88,Sogukkanlilik=88,Agresiflik=76,Teknik=87,Bitiricilik=70,Topcalma=82,Atak=76,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="De Paul",         Pas=87,Vizyon=85,Dayaniklilik=88,Hiz=80,KararAlma=86,Sogukkanlilik=84,Agresiflik=78,Teknik=86,Def=74,Bitiricilik=76,Topcalma=78,Atak=80,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Lino",            Teknik=87,Pas=84,Dayaniklilik=84,Hiz=88,KararAlma=84,Vizyon=84,Sogukkanlilik=82,Agresiflik=64,Def=62,Bitiricilik=82,Topcalma=72,Atak=86,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Griezmann",       Bitiricilik=91,Guc=80,Dayaniklilik=88,Hiz=84,KararAlma=92,Vizyon=88,Sogukkanlilik=92,Agresiflik=70,Teknik=90,Pas=84,Def=36,Topcalma=64,Atak=90,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 26. BORUSSIA DORTMUND ────────────────────────────────────
                case 26:
                    t.Kaleci = new Kaleci  { isim="Kobel",           Kalecilik=87,Hiz=46,Guc=76,Dayaniklilik=82,KararAlma=86,Vizyon=70,Sogukkanlilik=86,Agresiflik=40,Teknik=58,Pas=62,Bitiricilik=10,Topcalma=40,Atak=8, Def=68,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Hummels",         Def=88,Guc=84,Dayaniklilik=82,Hiz=74,KararAlma=90,Vizyon=80,Sogukkanlilik=88,Agresiflik=76,Teknik=78,Pas=80,Bitiricilik=38,Topcalma=86,Atak=52,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Emre Can",        Def=84,Pas=80,Dayaniklilik=88,Hiz=78,KararAlma=82,Vizyon=78,Sogukkanlilik=80,Agresiflik=80,Teknik=78,Bitiricilik=64,Topcalma=82,Atak=68,Guc=84,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Brandt",          Pas=86,Vizyon=86,Dayaniklilik=84,Hiz=82,KararAlma=85,Sogukkanlilik=83,Agresiflik=68,Teknik=87,Def=68,Bitiricilik=76,Topcalma=72,Atak=80,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Sancho",          Teknik=89,Pas=83,Dayaniklilik=82,Hiz=90,KararAlma=82,Vizyon=84,Sogukkanlilik=80,Agresiflik=64,Def=58,Bitiricilik=84,Topcalma=74,Atak=88,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Füllkrug",        Bitiricilik=87,Guc=88,Dayaniklilik=86,Hiz=78,KararAlma=84,Vizyon=76,Sogukkanlilik=85,Agresiflik=74,Teknik=78,Pas=68,Def=36,Topcalma=58,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 27. AJAX ─────────────────────────────────────────────────
                case 27:
                    t.Kaleci = new Kaleci  { isim="Flekken",         Kalecilik=83,Hiz=44,Guc=72,Dayaniklilik=80,KararAlma=82,Vizyon=68,Sogukkanlilik=82,Agresiflik=38,Teknik=56,Pas=60,Bitiricilik=10,Topcalma=38,Atak=8, Def=64,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Sutalo",          Def=82,Guc=82,Dayaniklilik=82,Hiz=76,KararAlma=80,Vizyon=70,Sogukkanlilik=78,Agresiflik=74,Teknik=68,Pas=70,Bitiricilik=34,Topcalma=80,Atak=46,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Berghuis",        Def=78,Pas=82,Dayaniklilik=82,Hiz=78,KararAlma=82,Vizyon=82,Sogukkanlilik=80,Agresiflik=70,Teknik=82,Bitiricilik=68,Topcalma=76,Atak=72,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Henderson",       Pas=83,Vizyon=80,Dayaniklilik=82,Hiz=76,KararAlma=83,Sogukkanlilik=80,Agresiflik=72,Teknik=80,Def=72,Bitiricilik=66,Topcalma=72,Atak=70,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Brobbey",         Teknik=82,Pas=76,Dayaniklilik=82,Hiz=86,KararAlma=78,Vizyon=76,Sogukkanlilik=78,Agresiflik=70,Def=60,Bitiricilik=80,Topcalma=68,Atak=82,Guc=82,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Weghorst",        Bitiricilik=80,Guc=84,Dayaniklilik=82,Hiz=76,KararAlma=78,Vizyon=72,Sogukkanlilik=78,Agresiflik=70,Teknik=72,Pas=62,Def=30,Topcalma=50,Atak=80,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 28. TÜRKİYE MİLLİ TAKIM ─────────────────────────────────
                case 28:
                    t.Kaleci = new Kaleci  { isim="Günay Güvenç",    Kalecilik=86,Hiz=48,Guc=74,Dayaniklilik=82,KararAlma=84,Vizyon=68,Sogukkanlilik=84,Agresiflik=40,Teknik=58,Pas=60,Bitiricilik=10,Topcalma=38,Atak=8, Def=68,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Samet Akaydın",   Def=82,Guc=82,Dayaniklilik=82,Hiz=74,KararAlma=80,Vizyon=68,Sogukkanlilik=78,Agresiflik=76,Teknik=66,Pas=68,Bitiricilik=36,Topcalma=80,Atak=46,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Hakan Çalhanoğlu",Def=80,Pas=91,Dayaniklilik=86,Hiz=78,KararAlma=90,Vizyon=89,Sogukkanlilik=88,Agresiflik=72,Teknik=88,Bitiricilik=78,Topcalma=74,Atak=80,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Kaan Ayhan",      Pas=78,Vizyon=76,Dayaniklilik=82,Hiz=74,KararAlma=80,Sogukkanlilik=78,Agresiflik=74,Teknik=76,Def=76,Bitiricilik=60,Topcalma=76,Atak=66,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Arda Güler",      Teknik=93,Pas=88,Dayaniklilik=84,Hiz=87,KararAlma=90,Vizyon=93,Sogukkanlilik=89,Agresiflik=62,Def=58,Bitiricilik=88,Topcalma=72,Atak=90,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Kerem Aktürkoğlu", Bitiricilik=86,Guc=80,Dayaniklilik=84,Hiz=90,KararAlma=82,Vizyon=80,Sogukkanlilik=82,Agresiflik=70,Teknik=86,Pas=76,Def=32,Topcalma=60,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 29. FRANSA ───────────────────────────────────────────────
                case 29:
                    t.Kaleci = new Kaleci  { isim="Maignan",         Kalecilik=91,Hiz=50,Guc=78,Dayaniklilik=84,KararAlma=90,Vizyon=74,Sogukkanlilik=90,Agresiflik=42,Teknik=62,Pas=66,Bitiricilik=10,Topcalma=42,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Upamecano",       Def=88,Guc=88,Dayaniklilik=86,Hiz=84,KararAlma=85,Vizyon=72,Sogukkanlilik=82,Agresiflik=80,Teknik=72,Pas=74,Bitiricilik=38,Topcalma=86,Atak=52,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Camavinga",       Def=84,Pas=86,Dayaniklilik=90,Hiz=86,KararAlma=86,Vizyon=84,Sogukkanlilik=84,Agresiflik=78,Teknik=86,Bitiricilik=70,Topcalma=82,Atak=74,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Tchouameni",      Pas=86,Vizyon=86,Dayaniklilik=90,Hiz=82,KararAlma=88,Sogukkanlilik=86,Agresiflik=80,Teknik=84,Def=88,Bitiricilik=68,Topcalma=86,Atak=72,Guc=86,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Dembélé",         Teknik=92,Pas=84,Dayaniklilik=84,Hiz=96,KararAlma=83,Vizyon=85,Sogukkanlilik=81,Agresiflik=66,Def=58,Bitiricilik=87,Topcalma=76,Atak=92,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Mbappé",          Bitiricilik=97,Guc=87,Dayaniklilik=92,Hiz=98,KararAlma=93,Vizyon=92,Sogukkanlilik=95,Agresiflik=74,Teknik=94,Pas=87,Def=40,Topcalma=68,Atak=97,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 30. İNGİLTERE ────────────────────────────────────────────
                case 30:
                    t.Kaleci = new Kaleci  { isim="Pickford",        Kalecilik=86,Hiz=46,Guc=74,Dayaniklilik=82,KararAlma=84,Vizyon=70,Sogukkanlilik=82,Agresiflik=40,Teknik=58,Pas=62,Bitiricilik=10,Topcalma=38,Atak=8, Def=66,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Walker",          Def=84,Guc=82,Dayaniklilik=86,Hiz=90,KararAlma=82,Vizyon=74,Sogukkanlilik=80,Agresiflik=74,Teknik=72,Pas=74,Bitiricilik=44,Topcalma=82,Atak=62,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Rice",            Def=88,Pas=87,Dayaniklilik=90,Hiz=82,KararAlma=88,Vizyon=84,Sogukkanlilik=84,Agresiflik=78,Teknik=84,Bitiricilik=70,Topcalma=86,Atak=72,Guc=82,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Bellingham",      Pas=91,Vizyon=92,Dayaniklilik=92,Hiz=87,KararAlma=92,Sogukkanlilik=90,Agresiflik=78,Teknik=91,Def=76,Bitiricilik=88,Topcalma=80,Atak=90,Guc=84,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Saka",            Teknik=91,Pas=87,Dayaniklilik=88,Hiz=90,KararAlma=88,Vizyon=88,Sogukkanlilik=87,Agresiflik=64,Def=60,Bitiricilik=88,Topcalma=74,Atak=90,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Kane",            Bitiricilik=94,Guc=86,Dayaniklilik=90,Hiz=84,KararAlma=94,Vizyon=90,Sogukkanlilik=94,Agresiflik=72,Teknik=88,Pas=86,Def=36,Topcalma=62,Atak=94,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 31. ALMANYA ──────────────────────────────────────────────
                case 31:
                    t.Kaleci = new Kaleci  { isim="Ter Stegen",      Kalecilik=91,Hiz=48,Guc=78,Dayaniklilik=82,KararAlma=90,Vizyon=78,Sogukkanlilik=90,Agresiflik=40,Teknik=64,Pas=70,Bitiricilik=10,Topcalma=42,Atak=8, Def=72,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Rüdiger",         Def=89,Guc=90,Dayaniklilik=88,Hiz=82,KararAlma=86,Vizyon=72,Sogukkanlilik=84,Agresiflik=84,Teknik=72,Pas=72,Bitiricilik=40,Topcalma=88,Atak=54,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Kroos",           Def=78,Pas=96,Dayaniklilik=86,Hiz=76,KararAlma=96,Vizyon=96,Sogukkanlilik=92,Agresiflik=68,Teknik=93,Bitiricilik=74,Topcalma=74,Atak=78,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Wirtz",           Pas=90,Vizyon=92,Dayaniklilik=88,Hiz=88,KararAlma=91,Sogukkanlilik=88,Agresiflik=68,Teknik=93,Def=68,Bitiricilik=86,Topcalma=76,Atak=88,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Musiala",         Teknik=93,Pas=88,Dayaniklilik=88,Hiz=90,KararAlma=90,Vizyon=90,Sogukkanlilik=88,Agresiflik=64,Def=60,Bitiricilik=86,Topcalma=76,Atak=90,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Havertz",         Bitiricilik=86,Guc=84,Dayaniklilik=86,Hiz=82,KararAlma=87,Vizyon=84,Sogukkanlilik=87,Agresiflik=70,Teknik=87,Pas=80,Def=36,Topcalma=62,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 32. İSPANYA ──────────────────────────────────────────────
                case 32:
                    t.Kaleci = new Kaleci  { isim="Raya",            Kalecilik=88,Hiz=46,Guc=76,Dayaniklilik=82,KararAlma=88,Vizyon=74,Sogukkanlilik=88,Agresiflik=40,Teknik=60,Pas=68,Bitiricilik=10,Topcalma=40,Atak=8, Def=70,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Le Normand",      Def=87,Guc=84,Dayaniklilik=86,Hiz=78,KararAlma=86,Vizyon=74,Sogukkanlilik=84,Agresiflik=76,Teknik=74,Pas=76,Bitiricilik=38,Topcalma=85,Atak=50,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Rodri",           Def=90,Pas=93,Dayaniklilik=92,Hiz=80,KararAlma=95,Vizyon=92,Sogukkanlilik=90,Agresiflik=76,Teknik=90,Bitiricilik=72,Topcalma=88,Atak=76,Guc=82,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Pedri",           Pas=92,Vizyon=92,Dayaniklilik=88,Hiz=84,KararAlma=92,Sogukkanlilik=89,Agresiflik=70,Teknik=93,Def=70,Bitiricilik=80,Topcalma=78,Atak=84,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Yamal",           Teknik=94,Pas=89,Dayaniklilik=86,Hiz=93,KararAlma=89,Vizyon=92,Sogukkanlilik=88,Agresiflik=62,Def=58,Bitiricilik=88,Topcalma=76,Atak=92,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Morata",          Bitiricilik=84,Guc=82,Dayaniklilik=84,Hiz=82,KararAlma=84,Vizyon=80,Sogukkanlilik=83,Agresiflik=70,Teknik=82,Pas=74,Def=34,Topcalma=58,Atak=84,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 33. BREZİLYA ─────────────────────────────────────────────
                case 33:
                    t.Kaleci = new Kaleci  { isim="Alisson",         Kalecilik=93,Hiz=48,Guc=78,Dayaniklilik=82,KararAlma=91,Vizyon=76,Sogukkanlilik=92,Agresiflik=42,Teknik=62,Pas=70,Bitiricilik=10,Topcalma=42,Atak=8, Def=74,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Militao",         Def=91,Guc=90,Dayaniklilik=88,Hiz=84,KararAlma=89,Vizyon=76,Sogukkanlilik=86,Agresiflik=82,Teknik=76,Pas=78,Bitiricilik=44,Topcalma=90,Atak=60,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Casemiro",        Def=90,Pas=83,Dayaniklilik=88,Hiz=78,KararAlma=88,Vizyon=82,Sogukkanlilik=84,Agresiflik=84,Teknik=80,Bitiricilik=66,Topcalma=88,Atak=70,Guc=86,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Paquetá",         Pas=88,Vizyon=88,Dayaniklilik=86,Hiz=82,KararAlma=87,Sogukkanlilik=85,Agresiflik=72,Teknik=88,Def=70,Bitiricilik=78,Topcalma=74,Atak=82,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Rodrygo",         Teknik=90,Pas=84,Dayaniklilik=86,Hiz=91,KararAlma=85,Vizyon=86,Sogukkanlilik=84,Agresiflik=64,Def=60,Bitiricilik=87,Topcalma=74,Atak=90,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Vinicius Jr.",    Bitiricilik=92,Guc=80,Dayaniklilik=88,Hiz=96,KararAlma=87,Vizyon=88,Sogukkanlilik=85,Agresiflik=72,Teknik=93,Pas=82,Def=38,Topcalma=72,Atak=93,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 34. ARJANTİN ─────────────────────────────────────────────
                case 34:
                    t.Kaleci = new Kaleci  { isim="E. Martínez",     Kalecilik=92,Hiz=46,Guc=78,Dayaniklilik=84,KararAlma=92,Vizyon=76,Sogukkanlilik=93,Agresiflik=44,Teknik=62,Pas=66,Bitiricilik=10,Topcalma=44,Atak=8, Def=74,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Romero",          Def=89,Guc=88,Dayaniklilik=86,Hiz=80,KararAlma=87,Vizyon=74,Sogukkanlilik=84,Agresiflik=84,Teknik=74,Pas=74,Bitiricilik=40,Topcalma=87,Atak=54,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="De Paul",         Def=82,Pas=87,Dayaniklilik=88,Hiz=80,KararAlma=86,Vizyon=85,Sogukkanlilik=84,Agresiflik=78,Teknik=86,Bitiricilik=74,Topcalma=78,Atak=78,Guc=80,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Mac Allister",    Pas=88,Vizyon=86,Dayaniklilik=88,Hiz=80,KararAlma=87,Sogukkanlilik=86,Agresiflik=74,Teknik=86,Def=82,Bitiricilik=72,Topcalma=76,Atak=76,Guc=78,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="Di María",        Teknik=90,Pas=88,Dayaniklilik=82,Hiz=88,KararAlma=88,Vizyon=90,Sogukkanlilik=86,Agresiflik=66,Def=58,Bitiricilik=86,Topcalma=72,Atak=90,Guc=70,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Messi",           Bitiricilik=94,Guc=74,Dayaniklilik=84,Hiz=82,KararAlma=97,Vizyon=98,Sogukkanlilik=96,Agresiflik=68,Teknik=98,Pas=96,Def=36,Topcalma=80,Atak=93,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 35. PORTEKİZ ─────────────────────────────────────────────
                case 35:
                    t.Kaleci = new Kaleci  { isim="Costa",           Kalecilik=86,Hiz=44,Guc=74,Dayaniklilik=80,KararAlma=84,Vizyon=70,Sogukkanlilik=84,Agresiflik=38,Teknik=58,Pas=62,Bitiricilik=10,Topcalma=38,Atak=8, Def=68,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Pepe",            Def=84,Guc=84,Dayaniklilik=80,Hiz=72,KararAlma=84,Vizyon=70,Sogukkanlilik=82,Agresiflik=84,Teknik=70,Pas=70,Bitiricilik=36,Topcalma=82,Atak=48,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Vitinha",         Def=80,Pas=90,Dayaniklilik=88,Hiz=82,KararAlma=88,Vizyon=87,Sogukkanlilik=86,Agresiflik=70,Teknik=89,Bitiricilik=70,Topcalma=78,Atak=76,Guc=74,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Bruno F.",        Pas=93,Vizyon=92,Dayaniklilik=88,Hiz=80,KararAlma=93,Sogukkanlilik=90,Agresiflik=72,Teknik=91,Def=72,Bitiricilik=84,Topcalma=76,Atak=86,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="B. Silva",        Teknik=93,Pas=91,Dayaniklilik=90,Hiz=88,KararAlma=91,Vizyon=91,Sogukkanlilik=88,Agresiflik=64,Def=64,Bitiricilik=84,Topcalma=74,Atak=88,Guc=72,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Cristiano Ronaldo",Bitiricilik=90,Guc=84,Dayaniklilik=90,Hiz=84,KararAlma=90,Vizyon=84,Sogukkanlilik=92,Agresiflik=72,Teknik=88,Pas=76,Def=34,Topcalma=60,Atak=92,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                // ── 36. KOLOMBİYA ────────────────────────────────────────────
                case 36:
                    t.Kaleci = new Kaleci  { isim="Vargas",          Kalecilik=82,Hiz=44,Guc=72,Dayaniklilik=80,KararAlma=80,Vizyon=66,Sogukkanlilik=80,Agresiflik=38,Teknik=54,Pas=56,Bitiricilik=10,Topcalma=36,Atak=8, Def=64,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Cuesta",          Def=80,Guc=80,Dayaniklilik=80,Hiz=74,KararAlma=76,Vizyon=66,Sogukkanlilik=74,Agresiflik=74,Teknik=62,Pas=62,Bitiricilik=30,Topcalma=78,Atak=42,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="Lerma",           Def=80,Pas=78,Dayaniklilik=82,Hiz=76,KararAlma=78,Vizyon=76,Sogukkanlilik=76,Agresiflik=74,Teknik=76,Bitiricilik=60,Topcalma=78,Atak=64,Guc=76,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="Cuadrado",        Pas=82,Vizyon=83,Dayaniklilik=80,Hiz=84,KararAlma=82,Sogukkanlilik=80,Agresiflik=68,Teknik=83,Def=66,Bitiricilik=72,Topcalma=70,Atak=80,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="James Rodríguez", Teknik=88,Pas=90,Dayaniklilik=80,Hiz=78,KararAlma=88,Vizyon=92,Sogukkanlilik=86,Agresiflik=60,Def=56,Bitiricilik=80,Topcalma=66,Atak=82,Guc=68,Kalecilik=8, Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Díaz L.",         Bitiricilik=84,Guc=80,Dayaniklilik=84,Hiz=90,KararAlma=82,Vizyon=80,Sogukkanlilik=82,Agresiflik=68,Teknik=86,Pas=76,Def=30,Topcalma=58,Atak=88,Kalecilik=5, Moral=100,Kondisyon=100 };
                    break;

                default:
                    t.Kaleci = new Kaleci  { isim="Kaleci",   Kalecilik=75,Hiz=40,Guc=70,Dayaniklilik=75,KararAlma=75,Vizyon=60,Sogukkanlilik=75,Agresiflik=35,Teknik=50,Pas=50,Bitiricilik=10,Topcalma=30,Atak=8,Def=60,Moral=100,Kondisyon=100 };
                    t.Defans = new Defans  { isim="Defans",   Def=75,Guc=75,Dayaniklilik=75,Hiz=70,KararAlma=70,Vizyon=65,Sogukkanlilik=70,Agresiflik=70,Teknik=62,Pas=62,Bitiricilik=30,Topcalma=74,Atak=40,Kalecilik=8,Moral=100,Kondisyon=100 };
                    t.DOS    = new OrtaSaha{ isim="DOS",      Def=72,Pas=72,Dayaniklilik=75,Hiz=70,KararAlma=72,Vizyon=70,Sogukkanlilik=70,Agresiflik=65,Teknik=70,Bitiricilik=55,Topcalma=70,Atak=60,Guc=68,Kalecilik=8,Moral=100,Kondisyon=100 };
                    t.MOS    = new OrtaSaha{ isim="MOS",      Pas=75,Vizyon=74,Dayaniklilik=74,Hiz=72,KararAlma=74,Sogukkanlilik=72,Agresiflik=62,Teknik=73,Def=62,Bitiricilik=60,Topcalma=65,Atak=65,Guc=65,Kalecilik=8,Moral=100,Kondisyon=100 };
                    t.OOS    = new OrtaSaha{ isim="OOS",      Teknik=75,Pas=73,Dayaniklilik=72,Hiz=74,KararAlma=72,Vizyon=75,Sogukkanlilik=72,Agresiflik=52,Def=50,Bitiricilik=68,Topcalma=55,Atak=72,Guc=60,Kalecilik=8,Moral=100,Kondisyon=100 };
                    t.Forvet = new Forvet  { isim="Forvet",   Bitiricilik=75,Guc=74,Dayaniklilik=74,Hiz=76,KararAlma=70,Vizyon=68,Sogukkanlilik=72,Agresiflik=62,Teknik=68,Pas=58,Def=24,Topcalma=46,Atak=76,Kalecilik=5,Moral=100,Kondisyon=100 };
                    break;
            }
            return t;
        }
    }
}
