using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
    public enum TipKorisnika
    {
        Administrator,
        Prodavac
    }
    public class Korisnik
    {
        public int ID { get; set; }
        public bool Obrisan { get; set; }
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public TipKorisnika TipKorisnika { get; set; }

        public override string ToString()
        {
            if (!Obrisan)
            {
                return $"{Ime} {Prezime} {KorisnickoIme} {Lozinka} {TipKorisnika}";
            }
            return null;
        }

        public static Korisnik PronadjiKorisnika(string userName)
        {
            foreach (var korisnik in Projekat.Instance.Korisnici)
            {
                if (korisnik.KorisnickoIme==userName)
                {
                    return korisnik;
                }

            }
            return null;
        }

    }
}
