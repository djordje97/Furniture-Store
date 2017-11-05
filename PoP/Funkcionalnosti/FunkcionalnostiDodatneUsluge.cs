using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;

namespace PoP.Funkcionalnosti
{
   public class FunkcionalnostiDodatneUsluge
    {

        public static DodatnaUsluga pronadjiUslugu(int id,List<DodatnaUsluga> dodatneUsluge)
        {
            foreach(DodatnaUsluga du in dodatneUsluge)
            {
                if (du.ID == id)
                {
                    return du;
                }

            }
            return null;

        }

        public static void IspisMenijaUsluga()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("=== Meni Usluge ===");
                Console.WriteLine("1. Izlistaj Usluge");
                Console.WriteLine("2.Dodaj novu Uslugu");
                Console.WriteLine("3.Izmeni postojecu Uslugu");
                Console.WriteLine("4. Obrisi postojecu Uslugu");
                Console.WriteLine("0. Povratak u glavni meni\n ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajUsluge();
                    IspisMenijaUsluga();
                    break;
                case 2:
                    DodavanjeUsluge();
                    IspisMenijaUsluga();
                    break;
                case 3:
                   IzmenaUsluge();
                   IspisMenijaUsluga();
                    break;
                case 4:
                   BrisanjeUsluge();
                   IspisMenijaUsluga();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        public static void IzlistajUsluge()
        {
            Console.WriteLine("\n=== Ispis usluga ===\n");
            var usluge= Projekat.Instance.DodatneUsluge;
            for(int i = 0; i < usluge.Count; i++)
            {
                if (!usluge[i].Obrisan)
                {
                    string ispis = $"{usluge[i].ID}. Naziv usluge:  {usluge[i].NazivUsluge}  Cena usluge: {usluge[i].CenaUsluge}";
                    Console.WriteLine(ispis);
                }
            }
        }
        public static void DodavanjeUsluge()
        {
            var usluge = Projekat.Instance.DodatneUsluge;
            Console.WriteLine("Unesite naziv usluge: ");
            var naziv = Console.ReadLine();
            Console.WriteLine("Unesite cenu usluge: ");
            var cena = double.Parse(Console.ReadLine());

            var u = new DodatnaUsluga()
            {
                ID=usluge.Count+1,
                NazivUsluge=naziv,
                CenaUsluge=cena
            };
            usluge.Add(u);
            Projekat.Instance.DodatneUsluge = usluge;
        }
        public static void IzmenaUsluge()
        {
            var usluge = Projekat.Instance.DodatneUsluge;
            IzlistajUsluge();
            Console.WriteLine("Izaberite uslugu za izmenu: ");
            int id = int.Parse(Console.ReadLine());
            var iusluga = pronadjiUslugu(id, usluge);
            Console.WriteLine("Izaberite parametar za izmenu: ");
            Console.WriteLine(" 1.Izmena naziva usluge\n 2.Izmena cene usluge\n");
            int izmena = int.Parse(Console.ReadLine());
            switch (izmena)
            {
                case 1:
                    Console.WriteLine("Unesite novi naziv usluge: ");
                    var naziv = Console.ReadLine();
                    iusluga.NazivUsluge = naziv;
                    break;
                case 2:
                    Console.WriteLine("Unesite novu cenu usluge: ");
                    var cena = double.Parse(Console.ReadLine());
                    iusluga.CenaUsluge = cena;
                    break;
            }
            Projekat.Instance.DodatneUsluge = usluge;
        }
        public static void BrisanjeUsluge()
        {
            var usluge = Projekat.Instance.DodatneUsluge;
            IzlistajUsluge();
            Console.WriteLine("\nIzaberite uslugu za brisanje: ");
            var id = int.Parse(Console.ReadLine());
            var ouslugu = pronadjiUslugu(id, usluge);
            ouslugu.Obrisan = true;
            Projekat.Instance.DodatneUsluge = usluge;
        }
    }
}
