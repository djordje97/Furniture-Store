using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
    public class Akcija
    {
        public int ID { get; set; }
        public bool Obrisana { get; set; }
        public DateTime PocetakAkcije { get; set; }
        public DateTime KrajAkcije { get; set; }
        public decimal Popust { get; set; }
        public List<int> NamestajNaPopustu { get; set; }

        public override string ToString()
        {
            if (!Obrisana)
            {


                string ispis = $"{ID}. {PocetakAkcije} {KrajAkcije} {Popust} ";

                for (int i = 0; i < NamestajNaPopustu.Count; i++)
                {
                    ispis += Namestaj.PronadjiNamestaj(NamestajNaPopustu[i]).Naziv + " ,";

                }
                return ispis;
            }
            return null;
            
        }

        public static Akcija PronadjiAkciju(int id)
        {
            foreach (var akcija in Projekat.Instance.Akcija)
            {
                if (akcija.ID == id)
                {
                    return akcija;
                }

            }
            return null;
        }
    }

 
}
