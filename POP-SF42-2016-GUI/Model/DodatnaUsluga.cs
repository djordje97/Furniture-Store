using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
   
   public class DodatnaUsluga
    {
        public int ID { get; set; }
        public bool Obrisan { get; set; }
        public string NazivUsluge { get; set; }
        public double CenaUsluge { get; set; }

        public override string ToString()
        {
            if (!Obrisan)
            {
                return $"{ID} {NazivUsluge} {CenaUsluge}";
            }
            return null;
        }
        public static DodatnaUsluga PronadjiUslugu(int id)
        {
            foreach (var usluga in Projekat.Instance.DodatneUsluge)
            {
                if (usluga.ID == id)
                {
                    return usluga;
                }

            }
            return null;
        }


    }
}
