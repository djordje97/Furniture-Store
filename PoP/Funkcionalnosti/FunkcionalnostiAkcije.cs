using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;

namespace PoP.Funkcionalnosti
{
    public class FunkcionalnostiAkcije
    {
        public static Akcija pronadjiAkciju(int id)
        {
            var Akcije = Projekat.Instance.Akcija;

            foreach (Akcija a in Akcije)
            {
                if (a.ID == id)
                {
                    return a;

                }
            }
            return null;
        }

        public static void IspisMenijaAkcija()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("=== Meni Akcija ===");
                Console.WriteLine("1. Izlistaj Akcije");
                Console.WriteLine("2.Dodaj novu Akciju");
                Console.WriteLine("3.Izmeni postojecu Akciju");
                Console.WriteLine("4. Obrisi postojecu Akciju ");
                Console.WriteLine("0. Povratak u glavni meni\n ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajAkcije();
                    break;
                case 2:
                    DodavanjeAkcije();
                    break;
                case 3:
                    //IzmenaNamestaja();
                    break;
                case 4:
                    // BrisanjeNamestaja();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }
        public static void IzlistajAkcije()
        {
            Console.WriteLine("=== Ispis akcija ===");
            var akcije = Projekat.Instance.Akcija;
            for (int i = 0; i < akcije.Count; i++)
            {
                if (!akcije[i].Obrisana)
                {
                    string ispis= $"{akcije[i].ID}. Pocetak akcije:  {akcije[i].PocetakAkcije}  Kraj akcije: {akcije[i].KrajAkcije}  Namestaj na akciji:";

                    for (int j = 0; j < akcije[i].NamestajNaPopustu.Count; j++)
                    {
                        ispis+=$"{FunkcionalnostiNamestaj.pronadjiNamestaj(akcije[i].NamestajNaPopustu[j]).Naziv},";
                    }
                    Console.WriteLine(ispis+$" Popust: {akcije[i].Popust}");
                }
            }
            IspisMenijaAkcija();
        }
        public static void DodavanjeAkcije()
        {
            List<int> namestajNaAkciji = new List<int>();
            var akcije = Projekat.Instance.Akcija;
            Console.WriteLine("Unesite pocetak akcije: ");
            var pocetak = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Unesite kraj akcije: ");
            var kraj = DateTime.Parse(Console.ReadLine());
            FunkcionalnostiNamestaj.IzlistajNamestaj();
            bool unos = true;
            while (unos)
            {
                Console.WriteLine("Izaberite namestaj za akciju,za prekid unosa pritisnite 0: ");
                var namestaj = FunkcionalnostiNamestaj.pronadjiNamestaj(int.Parse(Console.ReadLine()));
                namestajNaAkciji.Add(namestaj.Id);

            }
            Console.WriteLine("Unesite popust: ");
            var popust = Decimal.Parse(Console.ReadLine());
            var nakcija = new Akcija()
            {
                ID = akcije.Count + 1,
                PocetakAkcije = pocetak,
                KrajAkcije = kraj,
                NamestajNaPopustu = namestajNaAkciji,
                Popust=popust
                
            };
        }
    }
}
