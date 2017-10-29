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
        public List<TipNamestaja> NamestajNaPopustu { get; set; }
    }
}
