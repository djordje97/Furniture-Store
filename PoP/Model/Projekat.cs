using PoP.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoP.Model
{
    public class Projekat
    {
        public static Projekat Instance { get; } = new Projekat();

        private List<Namestaj> namestaj;
        private List<TipNamestaja> tipNamestaja;
        private List<Akcija> akcija;
        private List<DodatnaUsluga> dodatneUsluge;
        private List<ProdajaNamestaja> prodajaNamestaja;
        private List<Korisnik> korisnici;

        public List<Korisnik> Korisnici
        {
            get { return this.korisnici=GenericSerializer.Deserialize<Korisnik>("korisnici.xml"); }
            set { korisnici = value;
                GenericSerializer.Serialize<Korisnik>("korisnici.xml",korisnici);
            }
        }


        public List<ProdajaNamestaja> ProdajaNamestaja
        {
            get { return this.prodajaNamestaja=GenericSerializer.Deserialize<ProdajaNamestaja>("prodaja_namestaja.xml"); }
            set { prodajaNamestaja = value;
                GenericSerializer.Serialize<ProdajaNamestaja>("prodaja_namestaja",prodajaNamestaja);
            }
        }


        public List<DodatnaUsluga> DodatneUsluge
        {
            get { return this.dodatneUsluge=GenericSerializer.Deserialize<DodatnaUsluga>("dodatne_usluge.xml"); }
            set { dodatneUsluge = value;
                GenericSerializer.Serialize<DodatnaUsluga>("dodatne_usluge", dodatneUsluge);
            }
        }


        public List<Akcija> Akcija
        {
            get { return  this.akcija=GenericSerializer.Deserialize<Akcija>("akcije.xml"); }
            set { akcija = value;
                GenericSerializer.Serialize<Akcija>("akcije.xml",akcija);
            }
        }


        public List<TipNamestaja> TipNamestaja 
        {
            get { return this.tipNamestaja = GenericSerializer.Deserialize<TipNamestaja>("tip_namestaja.xml"); }
            set { this.tipNamestaja = value;
                GenericSerializer.Serialize<TipNamestaja>("tip_namestaja.xml",tipNamestaja);
            }
        }


        public List<Namestaj> Namestaj 
        {
            get {
                this.namestaj = GenericSerializer.Deserialize<Namestaj>("namestaj.xml");
                return namestaj;
            }
            set
            {
                this.namestaj = value;
                GenericSerializer.Serialize<Namestaj>("namestaj.xml",namestaj);
            }
        }

    }
}
