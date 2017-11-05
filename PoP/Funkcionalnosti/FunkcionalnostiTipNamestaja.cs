using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;
using PoP.Funkcionalnosti;

namespace PoP.Funkcionalnosti
{
    public class FunkcionalnostiTipNamestaja
    {
        public static TipNamestaja pronadjiTipNamestaja(int id)
        {
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;

            foreach (TipNamestaja t in TipoviNamestaja)
            {
                if (t.ID == id)
                {
                    return t;

                }
            }
            return null;
        }

        public static void IspisMenijaTipaNamestaja()
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
                    IspisMenijaTipaNamestaja();
                    break;
                case 2:
                    DodavanjeTipaNamestaja();
                    IspisMenijaTipaNamestaja();
                    break;
                case 3:
                    IzmenaTipaNamestaja();
                    IspisMenijaTipaNamestaja();
                    break;
                case 4:
                    BrisanjeTipaNamestaja();
                    IspisMenijaTipaNamestaja();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        public static void BrisanjeTipaNamestaja()
        {
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
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
            Projekat.Instance.TipNamestaja = TipoviNamestaja;
           
        }

        public static void IzmenaTipaNamestaja()
        {
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
            Console.WriteLine("\nIzaberite Tip namestaja za izmenu:\n");
            int Id = int.Parse(Console.ReadLine());
            TipNamestaja n = pronadjiTipNamestaja(Id);
            Console.WriteLine("Unesite naziv za izmenu:");
            string noviNaziv = Console.ReadLine();
            n.Naziv = noviNaziv;
            Projekat.Instance.TipNamestaja =TipoviNamestaja;
            


        }

        public static void DodavanjeTipaNamestaja()
        {
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
            Console.WriteLine("Izabrali ste dodavanje tipa namestaja,molimo da unesete odgovarajuce podatke:\n");
            Console.WriteLine("Unesite naziv tipa namestaja: ");
            string naziv = Console.ReadLine();
            var tn = new TipNamestaja()
            {
                ID = TipoviNamestaja.Count + 1,
                Naziv = naziv

            };
            TipoviNamestaja.Add(tn);
            Projekat.Instance.TipNamestaja = TipoviNamestaja;
            
        }

        public static void IzlistajTipNamestaj()
        {
            var TipoviNamestaja = Projekat.Instance.TipNamestaja;
            Console.WriteLine("=== Ispis tipova namestaja ===");
            for (int i = 0; i < TipoviNamestaja.Count; i++)
            {
                if (!TipoviNamestaja[i].Obrisan)
                {
                    Console.WriteLine($"{i + 1}. Naziv:  {TipoviNamestaja[i].Naziv}");
                }
            }
          
        }
    }
}
