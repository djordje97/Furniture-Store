using PoP.Model;
using PoP.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            dgPrikaz.IsSynchronizedWithCurrentItem = true;
          
        }

        private void NamestajMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            dgPrikaz.ItemsSource = Projekat.Instance.Namestaj;



        }


        private void DodatneUslugeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            dgPrikaz.ItemsSource = Projekat.Instance.DodatneUsluge;


        }
        private void TipoviNamestajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            dgPrikaz.ItemsSource = Projekat.Instance.TipNamestaja;


        }

        private void ProdajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            dgPrikaz.ItemsSource = Projekat.Instance.Prodaja;

        }

        private void AkcijeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            dgPrikaz.ItemsSource = Projekat.Instance.Akcije;

        }

        private void KorisniciMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            dgPrikaz.ItemsSource = Projekat.Instance.Korisnici;
   
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
       
        private void Dodaj(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case"Namestaj":
                    Namestaj noviNamestaj = new Namestaj();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
                    ndi.ShowDialog();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja noviTip = new TipNamestaja();
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(noviTip, TipNamestajaDodavanejIzmena.Operacija.DODAVANJE);
                    tdi.ShowDialog();
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga usluga = new DodatnaUsluga();
                    DodatneUslugeDodavanjeIzmena ddi = new DodatneUslugeDodavanjeIzmena(usluga,DodatneUslugeDodavanjeIzmena.Operacija.DODAVANJE);
                    ddi.ShowDialog();
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
                    Namestaj namestajIzmena = dgPrikaz.SelectedItem as Namestaj;
                    Namestaj namestajKopija = (Namestaj)namestajIzmena.Clone();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(namestajIzmena, NamestajDodavanjeIzmena.Operacija.IZMENA);
                    if (ndi.ShowDialog() != true) 
                    {


                        int index = Projekat.Instance.Namestaj.IndexOf(namestajIzmena);
                        Projekat.Instance.Namestaj[index]= namestajKopija;
                    }
                    break;
                case "TipoviNamestaja":
                    TipNamestaja tipIzmena = dgPrikaz.SelectedItem as TipNamestaja;
                    TipNamestaja kopija = (TipNamestaja)tipIzmena.Clone();
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(tipIzmena, TipNamestajaDodavanejIzmena.Operacija.IZMENA);
                    if (tdi.ShowDialog() != true)
                    {


                        int index = Projekat.Instance.TipNamestaja.IndexOf(tipIzmena);
                        Projekat.Instance.TipNamestaja[index] = kopija;
                    }
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga usluga = dgPrikaz.SelectedItem as DodatnaUsluga;
                    DodatnaUsluga kopijaUsluge = (DodatnaUsluga)usluga.Clone();
                    DodatneUslugeDodavanjeIzmena ddi = new DodatneUslugeDodavanjeIzmena(usluga, DodatneUslugeDodavanjeIzmena.Operacija.IZMENA);
                    if (ddi.ShowDialog() != true)
                    {


                        int index = Projekat.Instance.DodatneUsluge.IndexOf(usluga);
                        Projekat.Instance.DodatneUsluge[index] = kopijaUsluge;
                    }
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
                    Namestaj namestajBrisanje = dgPrikaz.SelectedItem as Namestaj;
                    foreach (var namestaj in list)
                    {
                        if (namestaj.Id == namestajBrisanje.Id)
                        {
                            namestaj.Obrisan = true;
                        }
                    }
                    GenericSerializer.Serialize("namestaj.xml", list);
                    break;
                case "TipoviNamestaja":
                 var lista = Projekat.Instance.TipNamestaja;
                    TipNamestaja tip= dgPrikaz.SelectedItem as TipNamestaja;
                    foreach (var tipN in lista)
                    {
                        if (tipN.Id == tip.Id)
                        {
                            tipN.Obrisan = true;
                        }
                    }
                    GenericSerializer.Serialize("tip_namestaja.xml", lista);
                    break;
                case "DodatneUsluge":
                    var listaUsluga = Projekat.Instance.DodatneUsluge;
                    DodatnaUsluga uslugaBrisanje = dgPrikaz.SelectedItem as DodatnaUsluga;
                    foreach (var usluga in listaUsluga)
                    {
                        if (usluga.Id== uslugaBrisanje.Id)
                        {
                            usluga.Obrisan = true;
                        }
                    }
                    GenericSerializer.Serialize("dodatne_uslge.xml", listaUsluga);
                    break;
                default:
                    break;
            }
        }
    }
}
