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
    /// Interaction logic for NamestajDodavanjeIzmena.xaml
    /// </summary>
    public partial class NamestajDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private Namestaj namestaj;
        private Operacija operacija;
        public NamestajDodavanjeIzmena(Namestaj namestaj ,Operacija operacija=Operacija.DODAVANJE)
        {
            InitializeComponent();
            this.namestaj = namestaj;
            this.operacija = operacija;
            tbNazivNamestaja.Text = namestaj.Naziv;
            tbSifraNamestaja.Text = namestaj.Sifra;
            tbCenaNamestaja.Text = namestaj.Sifra;
            tbKolicinaNamestaja.Text = namestaj.KolicinaUMagacinu.ToString();
            var list = Projekat.Instance.TipNamestaja;
            for (int i = 0; i < list.Count; i++) 
            {
                cbTipNamestaja.Items.Add(list[i].Naziv);
            }
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            var list = Projekat.Instance.Namestaj;
            namestaj.Id = Projekat.Instance.Namestaj.Count + 1;
            namestaj.Naziv = tbNazivNamestaja.Text;
            namestaj.Sifra = tbSifraNamestaja.Text;
            namestaj.JedinicnaCena =double.Parse(tbCenaNamestaja.Text);
            namestaj.KolicinaUMagacinu = int.Parse(tbKolicinaNamestaja.Text);
            TipNamestaja tip = null;
            foreach (var item in Projekat.Instance.TipNamestaja)
            {   
                if(item.Naziv == cbTipNamestaja.SelectedValue.ToString())
                {
                     tip = item;

                }

            }
            namestaj.IdTipaNamestaja = tip.ID;
            this.DialogResult = true;
            if (operacija == Operacija.DODAVANJE)
            {
               list.Add(namestaj);
             
            }
            Projekat.Instance.Namestaj = list;
            Close();
        }
    }
}
