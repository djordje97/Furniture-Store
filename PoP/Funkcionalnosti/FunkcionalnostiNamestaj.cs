using PoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PoP.Funkcionalnosti
{
    class FunkcionalnostiNamestaj
    {
        public static Namestaj pronadjiNamestaj(int id)
        {
            var Namestaj = Projekat.Instance.Namestaj;

            foreach (Namestaj n in Namestaj)
            {
                if (n.Id == id)
                {
                    return n;

                }
            }
            return null;
        }

        public static void IspisMenijaNamestaja()
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
                    IspisMenijaNamestaja();
                    break;
                case 2:
                    DodavanjeNamestaja();
                    IspisMenijaNamestaja();
                    break;
                case 3:
                    IzmenaNamestaja();
                    IspisMenijaNamestaja();
                    break;
                case 4:
                    BrisanjeNamestaja();
                    IspisMenijaNamestaja();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        public static void IzlistajNamestaj()
        {
            var ukupanNamestaj = Projekat.Instance.Namestaj;
            Console.WriteLine("=== Ispis namestaja ===");
            for (int i = 0; i < ukupanNamestaj.Count; i++)
            {
                if (!ukupanNamestaj[i].Obrisan)
                {
                    Console.WriteLine($"{ukupanNamestaj[i].Id}. Sifra:{ukupanNamestaj[i].Sifra} \tNaziv:{ukupanNamestaj[i].Naziv},\tCena: {ukupanNamestaj[i].JedinicnaCena}\n");
                }
            }
  
        }

        public static void DodavanjeNamestaja()
        {
            var ukupanNamestaj = Projekat.Instance.Namestaj;
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
            Console.WriteLine("Izabrali ste dodavanje namestaja,molimo da unesete odgovarajuce podatke:");
            Console.WriteLine("Unesite naziv namestaja: ");
            string naziv = Console.ReadLine();
            Console.WriteLine("Unesite siftu namestaja: ");
            string sifra = Console.ReadLine();
            Console.WriteLine("Unesite cenu namestaja: ");
            double cena = double.Parse(Console.ReadLine());
            Console.WriteLine("Izaberite tip namestaja\n");
            for (int i = 0; i < TipoviNamestaja.Count; i++)
            {
                Console.WriteLine($"{TipoviNamestaja[i].ID}. {TipoviNamestaja[i].Naziv}");

            }
            int tipID = int.Parse(Console.ReadLine());
            TipNamestaja t2 = FunkcionalnostiTipNamestaja.pronadjiTipNamestaja(tipID);

            var n2 = new Namestaj()
            {
                Id = ukupanNamestaj.Count + 1,
                Naziv = naziv,
                Sifra = sifra,
                JedinicnaCena = cena,
                TipNamestaja = t2.ID
            };
            ukupanNamestaj.Add(n2);
            Projekat.Instance.Namestaj = ukupanNamestaj;
            


        }
        public static void IzmenaNamestaja()
        {
            var ukupanNamestaj = Projekat.Instance.Namestaj;
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
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
            switch (izbor)
            {
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
                    nIzmena.TipNamestaja = FunkcionalnostiTipNamestaja.pronadjiTipNamestaja(idTipa).ID;
                    break;
            }
            Projekat.Instance.Namestaj = ukupanNamestaj;
            
        }
        public static void BrisanjeNamestaja()
        {
            var ukupanNamestaj = Projekat.Instance.Namestaj;
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
            Projekat.Instance.Namestaj = ukupanNamestaj;
            
        }

       

    }
}
