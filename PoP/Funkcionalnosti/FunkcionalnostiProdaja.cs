using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoP.Model;

namespace PoP.Funkcionalnosti
{
    public class FunkcionalnostiProdaja
    {
        public static ProdajaNamestaja pronadjiProdaju(int id, List<ProdajaNamestaja> prodaje)
        {
            foreach (ProdajaNamestaja pr in prodaje)
            {
                if (pr.Id == id)
                {
                    return pr;
                }
            }
            return null;
        }
        public static void IspisMenijaProdaje()
        {
            int izbor = 0;
            do
            {
                Console.WriteLine("\n=== Meni Prodaje ===");
                Console.WriteLine("1.Izlistaj Prodaje");
                Console.WriteLine("2.Dodaj novu Prodaju");
                Console.WriteLine("3.Obrisi postojecu Prodaju");
                Console.WriteLine("0.Povratak u glavni meni\n ");
                izbor = int.Parse(Console.ReadLine());
            }
            while (izbor < 0 || izbor > 4);
            switch (izbor)
            {
                case 1:
                    IzlistajProdaje();
                    IspisMenijaProdaje();
                    break;
                case 2:
                  DodavanjeProdaje();
                   IspisMenijaProdaje();
                    break;
                case 3:
                    BrisanjeProdaje();
                 IspisMenijaProdaje();
                    break;
                case 0:
                    Program.IspisGlavnogMenija();
                    break;

                default:
                    break;
            }
        }

        private static void IzlistajProdaje()
        {
            var prodaje = Projekat.Instance.ProdajaNamestaja;
            var dodatneUsluge = Projekat.Instance.DodatneUsluge;
            Console.WriteLine("\n=== Ispis prodaja ===\n");

            for (int i = 0; i < prodaje.Count; i++)
            {
                if (!prodaje[i].Obrisan)
                {
                    string ispis = $"{prodaje[i].Id}. Namestaj:";
                    for (int j = 0; j < prodaje[i].TipNamestajZaProdaju.Count; j++)
                    {
                        ispis += $"{FunkcionalnostiTipNamestaja.pronadjiTipNamestaja(prodaje[i].TipNamestajZaProdaju[j]).Naziv},";
                    }
                    ispis += $" Datum: {prodaje[i].DatumProdaje} Broj racuna:{prodaje[i].BrojRacuna} Kupac:{prodaje[i].Kupac} Dodatne usluge:";
                    for(int s = 0; s < prodaje[i].DodatneUsluge.Count; s++)
                    {
                        ispis += $"{FunkcionalnostiDodatneUsluge.pronadjiUslugu(prodaje[i].DodatneUsluge[s],dodatneUsluge).NazivUsluge},";
                    }
                    ispis += $"\nUkupna cena:{prodaje[i].UkupanIznos}";
                    Console.WriteLine(ispis);
                }
            }
        }
        public static void DodavanjeProdaje()
        {
            var Usluge = Projekat.Instance.DodatneUsluge;
            var tipNamestaja = new List<int>();
            var usluga = new List<int>();
            var prodaja = Projekat.Instance.ProdajaNamestaja;
            Console.WriteLine("Unesite datum prodaje: ");
            var datum = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Unesite broj racuna: ");
            var racun = int.Parse(Console.ReadLine());
            Console.WriteLine("Unesite kupca: ");
            var kupac = Console.ReadLine();
            Console.WriteLine("Unesite ukupnu cenu: ");
            var ucena = double.Parse(Console.ReadLine());
            bool unos = true;
            FunkcionalnostiTipNamestaja.IzlistajTipNamestaj();
            while (unos)
            {
                Console.WriteLine("\nIzaberite tip namestaja,za prekid unosa pritisnite 0: ");
                var izbor = int.Parse(Console.ReadLine());
                if (izbor != 0)
                {
                    var namestaj = FunkcionalnostiTipNamestaja.pronadjiTipNamestaja(izbor);
                    tipNamestaja.Add(namestaj.ID);
                }
                else
                {
                    unos = false;
                }
            }
            FunkcionalnostiDodatneUsluge.IzlistajUsluge();
            bool unosUsluga = true;
            while (unosUsluga)
            {
                Console.WriteLine("\nIzaberite dodatnu uslugu,za prekid unosa pritisnite 0: ");
                var izbor = int.Parse(Console.ReadLine());
                if (izbor != 0)
                {
                    var uslugaD = FunkcionalnostiDodatneUsluge.pronadjiUslugu(izbor,Usluge);
                    usluga.Add(uslugaD.ID);
                }
                else
                {
                    unosUsluga = false;
                }
            }
            var nprodaja = new ProdajaNamestaja()
            { Id=prodaja.Count+1,
            BrojRacuna=racun,
            DatumProdaje=datum,
            Kupac=kupac,
            DodatneUsluge=usluga,
            TipNamestajZaProdaju=tipNamestaja,
            UkupanIznos=ucena
               
            };
            prodaja.Add(nprodaja);
            Projekat.Instance.ProdajaNamestaja = prodaja;
        }
        public static void BrisanjeProdaje()
        {
            var prodaje = Projekat.Instance.ProdajaNamestaja;
            IzlistajProdaje();
            var izbor = int.Parse(Console.ReadLine());
            var prodajaBrisanje = pronadjiProdaju(izbor, prodaje);
            prodajaBrisanje.Obrisan = true;

        }
    }
}
