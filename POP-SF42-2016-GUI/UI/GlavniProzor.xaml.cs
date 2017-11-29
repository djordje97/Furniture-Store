﻿using PoP.Model;
using PoP.Util;
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
          
        }

        private void NamestajMeni(object sender, RoutedEventArgs e)
        {
            TrenutnoAktivno = "Namestaj";
            view = CollectionViewSource.GetDefaultView(Projekat.Instance.Namestaj);
            view.Filter = NamestajIspis;
            dgPrikaz.ItemsSource = view;



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
        public bool NamestajIspis(object obj)
        {
            return ((Namestaj)obj).Obrisan == false;
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
                case "Korisnici":
                    Korisnik korisnik = new Korisnik();
                    DodavanjeIzmenaKorisnik dik = new DodavanjeIzmenaKorisnik(korisnik, DodavanjeIzmenaKorisnik.Operacija.DODAVANJE);
                    dik.ShowDialog();
                    break;
                case "Akcije":
                    Akcija akcija = new Akcija();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(akcija, AkcijaDodavanjeIzmena.Operacija.DODAVANJE);
                    dia.ShowDialog();
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
                case "Korisnici":
                    Korisnik korisnik= dgPrikaz.SelectedItem as Korisnik;
                    Korisnik kopijaKorisnika = (Korisnik)korisnik.Clone();
                    DodavanjeIzmenaKorisnik dik = new DodavanjeIzmenaKorisnik(korisnik, DodavanjeIzmenaKorisnik.Operacija.IZMENA);
                    if (dik.ShowDialog() != true)
                    {


                        int index = Projekat.Instance.Korisnici.IndexOf(korisnik);
                        Projekat.Instance.Korisnici[index] = kopijaKorisnika;
                    }
                    break;
                case "Akcije":
                    Akcija akcija = dgPrikaz.SelectedItem as Akcija;
                    Akcija kopijaAkcije = (Akcija)akcija.Clone();
                    AkcijaDodavanjeIzmena dia = new AkcijaDodavanjeIzmena(akcija, AkcijaDodavanjeIzmena.Operacija.IZMENA);
                    if (dia.ShowDialog() != true)
                    {
                        int index = Projekat.Instance.Akcije.IndexOf(akcija);
                        Projekat.Instance.Akcije[index] = kopijaAkcije;
                    }
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
                    if(MessageBox.Show("Da li ste sigurni?","Potvrda",MessageBoxButton.YesNo,MessageBoxImage.Question)==MessageBoxResult.Yes)
                    namestajBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("namestaj.xml", list);
                    break;
                case "TipoviNamestaja":
                 var lista = Projekat.Instance.TipNamestaja;
                    TipNamestaja tip= dgPrikaz.SelectedItem as TipNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        tip.Obrisan = true;
                    GenericSerializer.Serialize("tip_namestaja.xml", lista);
                    break;
                case "DodatneUsluge":
                    var listaUsluga = Projekat.Instance.DodatneUsluge;
                    DodatnaUsluga uslugaBrisanje = dgPrikaz.SelectedItem as DodatnaUsluga;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        uslugaBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("dodatne_uslge.xml", listaUsluga);
                    break;
                case "Korisnici":
                    var listaKorisnika = Projekat.Instance.Korisnici;
                    var korisnikBrisanje = dgPrikaz.SelectedItem as Korisnik;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        korisnikBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("korisnici.xml", listaKorisnika);
                    break;
                case "Akcije":
                    var listaAkcija = Projekat.Instance.Akcije;
                    Akcija akcijaBrisanje = dgPrikaz.SelectedItem as Akcija;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        akcijaBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("akcije.xml", listaAkcija);
                    break;
                case "Prodaja":
                    var listaProdaja = Projekat.Instance.Prodaja;
                    ProdajaNamestaja prodajaBrisanje = dgPrikaz.SelectedItem as ProdajaNamestaja;
                    if (MessageBox.Show("Da li ste sigurni?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        prodajaBrisanje.Obrisan = true;
                    GenericSerializer.Serialize("prodaja_namestaja.xml", listaProdaja);
                    break;
                default:
                    break;
            }
        }

        private void dgPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "Obrisan" ||(string)e.Column.Header== "NamestajProdajaId" || (string)e.Column.Header== "DodatneUslugaId"
                || (string)e.Column.Header == "StavkaProdajeId" || (string)e.Column.Header == "TipNamestajaId" || (string)e.Column.Header == "NamestajPopustId")

            {
                e.Cancel = true;
            }
        }
    }
}
