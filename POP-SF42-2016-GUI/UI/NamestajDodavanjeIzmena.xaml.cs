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
        public NamestajDodavanjeIzmena(Namestaj namestaj ,Operacija operacija)
        {
            InitializeComponent();
            InicijalizujVrednosti(namestaj,operacija);
          
        }
    
        private void InicijalizujVrednosti(Namestaj namestaj ,Operacija operacija)
        {
            try
            {
                this.namestaj = namestaj;
                this.operacija = operacija;
                tbNazivNamestaja.Text = namestaj.Naziv;
                tbSifraNamestaja.Text = namestaj.Sifra;
                if (namestaj.JedinicnaCena == 0)
                    tbCenaNamestaja.Text = "";
                else
                    tbCenaNamestaja.Text = namestaj.JedinicnaCena.ToString();
                if (namestaj.KolicinaUMagacinu == 0)
                    tbKolicinaNamestaja.Text = "";
                else
                    tbKolicinaNamestaja.Text = namestaj.KolicinaUMagacinu.ToString();
                foreach (var tipNamestaja in Projekat.Instance.TipNamestaja)
                {
                    cbTipNamestaja.Items.Add(tipNamestaja);
                }
                foreach (TipNamestaja tipNamestaja in cbTipNamestaja.Items)
                {
                    if (tipNamestaja.ID == namestaj.IdTipaNamestaja)
                    {
                        cbTipNamestaja.SelectedItem = tipNamestaja;
                        break;
                    }
                }
            }catch (FormatException)
            {
                MessageBox.Show("Kolicina i cena moraju biti broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            try
            {
                var lista = Projekat.Instance.Namestaj;
                var izabraniTip = (TipNamestaja)cbTipNamestaja.SelectedItem;
                switch (operacija)
                {
                    case Operacija.DODAVANJE:
                        var noviNamestaj = new Namestaj()
                        {
                            Id = lista.Count + 1,
                            Naziv = tbNazivNamestaja.Text,
                            Sifra = tbSifraNamestaja.Text,
                            KolicinaUMagacinu = int.Parse(tbKolicinaNamestaja.Text),
                            JedinicnaCena = double.Parse(tbCenaNamestaja.Text),
                            IdTipaNamestaja = izabraniTip.ID
                        };
                        lista.Add(noviNamestaj);
                        break;
                    case Operacija.IZMENA:
                        foreach (var n in lista)
                        {
                            if (n.Id == namestaj.Id)
                            {
                                n.Naziv = tbNazivNamestaja.Text;
                                n.Sifra = tbSifraNamestaja.Text;
                                n.KolicinaUMagacinu = int.Parse(tbKolicinaNamestaja.Text);
                                n.JedinicnaCena = double.Parse(tbCenaNamestaja.Text);
                                n.IdTipaNamestaja = izabraniTip.ID;
                            }
                        }
                        break;
                }
                Projekat.Instance.Namestaj = lista;
                Close();
            }
            catch(FormatException)
            {
                MessageBox.Show("Kolicina i cena moraju biti broj!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
