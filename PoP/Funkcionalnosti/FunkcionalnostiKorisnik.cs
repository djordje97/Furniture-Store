using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;


namespace PoP.Funkcionalnosti
{
   public class FunkcionalnostiKorisnik
    {
        public static Korisnik pronadjiKorisnika(int id,List<Korisnik> korisnici)
        {
            foreach(var k in korisnici)
            {
                if (k.ID == id)
                {
                    return k;
                }
            }
            return null;
        }
        public static void IspisMenijaKorisnik()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("=== Meni Korisnika===");
                Console.WriteLine("1. Izlistaj korisnike");
                Console.WriteLine("2.Dodaj novog korisnika");
                Console.WriteLine("3.Izmeni postojeceg korisnika");
                Console.WriteLine("4. Obrisi postojeceg korisnika");
                Console.WriteLine("0. Povratak u glavni meni\n ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajKorisnika();
                    IspisMenijaKorisnik();
                    break;
                case 2:
                    DodavanjeKorisnika();
                    IspisMenijaKorisnik();
                    break;
                case 3:
                    IzmenaKorisnika();
                    IspisMenijaKorisnik();
                    break;
                case 4:
                    BrisanjeKorisnika();
                    IspisMenijaKorisnik();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void BrisanjeKorisnika()
        {
            var korisnici = Projekat.Instance.Korisnici;
            IzlistajKorisnika();
            Console.WriteLine("Izaberite korisnika za brisanje: ");
            var id = int.Parse(Console.ReadLine());
            var korbrisanje = pronadjiKorisnika(id, korisnici);
            korbrisanje.Obrisan = true;
            Projekat.Instance.Korisnici = korisnici;
        }

        private static void IzmenaKorisnika()
        {
            var korisnici = Projekat.Instance.Korisnici;
            IzlistajKorisnika();
            Console.WriteLine("Izaberite korisnika za izmenu: ");
            var id = int.Parse(Console.ReadLine());
            var izabrani = pronadjiKorisnika(id, korisnici);
            Console.WriteLine(" 1. Izmena imena\n 2.Izmena prezimena\n 3.Izmena lozinke\n4.Izmena tipa korisnika");
            var izbor = int.Parse(Console.ReadLine());
            switch (izbor)
            {
                case 1:
                    Console.WriteLine("Unesite ime za izmenu: ");
                    var ime = Console.ReadLine();
                    izabrani.Ime = ime;
                    break;
                case 2:
                    Console.WriteLine("Unesite prezime za izmenu: ");
                    var prezime = Console.ReadLine();
                    izabrani.Prezime = prezime;
                    break;
                case 3:
                    Console.WriteLine("Unesite lozinku za izmenu: ");
                    var lozinka = Console.ReadLine();
                    izabrani.Lozinka = lozinka;
                    break;
                case 4:
                    Console.WriteLine("Izaberite tip za izmenu:\n1.Administrator\n2.Prodavac ");
                    var izborT = int.Parse(Console.ReadLine());
                    TipKorisnika t;
                    switch (izborT)
                    {
                        case 1:
                            t = TipKorisnika.Administrator;
                            break;
                        case 2:
                            t = TipKorisnika.Prodavac;
                            break;
                        default:
                            t = TipKorisnika.Prodavac;
                            break;
                    }
                    izabrani.TipKorisnika = t;
                    break;
                default:
                    Console.WriteLine("Pogresan izbor: ");
                    IzmenaKorisnika();
                    break;
            }
            Projekat.Instance.Korisnici = korisnici;
        }

        private static void DodavanjeKorisnika()
        {
            var korisnici = Projekat.Instance.Korisnici;
            Console.WriteLine("Unesite ime korisnika: ");
            var ime = Console.ReadLine();
            Console.WriteLine("Unesite prezime korisnika: ");
            var prezime = Console.ReadLine();
            Console.WriteLine("Unesite korisnicko ime korisnika: ");
            var korIme = Console.ReadLine();
            Console.WriteLine("Unesite lozinku korisnika: ");
            var lozinka = Console.ReadLine();
            Console.WriteLine("Izaberite tip korisnika:\n1.Administrator\n2.Prodavac");
            var izbor = int.Parse(Console.ReadLine());
            TipKorisnika t;
            switch (izbor)
            {
                case 1:
                    t = TipKorisnika.Administrator;
                    break;
                case 2:
                    t = TipKorisnika.Prodavac;
                    break;
                default:
                    t = TipKorisnika.Prodavac;
                    break;
            }
            var nk = new Korisnik()
            {
                ID = korisnici.Count + 1,
                Ime = ime,
                Prezime = prezime,
                KorisnickoIme = korIme,
                Lozinka = lozinka,
                TipKorisnika = t
            };
            korisnici.Add(nk);
            Projekat.Instance.Korisnici = korisnici;
        }

        private static void IzlistajKorisnika()
        {
            var korisnici = Projekat.Instance.Korisnici;
            for (int i = 0; i < korisnici.Count; i++)
            {
                if (!korisnici[i].Obrisan)
                {
                    Console.WriteLine($"{korisnici[i].ID}. Ime:{korisnici[i].Ime} Prezime:{korisnici[i].Prezime} Korisnicko ime:{korisnici[i].KorisnickoIme} Lozinka:{korisnici[i].Lozinka} Tip korisnika:{korisnici[i].TipKorisnika}");

                }
            }

        }
    }
}
