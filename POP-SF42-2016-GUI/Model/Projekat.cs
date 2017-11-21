using PoP.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();
        public ObservableCollection<Namestaj> Namestaj { get; set; }
        public ObservableCollection<TipNamestaja> TipNamestaja { get; set; }
        public ObservableCollection<Akcija> Akcije { get; set; }
        public ObservableCollection<Korisnik> Korisnici { get; set; }
        public ObservableCollection<ProdajaNamestaja> Prodaja { get; set; }
        public ObservableCollection<DodatnaUsluga> DodatneUsluge { get; set; }

        private Projekat()
        {
            Namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
            TipNamestaja= GenericSerializer.Deserialize<TipNamestaja>("tip_namestaja.xml");
            Akcije = GenericSerializer.Deserialize<Akcija>("akcije.xml");
            Korisnici = GenericSerializer.Deserialize<Korisnik>("korisnici.xml");
            Prodaja = GenericSerializer.Deserialize<ProdajaNamestaja>("prodaja_namestaja.xml");
            DodatneUsluge = GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml");

        }
    }
}
