using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
   public class ProdajaNamestaja
    {
        public int Id { get; set; }
        public List<int> TipNamestajZaProdaju { get; set; }
        public DateTime DatumProdaje { get; set; }
        public int BrojRacuna { get; set; }
        public String Kupac { get; set; }
        public const double PDV = 0.02;
        public List<int> DodatneUsluge { get; set; }
        public double UkupanIznos { get; set; }
        public bool Obrisan { get; set; }

    }
}
