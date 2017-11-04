
using PoP.Model;
using PoP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Funkcionalnosti;

namespace PoP
{
    class Program
    {
        

        static void Main(string[] args)

        {
        
            login();

        }
        public static void IspisGlavnogMenija()
        { int izbor = 0;
            do
            {
                Console.WriteLine("=== GLAVNI MENI ===");
                Console.WriteLine("1. Rad sa namestajem");
                Console.WriteLine("2. Rad sa tipom namestaja");
                Console.WriteLine("3. Rad sa akcijama");
                Console.WriteLine("0. Izlazak iz aplikacije\n");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 3);
            switch (izbor)
            {
                case 1:
                    FunkcionalnostiNamestaj.IspisMenijaNamestaja();
                    break;
                case 2:
                    FunkcionalnostiTipNamestaja.IspisMenijaTipaNamestaja();
                    break;
                case 3:
                    FunkcionalnostiAkcije.IspisMenijaAkcija();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        private static void login()
        {
            for (int i = 0; i < 3; i++)
            {
                var korisnici = Projekat.Instance.Korisnici;
                Console.WriteLine("Unesite vase korisnicko ime: ");
                var kIme = Console.ReadLine();
                Console.WriteLine("Unesite vasu lozinku: ");
                var lozinka = Console.ReadLine();
                foreach (Korisnik k in korisnici)
                {
                    if (k.KorisnickoIme == kIme && k.Lozinka == lozinka)
                    {
                        IspisGlavnogMenija();
                        return;
                    }

                }

            }
            Environment.Exit(0);
        }

    }

}
