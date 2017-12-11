using PoP.Model;
using PoP.Util;
using POP_SF42_2016_GUI.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        ICollectionView view;
        public static string TrenutnoAktivno;
        public GlavniProzor()
            
        {
            
            InitializeComponent();
            ProveraprijavljenogKorisnika();
            dgPrikaz.IsSynchronizedWithCurrentItem = true;
            dgPrikaz.SelectedIndex = 0;
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;


        }

        private void NamestajMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            NamestajIspis();
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

        }


        private void DodatneUslugeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "DodatneUsluge";
            UslugaIspis();
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

        }
        private void TipoviNamestajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "TipoviNamestaja";
            TipNamestajaIspis();
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;
        }

        private void ProdajaMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Prodaja";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Prodaja);
            view.Filter = ProdajaIspis;
            dgPrikaz.ItemsSource = view;
            btnIzmeni.Content = "Storniraj";
            btnObrisi.Visibility = Visibility.Hidden;
            btnIzlistajStavke.Visibility = Visibility.Visible;
            btnIzlistajStavke.Content = "Izlistaj Stavke";
          
        }

        private void AkcijeMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Akcije";
            AkcijaIspis();
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Visible;
            btnIzlistajStavke.Content = "Izlistaj Namestaj";
            btnObrisi.Visibility = Visibility.Visible;

        }

        private void KorisniciMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Korisnici";
            KorisnikIspis();
            btnIzmeni.Content = "Izmeni";
            btnIzlistajStavke.Visibility = Visibility.Hidden;
            btnObrisi.Visibility = Visibility.Visible;

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
        public void NamestajIspis()
        {
            view = CollectionViewSource.GetDefaultView(NamestajDAO.SavNamestaj());
            dgPrikaz.ItemsSource = view;
        }
        public void TipNamestajaIspis()
        {
            view = CollectionViewSource.GetDefaultView(TipNamestajaDAO.SviTipovi());
            dgPrikaz.ItemsSource = view;
        }
        public void KorisnikIspis()
        {
            
            view = CollectionViewSource.GetDefaultView(KorisnikDAO.SviKorisnici());
            dgPrikaz.ItemsSource = view;
        }
        public void UslugaIspis()
        {
            view = CollectionViewSource.GetDefaultView(UslugeDAO.SveUsluge());
            dgPrikaz.ItemsSource = view;
        }
        public bool ProdajaIspis(object obj)
        {
            return ((ProdajaNamestaja)obj).Obrisan == false;
        }
        public void AkcijaIspis()
        {
            var akcije = AkcijaDAO.SveAkcije();
            var trenutni = akcije[0];
            for(int i = 0; i < akcije.Count; i++)
            {
                if (akcije[i].PocetakAkcije == trenutni.PocetakAkcije && akcije[i].KrajAkcije == trenutni.KrajAkcije)
                {
                    akcije.Remove(trenutni);
                    trenutni = akcije[i];
                }
            }
            view = CollectionViewSource.GetDefaultView(akcije);
            dgPrikaz.ItemsSource = view;
        }



        private void Dodaj(object sender, RoutedEventArgs e)
        {
            switch (TrenutnoAktivno)
            {
                case"Namestaj":
                    Namestaj noviNamestaj = new Namestaj();
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(noviNamestaj, NamestajDodavanjeIzmena.Operacija.DODAVANJE);
                    ndi.ShowDialog();
                    NamestajIspis();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja noviTip = new TipNamestaja();
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(noviTip, TipNamestajaDodavanejIzmena.Operacija.DODAVANJE);
                    tdi.ShowDialog();
                    TipNamestajaIspis();
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga usluga = new DodatnaUsluga();
                    DodatneUslugeDodavanjeIzmena ddi = new DodatneUslugeDodavanjeIzmena(usluga,DodatneUslugeDodavanjeIzmena.Operacija.DODAVANJE);
                    ddi.ShowDialog();
                    UslugaIspis();
                    break;
                case "Korisnici":
                    Korisnik korisnik = new Korisnik();
                    DodavanjeIzmenaKorisnik dik = new DodavanjeIzmenaKorisnik(korisnik, DodavanjeIzmenaKorisnik.Operacija.DODAVANJE);
                    dik.ShowDialog();
                    KorisnikIspis();
                    break;
                case "Akcije":
                    Akcija akcija = new Akcija();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(akcija, AkcijaDodavanjeIzmena.Operacija.DODAVANJE);
                    dia.ShowDialog();
                    AkcijaIspis();
                    break;
                case "Prodaja":
                    ProdajaNamestaja prodaja = new ProdajaNamestaja();
                    ProdajaWindow pwd = new ProdajaWindow(prodaja,ProdajaWindow.Operacija.DODAVANJE);
                    pwd.ShowDialog();
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
                    NamestajDodavanjeIzmena ndi = new NamestajDodavanjeIzmena(namestajKopija, NamestajDodavanjeIzmena.Operacija.IZMENA);
                    if (ndi.ShowDialog() == true) 
                    {
                        NamestajDAO.IzmenaNamestaja(ndi.namestaj);

                    }
                    NamestajIspis();
                    break;
                case "TipoviNamestaja":
                    TipNamestaja tipIzmena = dgPrikaz.SelectedItem as TipNamestaja;
                    TipNamestaja kopija = (TipNamestaja)tipIzmena.Clone();
                    TipNamestajaDodavanejIzmena tdi = new TipNamestajaDodavanejIzmena(kopija, TipNamestajaDodavanejIzmena.Operacija.IZMENA);
                    if (tdi.ShowDialog() == true)
                    {
                        TipNamestajaDAO.IzmenaTipa(tdi.tipNamestaja);
                    }
                    TipNamestajaIspis();
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga usluga = dgPrikaz.SelectedItem as DodatnaUsluga;
                    DodatnaUsluga kopijaUsluge = (DodatnaUsluga)usluga.Clone();
                    DodatneUslugeDodavanjeIzmena ddi = new DodatneUslugeDodavanjeIzmena(kopijaUsluge, DodatneUslugeDodavanjeIzmena.Operacija.IZMENA);
                    if (ddi.ShowDialog() == true)
                    {


                        UslugeDAO.IzmenaUsluge(ddi.dodatneUsluga);
                    }
                    UslugaIspis();
                    break;
                case "Korisnici":
                    Korisnik korisnik= dgPrikaz.SelectedItem as Korisnik;
                    Korisnik kopijaKorisnika = (Korisnik)korisnik.Clone();
                    DodavanjeIzmenaKorisnik dik = new DodavanjeIzmenaKorisnik(kopijaKorisnika, DodavanjeIzmenaKorisnik.Operacija.IZMENA);
                    if (dik.ShowDialog() == true)
                    {


                        KorisnikDAO.IzmenaKorisnika(dik.korisnik);
                    }
                    KorisnikIspis();
                    break;
                case "Akcije":
                    Akcija akcija = dgPrikaz.SelectedItem as Akcija;
                    Akcija kopijaAkcije = (Akcija)akcija.Clone();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(kopijaAkcije, AkcijaDodavanjeIzmena.Operacija.IZMENA);
                    if (dia.ShowDialog() == true)
                    {
                        AkcijaDAO.IzmenaAkcije(dia.akcija);
                    }
                    AkcijaIspis();
                    break;
                case "Prodaja":
                    ProdajaNamestaja prodaja = dgPrikaz.SelectedItem as ProdajaNamestaja;
                    ProdajaNamestaja kopijaProdaje = (ProdajaNamestaja)prodaja.Clone();
                    ProdajaWindow pw = new ProdajaWindow(prodaja, ProdajaWindow.Operacija.IZMENA);
                    if (pw.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Prodaja.IndexOf(prodaja);
                        Projekat.Instance.Prodaja[index] = kopijaProdaje;
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
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        NamestajDAO.BrisanjeNamestaja(namestajBrisanje);
                    NamestajIspis();
                    break;
                case "TipoviNamestaja":
                 var lista = Projekat.Instance.TipNamestaja;
                    TipNamestaja tip= dgPrikaz.SelectedItem as TipNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        TipNamestajaDAO.BrisanjeTipa(tip);
                    TipNamestajaIspis();
                    break;
                case "DodatneUsluge":
                    DodatnaUsluga uslugaBrisanje = dgPrikaz.SelectedItem as DodatnaUsluga;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    UslugeDAO.BrisanjeUsluge(uslugaBrisanje);
                    UslugaIspis();
                    break;
                case "Korisnici":
                    var listaKorisnika = Projekat.Instance.Korisnici;
                    var korisnikBrisanje = dgPrikaz.SelectedItem as Korisnik;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        KorisnikDAO.BrisanjeKorisnika(korisnikBrisanje);
                        KorisnikIspis();
                    break;
                case "Akcije":
                    var listaAkcija = Projekat.Instance.Akcije;
                    Akcija akcijaBrisanje = dgPrikaz.SelectedItem as Akcija;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        AkcijaDAO.BrisanjeAkcije(akcijaBrisanje);
                    AkcijaIspis();
                    break;
                case "Prodaja":
                    var listaProdaja = Projekat.Instance.Prodaja;
                    ProdajaNamestaja prodajaBrisanje = dgPrikaz.SelectedItem as ProdajaNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        prodajaBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("prodaja_namestaja.xml", listaProdaja);
                    view.Refresh();
                    break;
                default:
                    break;
            }
        }

        private void dgPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" ||(string)e.Column.Header== "NamestajProdajaId" || (string)e.Column.Header== "DodatneUslugaId"
                || (string)e.Column.Header == "StavkaProdajeId" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "NamestajPopustId"
                || (string)e.Column.Header == "StavkeProdaje" || (string)e.Column.Header == "NamestajPopust"  || (string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "DodatneUsluge")

            {
                e.Cancel = true;
            }
        }

        private void Izlistaj(object sender, RoutedEventArgs e)
        {
            ProdajaNamestaja pn = dgPrikaz.SelectedItem as ProdajaNamestaja;
            Akcija a = dgPrikaz.SelectedItem as Akcija;
            if(a == null)
            {
                IzlistajStavke izs = new IzlistajStavke(pn,null);
                izs.ShowDialog();
            }
            else
            {
                IzlistajStavke izs = new IzlistajStavke(null,a);
                izs.ShowDialog();
            }
               
        }
    }
}
