using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;

namespace PoP.Model
{
   public class Namestaj
    {
        public int Id { get; set; }
        public bool Obrisan { get; set; }
        public string Naziv { get; set; }
        public string Sifra { get; set; }
        public double JedinicnaCena { get; set; }
        public int KolicinaUMagacinu { get; set; }
        public int IdTipaNamestaja { get; set; }

        public override string ToString()
        {
            if (!Obrisan)
            {
                string ispis = "";
                return ispis += $"{Naziv},{JedinicnaCena},{TipNamestaja.PronadjiTip(IdTipaNamestaja).Naziv}";
            }
            return null;
           
        
        }

        public static Namestaj PronadjiNamestaj(int id)
        {
            foreach (var namestaj in Projekat.Instance.Namestaj)
            {
                if (namestaj.Id == id)
                {
                    return namestaj;
                }

            }
            return null;
        }
    }
}
