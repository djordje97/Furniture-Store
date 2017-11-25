﻿using PoP.Model;
using PoP.Util;
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
    /// Interaction logic for DodavanjeIzmenaKorisnik.xaml
    /// </summary>
    public partial class DodavanjeIzmenaKorisnik : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Korisnik korisnik;
        private Operacija operacija;
        public DodavanjeIzmenaKorisnik(Korisnik korisnik,Operacija operacija)
        {
            InitializeComponent();
            this.korisnik = korisnik;
            this.operacija = operacija;

            tbIme.DataContext = korisnik;
            tbPrezime.DataContext = korisnik;
            tbKorisnickoIme.DataContext = korisnik;
            tbLozinka.DataContext=korisnik;
            var tip = new List<TipKorisnika>();
            tip.Add(TipKorisnika.Administrator);
            tip.Add(TipKorisnika.Prodavac);
            cbTipKorisnika.ItemsSource = tip;
            cbTipKorisnika.DataContext = korisnik;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.Korisnici;
            var tip_korisnika =(TipKorisnika) cbTipKorisnika.SelectedItem;
            if (operacija == Operacija.DODAVANJE)
            {
                korisnik.Id = lista.Count + 1;
                
                lista.Add(korisnik);
            }

            GenericSerializer.Serialize("korisnici.xml", lista);
            Close();
        }

    }
}