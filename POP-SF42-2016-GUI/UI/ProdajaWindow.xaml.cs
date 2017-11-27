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
    /// Interaction logic for ProdajaWindow.xaml
    /// </summary>
    public partial class ProdajaWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private ProdajaNamestaja prodaja;
        private Operacija operacija;
        public ProdajaWindow(ProdajaNamestaja prodaja, Operacija operacija)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.operacija = operacija;
            dgNamestaj.ItemsSource = Projekat.Instance.Namestaj;
            tbKupac.DataContext = prodaja;
            dpDatum.DataContext = prodaja;
            tbKolicina.DataContext = prodaja;
            cbUsluga.ItemsSource = Projekat.Instance.DodatneUsluge;
            cbUsluga.SelectedItem = prodaja;
    }

        private void PovecajKolicinu(object sender, RoutedEventArgs e)
        {
            tbKolicina.DataContext = prodaja.StavkeProdaje[0].Kolicina + 1;
        }
    }
}
