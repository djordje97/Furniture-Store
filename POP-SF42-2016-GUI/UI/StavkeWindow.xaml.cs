using PoP.Model;
using PoP.Util;
using POP_SF42_2016_GUI.Model;
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
    /// Interaction logic for StavkeWindow.xaml
    /// </summary>

    public partial class StavkeWindow : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public StavkaProdaje Stavka { set; get; }
        private Operacija operacija;
        public StavkeWindow(StavkaProdaje stavka,Operacija operacija)
        {
            InitializeComponent();
            Stavka = stavka;
            this.operacija = operacija;
            dgNamestaj.ItemsSource = NamestajPrikaz();
            dgNamestaj.SelectedIndex = 0;
            tbKolicina.DataContext = Stavka;
           
        }
       
        public List<Namestaj> NamestajPrikaz()
        {
            var namestaj = Projekat.Instance.Namestaj;
            List<Namestaj> zaPrikaz = new List<Namestaj>();
            foreach (var trenutni in namestaj)
            {
                if (trenutni.Obrisan == false)
                    zaPrikaz.Add(trenutni);


            }
            return zaPrikaz;
        }
        private void PotvrdiUslugu(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (operacija == Operacija.DODAVANJE)
            {
               
                Stavka.Cena = (Stavka.NamestajProdaja.Cena )* Stavka.Kolicina;

            }
            this.Close();
        }

        private void dgNamestaj_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if((string)e.Column.Header=="Id" || (string)e.Column.Header == "TipNamestajaId" || 
            (string)e.Column.Header == "DodatneUslugaId" || (string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void dgNamestaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Stavka.NamestajProdaja = dgNamestaj.SelectedItem as Namestaj;
            KolicinaValidation.Nam = Stavka.NamestajProdaja;
            
        }

       
        private void dgNamestaj_LostFocus(object sender, RoutedEventArgs e)
        {
            Stavka.NamestajProdaja = dgNamestaj.SelectedItem as Namestaj;
            KolicinaValidation.Nam = Stavka.NamestajProdaja;
        }
    }
}
