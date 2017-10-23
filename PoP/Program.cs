
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

            var n1 = new Namestaj()
            {
                Id = 1,
                Naziv = "Super sofa",
                Sifra = "1234",
                TipNamestaja = t1,
                KolicinaUMagacinu = 5

            };
            Namestaj.Add(n1);
            IspisGlavnogMenija();

        }
        private static void IspisGlavnogMenija()
        { int izbor = 0;
            do
            {
                Console.WriteLine("=== GLAVNI MENI ===");
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom namestaja");
                Console.WriteLine("0. Izlazak iz aplikacije");
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
                Console.WriteLine("3.Izmeni postojeci nametaj");
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
                case 0:
                    IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void IzlistajNamestaj()
        {
            for( int i=0; i < Namestaj.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{Namestaj[i].Naziv},cena: {Namestaj[i].JedinicnaCena}");
            }
            IspisMenijaNamestaja();
        }
    }
}
