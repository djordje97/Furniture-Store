using PoP.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace POP_SF42_2016_GUI.UI
{
    /// <summary>
    /// Interaction logic for GlavniProzor.xaml
    /// </summary>
    public partial class GlavniProzor : Window
    {
        public static string TrenutnoAktivno;
        public GlavniProzor()
            
        {
            
            InitializeComponent();
            ProveraprijavljenogKorisnika();

        }

        private void NamestajMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            OsveziPrikazNamestaj();
           

        }


        private void DodatneUslugeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            OsveziPrikazDodatnaUsluga();


        }
        private void TipoviNamestajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            OsveziPrikazTipNamestaja();


        }

        private void ProdajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            OsveziPrikazProdaja();

        }

        private void AkcijeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            OsveziPrikazAkcije();

        }

        private void KorisniciMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            OsveziPrikazKorisnik();

        }

        private void ProveraprijavljenogKorisnika()
        {
            var korisnik = Korisnik.PronadjiKorisnika(MainWindow.loggedUser);
            if (korisnik.TipKorisnika!=TipKorisnika.Administrator)
            {
                btnAkcije.Visibility = Visibility.Hidden;
                btnDodatneUsluge.Visibility = Visibility.Hidden;
                btnKorisnici.Visibility = Visibility.Hidden;
                btnNamestaj.Visibility = Visibility.Hidden;
                btnTipoviNamestaja.Visibility=Visibility.Hidden;
                btnAkcije.Visibility = Visibility.Hidden;
            }
           
         
        }
        public void OsveziPrikazNamestaj()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.Namestaj)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }
        public void OsveziPrikazTipNamestaja()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.TipNamestaja)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }

        public void OsveziPrikazKorisnik()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.Korisnici)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }

        public void OsveziPrikazProdaja()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.ProdajaNamestaja)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }

        public void OsveziPrikazAkcije()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.Akcija)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }

        public void OsveziPrikazDodatnaUsluga ()
        {

            lbPrikaz.Items.Clear();
            foreach (var item in Projekat.Instance.DodatneUsluge)
            {
                lbPrikaz.Items.Add(item);

            }
            lbPrikaz.SelectedIndex = 0;
        }

        private void Dodaj(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case"Namestaj":
                    Namestaj noviNamestaj = new Namestaj();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
                    ndi.ShowDialog();
                    OsveziPrikazNamestaj();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja noviTip = new TipNamestaja();
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(noviTip, TipNamestajaDodavanejIzmena.Operacija.DODAVANJE);
                    tdi.ShowDialog();
                    OsveziPrikazTipNamestaja();
                    break;
                 

                default:
                    break;
            }
        }

        private void Izmena(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    Namestaj namestajIzmena = lbPrikaz.SelectedItem as Namestaj;
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(namestajIzmena, NamestajDodavanjeIzmena.Operacija.IZMENA);
                    ndi.ShowDialog();
                    OsveziPrikazNamestaj();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja tipIzmena = lbPrikaz.SelectedItem as TipNamestaja;
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(tipIzmena, TipNamestajaDodavanejIzmena.Operacija.IZMENA);
                    tdi.ShowDialog();
                    OsveziPrikazTipNamestaja();
                    break;
                default:
                    break;
            }
        }

        private void Brisanje(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case "Namestaj":
                    var list=Projekat.Instance.Namestaj;
                    Namestaj namestajBrisanje = lbPrikaz.SelectedItem as Namestaj;
                    foreach (var namestaj in list)
                    {
                       if(namestaj.Id == namestajBrisanje.Id)
                        {
                            namestaj.Obrisan = true;
                        }
                    }
                    Projekat.Instance.Namestaj = list;
                    OsveziPrikazNamestaj();
                    break;
                default:
                    break;
            }
        }
    }
}
