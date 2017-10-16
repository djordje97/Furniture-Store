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
        public List<Namestaj> NamestajZaProdaju { get; set; }
        public DateTime DatumProdaje { get; set; }
        public int BrojRacuna { get; set; }
        public String Kupac { get; set; }
        public double PDV { get; set; }
        public List<DodatnaUsluga> DodatneUsluge { get; set; }
        public double UkupanIznos { get; set; }

    }
}
