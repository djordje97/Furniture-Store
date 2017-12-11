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
    /// Interaction logic for IzlistajStavke.xaml
    /// </summary>
    public partial class IzlistajStavke : Window
    {
        public ProdajaNamestaja prodaja;
        public Akcija akcija;
        public IzlistajStavke(ProdajaNamestaja prodaja=null,Akcija akcija=null)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.akcija = akcija;
            if (akcija == null)
                dgIspisStavki.ItemsSource = prodaja.StavkeProdaje;
            else
                dgIspisStavki.ItemsSource = akcija.NamestajPopust;
            dgIspisStavki.SelectedIndex = 0;
        }

        private void Izlaz(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dgIspisStavki_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header == "NamestajProdajaId" ||
                (string)e.Column.Header == "DodatnaUslugaId" || (string)e.Column.Header == "Obrisan")
                e.Cancel = true;
        }
    }
}
