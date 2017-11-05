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
        public static Akcija pronadjiAkciju(int id, List<Akcija> Akcije)
        {


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
                Console.WriteLine("\n=== Meni Akcija ===\n");
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
                    IspisMenijaAkcija();
                    break;
                case 2:
                    DodavanjeAkcije();
                    IspisMenijaAkcija();
                    break;
                case 3:
                    IzmenaAkcije();
                    IspisMenijaAkcija();
                    break;
                case 4:
                     BrisanjeAkcije();
                    IspisMenijaAkcija();
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
            Console.WriteLine("\n=== Ispis akcija ===\n");
            var akcije = Projekat.Instance.Akcija;
            for (int i = 0; i < akcije.Count; i++)
            {
                if (!akcije[i].Obrisana)
                {
                    string ispis = $"{akcije[i].ID}. Pocetak akcije:  {akcije[i].PocetakAkcije}  Kraj akcije: {akcije[i].KrajAkcije}  Namestaj na akciji:";

                    for (int j = 0; j < akcije[i].NamestajNaPopustu.Count; j++)
                    {
                        ispis += $"{FunkcionalnostiNamestaj.pronadjiNamestaj(akcije[i].NamestajNaPopustu[j]).Naziv},";
                    }
                    Console.WriteLine(ispis + $" Popust: {akcije[i].Popust}");
                }
            }
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
                Console.WriteLine("\nIzaberite namestaj za akciju,za prekid unosa pritisnite 0: ");
                var izbor = int.Parse(Console.ReadLine());
                if (izbor != 0)
                {
                    var namestaj = FunkcionalnostiNamestaj.pronadjiNamestaj(izbor);
                    namestajNaAkciji.Add(namestaj.Id);
                }
                else
                {
                    unos = false;
                }
            }
            Console.WriteLine("Unesite popust: ");
            var popust = Decimal.Parse(Console.ReadLine());
            var nakcija = new Akcija()
            {
                ID = akcije.Count + 1,
                PocetakAkcije = pocetak,
                KrajAkcije = kraj,
                NamestajNaPopustu = namestajNaAkciji,
                Popust = popust

            };
            akcije.Add(nakcija);
            Projekat.Instance.Akcija = akcije;
        }
        public static void IzmenaAkcije()
        {
            var Akcije = Projekat.Instance.Akcija;
            IzlistajAkcije();
            Console.WriteLine("Izaberite akciju za izmenu: ");
            int izbor = int.Parse(Console.ReadLine());
            var iakcija = pronadjiAkciju(izbor, Akcije);
            Console.WriteLine("Izaberite parametar za izmenu: ");
            Console.WriteLine(" 1.Izmena kraja akcije\n 2.Izmena namestaja na akciji\n 3.Izmena popusta\n");
            int izmena = int.Parse(Console.ReadLine());
            switch (izmena)
            {
                case 1:
                    Console.WriteLine("Unesite novi datum za kraj akcije: ");
                    var kraj = DateTime.Parse(Console.ReadLine());
                    iakcija.KrajAkcije = kraj;
                    break;
                case 2:
                    FunkcionalnostiNamestaj.IzlistajNamestaj();
                    List<int> namestajNaAkciji = new List<int>();
                    bool unos = true;
                    while (unos)
                    {
                        Console.WriteLine("Unesite novi namestaj za izmenu,za prekid unosa unesite 0: ");
                        var nizbor = int.Parse(Console.ReadLine());
                        if (nizbor != 0)
                        {
                            var namestaj = FunkcionalnostiNamestaj.pronadjiNamestaj(nizbor);
                            namestajNaAkciji.Add(namestaj.Id);
                        }
                        else
                        {
                            unos = false;
                        }
                    }
                    iakcija.NamestajNaPopustu = namestajNaAkciji;
                    break;
                case 3:
                    Console.WriteLine("Unesite novi popust za izmenu: ");
                    var npopust= Decimal.Parse( Console.ReadLine());
                    iakcija.Popust = npopust;
                    break;
                default:
                    Console.WriteLine("Pogresan unos!");
                    IspisMenijaAkcija();
                    break;
            }
            Projekat.Instance.Akcija = Akcije;
        }
        public static void BrisanjeAkcije()
        {var akcije = Projekat.Instance.Akcija;
            IzlistajAkcije();
            Console.WriteLine("\nIzaberite akciju za brisanje: ");
            var izbor = int.Parse( Console.ReadLine());
            var akcijaBrisanje = pronadjiAkciju(izbor, akcije);
            akcijaBrisanje.Obrisana = true;
            Projekat.Instance.Akcija = akcije;
        }

    }
}
