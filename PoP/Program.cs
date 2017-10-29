
using PoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP
{
    class Program
    {
        static List<Namestaj>ukupanNamestaj { get; set;} =new List<Namestaj>();
        static List<TipNamestaja> TipoviNamestaja { get; set; } = new List<TipNamestaja>();
        static void Main(string[] args)
        {
            var s1 = new Salon()
            {
                ID = 1,
                Naziv = "Forma FTNale",
                Adresa = "Maksima Gorkog 6",
                BrojTelefona = "015-337-492",
                Email = "FTNale.uns@gmail.com",
                AdresaInternetSajta = "www.ftnale.rs",
                PIB = 1897562,
                BrojZiroRacuna = "840-9854478-1-7598",



            };
            var t1 = new TipNamestaja()
            {
                ID = 1,
                Naziv = "Garnitura",

            };
            var t2 = new TipNamestaja()
            {
                ID=TipoviNamestaja.Count+1,
                Naziv="Ormani",
            };

            var n1 = new Namestaj()
            {
                Id = 1,
                Naziv = "Super sofa",
                Sifra = "1234",
                TipNamestaja = t1,
                KolicinaUMagacinu = 5,
                JedinicnaCena = 1000000

            };
            ukupanNamestaj.Add(n1);
            TipoviNamestaja.Add(t1);
            TipoviNamestaja.Add(t2);
            IspisGlavnogMenija();

        }
        private static void IspisGlavnogMenija()
        { int izbor = 0;
            do
            {
                Console.WriteLine("=== GLAVNI MENI ===");
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom namestaja");
                Console.WriteLine("0. Izlazak iz aplikacije\n");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 2);
            switch (izbor)
            {
                case 1:
                    IspisMenijaNamestaja();
                    break;
                case 2:
                    IspisMenijaTipaNamestaja();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        private static void IspisMenijaNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("=== Meni namestaja ===");
                Console.WriteLine("1. Izlistaj namestaj");
                Console.WriteLine("2.Dodaj novi namestaj");
                Console.WriteLine("3.Izmeni postojeci namestaj");
                Console.WriteLine("4. Obrisi postojeci ");
                Console.WriteLine("0. Povratak u glavni meni\n ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajNamestaj();
                    break;
                case 2:
                    DodavanjeNamestaja();
                    break;
                case 3:
                    IzmenaNamestaja();
                    break;
                case 4:
                    BrisanjeNamestaja();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void IzlistajNamestaj()
        {
            Console.WriteLine("=== Ispis namestaja ===");
            for( int i=0; i < ukupanNamestaj.Count; i++)
            {
                if (!ukupanNamestaj[i].Obrisan)
                {
                    Console.WriteLine($"{ukupanNamestaj[i].Id}. Sifra:{ukupanNamestaj[i].Sifra} \tNaziv:{ukupanNamestaj[i].Naziv},\tCena: {ukupanNamestaj[i].JedinicnaCena}\n");
                }
            }
            IspisMenijaNamestaja();
        }
        private static TipNamestaja pronadjiTipNamestaja(int id)
        {
            
            foreach (TipNamestaja t in TipoviNamestaja)
            {
                if (t.ID == id)
                {
                    return t;
                    
                }
            }
            return null;
        }
        private static void DodavanjeNamestaja()
        {
            Console.WriteLine("Izabrali ste dodavanje namestaja,molimo da unesete odgovarajuce podatke:");
            Console.WriteLine("Unesite naziv namestaja: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Unesite siftu namestaja: ");
            string sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu namestaja: ");
            double cena =double.Parse( Console.ReadLine());
            Console.WriteLine("Izaberite tip namestaja\n");
            for(int i=0;i<TipoviNamestaja.Count;i++)
            {
                Console.WriteLine($"{TipoviNamestaja[i].ID}. {TipoviNamestaja[i].Naziv}");

            }
            int tipID = int.Parse(Console.ReadLine());
            TipNamestaja t2=pronadjiTipNamestaja(tipID);
            
            var n2 = new Namestaj()
            {
                Id = ukupanNamestaj.Count+1,
                Naziv = naziv,
                Sifra = sifra,
                JedinicnaCena = cena,
                TipNamestaja = t2
            };
            ukupanNamestaj.Add(n2);
            IspisMenijaNamestaja();
            

        }
        private static void IzmenaNamestaja()
        {
            IzlistajNamestaj();
            Console.WriteLine("Unesite sifru namestaj za izmenu: ");
            string sifra = Console.ReadLine();
            Namestaj nIzmena = new Namestaj();
            foreach (Namestaj n in ukupanNamestaj)
            {
                if (n.Sifra == sifra)
                {
                    nIzmena = n;
                }
            }
            Console.WriteLine("Izaberite parametar za izmenu: ");
            Console.WriteLine(" 1. Izmena cene\n 2.Izmena naziva\n 3.Izmena sifre\n 4.Izmena kolicine\n 5.Izmena tipa namestaja");
            int izbor = int.Parse(Console.ReadLine());
            switch (izbor) {
                case 1:
            Console.WriteLine("Unesite cenu za  izmenu: ");
            nIzmena.JedinicnaCena = double.Parse(Console.ReadLine());
                    break;
                case 2:
            Console.WriteLine("Unesite naziv za  izmenu: ");
            nIzmena.Naziv = Console.ReadLine();
                    break;
                case 3:
            Console.WriteLine("Unesite Sifru za  izmenu: ");
            nIzmena.Sifra = Console.ReadLine();
                    break;
                case 4:
            Console.WriteLine("Unesite kolicinu za  izmenu: ");
            nIzmena.KolicinaUMagacinu = int.Parse(Console.ReadLine());
                    break;
                case 5:
                    for (int i = 0; i < TipoviNamestaja.Count; i++)
                    {
                        Console.WriteLine($"{TipoviNamestaja[i].ID}. {TipoviNamestaja[i].Naziv}");

                    }
                    Console.WriteLine("Unesite id tipa namestaja za  izmenu: ");
                    int idTipa = int.Parse(Console.ReadLine());
                    nIzmena.TipNamestaja = pronadjiTipNamestaja(idTipa);
                    break;
            }
            IspisMenijaNamestaja();
        }
        private static void BrisanjeNamestaja()
        {
            Console.WriteLine("Unesite sifru namestaja za brisanje: ");
            string sifra = Console.ReadLine();
            foreach (Namestaj n in ukupanNamestaj)
            {
                if (n.Sifra == sifra)
                {
                    n.Obrisan = true;
                    Console.WriteLine("Uspesno brisanje namestaja");
                    break;

                }
            }
            IspisMenijaNamestaja();
        }
        private static void IspisMenijaTipaNamestaja()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("\n=== Meni  tipa namestaja ===\n");
                Console.WriteLine("1. Izlistaj  tipove namestaj");
                Console.WriteLine("2.Dodaj novi tip namestaj");
                Console.WriteLine("3.Izmeni postojeci tip namestaj");
                Console.WriteLine("4. Obrisi postojeci tip namestaja ");
                Console.WriteLine("0. Povratak u glavni meni ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajTipNamestaj();
                    break;
                case 2:
                    DodavanjeTipaNamestaja();
                    break;
                case 3:
                    IzmenaTipaNamestaja();
                    break;
                case 4:
                    BrisanjeTipaNamestaja();
                    break;
                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void BrisanjeTipaNamestaja()
        {
            Console.WriteLine("\nIzaberite tip namestaja za brisanje: ");
            int izbor = int.Parse(Console.ReadLine());
            foreach (TipNamestaja n in TipoviNamestaja)
            {
                if (n.ID == izbor)
                {
                    n.Obrisan = true;
                    Console.WriteLine("Uspesno brisanje  tipa namestaja");
                    break;

                }
            }
            IspisMenijaTipaNamestaja();
        }

        private static void IzmenaTipaNamestaja()
        {
            Console.WriteLine("\nIzaberite Tip namestaja za izmenu:\n");
            int Id = int.Parse(Console.ReadLine());
            TipNamestaja n = pronadjiTipNamestaja(Id);
            Console.WriteLine("Unesite naziv za izmenu:");
            string noviNaziv=Console.ReadLine();
            n.Naziv= noviNaziv;
            IspisMenijaTipaNamestaja();

        }

        private static void DodavanjeTipaNamestaja()
        {
            Console.WriteLine("Izabrali ste dodavanje tipa namestaja,molimo da unesete odgovarajuce podatke:\n");
            Console.WriteLine("Unesite naziv tipa namestaja: ");
            string naziv = Console.ReadLine();
            var tn = new TipNamestaja()
            {
                ID = TipoviNamestaja.Count + 1,
                Naziv = naziv

            };
            TipoviNamestaja.Add(tn);
            IspisMenijaTipaNamestaja();
        }

        private static void IzlistajTipNamestaj()
        {
            Console.WriteLine("=== Ispis tipova namestaja ===");
            for (int i = 0; i < TipoviNamestaja.Count; i++)
            {
                if (!TipoviNamestaja[i].Obrisan)
                {
                    Console.WriteLine($"{i + 1}. Naziv:  {TipoviNamestaja[i].Naziv}");
                }
            }
            IspisMenijaTipaNamestaja();
        }
    }



}
