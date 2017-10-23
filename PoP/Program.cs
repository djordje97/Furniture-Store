
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
        static List<Namestaj> Namestaj { get; set;} =new List<Namestaj>();
        static List<TipNamestaja> TipNamestaj { get; set; } = new List<TipNamestaja>();
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
                ID=TipNamestaj.Count+1,
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
            Namestaj.Add(n1);
            TipNamestaj.Add(t1);
            TipNamestaj.Add(t2);
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
                Console.WriteLine("0. Povratak u glavni meni ");
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
            for( int i=0; i < Namestaj.Count; i++)
            {
                if (!Namestaj[i].Obrisan)
                {
                    Console.WriteLine($"{i + 1}. Sifra:{Namestaj[i].Sifra} \tNaziv:{Namestaj[i].Naziv},\tCena: {Namestaj[i].JedinicnaCena}\n");
                }
            }
            IspisMenijaNamestaja();
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
            for(int i=0;i<TipNamestaj.Count;i++)
            {
                Console.WriteLine($"{i + 1}. {TipNamestaj[i].Naziv}");

            }
            int tipID = int.Parse(Console.ReadLine());
            TipNamestaja t2=new TipNamestaja();
            foreach(TipNamestaja t in TipNamestaj)
            {
                if (t.ID == tipID)
                {
                     t2 = t;
                    break;
                }
            }
            var n2 = new Namestaj()
            {
                Id = TipNamestaj.Count+1,
                Naziv = naziv,
                Sifra = sifra,
                JedinicnaCena = cena,
                TipNamestaja = t2
            };
            Namestaj.Add(n2);
            IspisMenijaNamestaja();
            

        }
        private static void IzmenaNamestaja()
        {
            Console.WriteLine("Unesite sifru namestaj za izmenu: ");
            string sifra = Console.ReadLine();
            Namestaj nIzmena = new Namestaj();
            foreach (Namestaj n in Namestaj)
            {
                if (n.Sifra == sifra)
                {
                    nIzmena = n;
                }
            }
            Console.WriteLine("Izaberite parametar za izmenu: ");
            Console.WriteLine(" 1. Izmena cene\n 2.Izmena naziva\n 3.Izmena sifre\n 4.Izmena kolicine");
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
        }
            IspisMenijaNamestaja();
        }
        private static void BrisanjeNamestaja()
        {
            Console.WriteLine("Unesite sifru namestaj za brisanje: ");
            string sifra = Console.ReadLine();
            foreach (Namestaj n in Namestaj)
            {
                if (n.Sifra == sifra)
                {
                    n.Obrisan = false;
                    Console.WriteLine("Uspesno brisanje namestaja");
                    break;

                }
            }
            IspisMenijaNamestaja();
        }
    }


}
