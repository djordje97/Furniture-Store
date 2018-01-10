using PoP.Model;
using PoP.Util;
using POP_SF42_2016_GUI.DAO;
using POP_SF42_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<StavkaProdaje> dodatestavke = new ObservableCollection<StavkaProdaje>();
        public ObservableCollection<DodatnaUsluga> dodateusluge  = new ObservableCollection<DodatnaUsluga>();
        public ObservableCollection<StavkaProdaje> obrisanestavke  = new ObservableCollection<StavkaProdaje>();
        public ObservableCollection<DodatnaUsluga> obrisaneusluge = new ObservableCollection<DodatnaUsluga>();
        public ProdajaWindow(ProdajaNamestaja prodaja, Operacija operacija)
        {
            InitializeComponent();
            this.prodaja = prodaja;
            this.operacija = operacija;
            dgStavke.ItemsSource = prodaja.StavkeProdaje;
            dgUsluge.ItemsSource = prodaja.DodatneUsluge;
            tbKupac.DataContext = prodaja;
            dpDatum.DataContext = prodaja;


        }

        private void DodajStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje stavka = new StavkaProdaje();
            StavkeWindow st = new StavkeWindow(stavka,StavkeWindow.Operacija.DODAVANJE);
            if (st.ShowDialog() == true)
            {
                prodaja.StavkeProdaje.Add(st.Stavka);
                dodatestavke.Add(st.Stavka);
            }
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (Provera() == true)
            {
                return;
            }
            Random rn = new Random();
            this.DialogResult = true;

            if (operacija == Operacija.DODAVANJE)
            {

                prodaja.BrojRacuna = rn.Next(100, 10000);
                ProdajaDAO.DodajProdaju(prodaja);
            }
            else
            {
                ProdajaDAO.IzmenaProdaje(prodaja);
                if (dodatestavke.Count > 0)
                    ProdajaDAO.DodajStavku(prodaja, dodatestavke);
                if (dodateusluge.Count > 0)
                    ProdajaDAO.DodajUslugu(prodaja, dodateusluge);
                ProdajaDAO.ObrisiStavku(prodaja, obrisanestavke);
                ProdajaDAO.ObrisiUslugu(prodaja, obrisaneusluge);
            }
            this.Close();
        }

        private void dgStavke_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if((string)e.Column.Header == "NamestajProdajaId" || (string)e.Column.Header == "Id"
                ||(string)e.Column.Header == "Obrisan")
            {
                e.Cancel = true;
            }
        }

        private void UkloniStavku(object sender, RoutedEventArgs e)
        {
            StavkaProdaje izabrana = dgStavke.SelectedItem as StavkaProdaje;
            prodaja.StavkeProdaje.Remove(izabrana);
            obrisanestavke.Add(izabrana);
            if(dodatestavke.Contains(izabrana)==true)
                dodatestavke.Remove(izabrana);
         
        }

        private void btnDodajU_Click(object sender, RoutedEventArgs e)
        {
            PreuzmiUslugu pu = new PreuzmiUslugu();
            if (pu.ShowDialog() == true)
            {
               
                prodaja.DodatneUsluge.Add(pu.Usluge);
                dodateusluge.Add(pu.Usluge);
              
            }
        }

        private void btnObisiU_Click(object sender, RoutedEventArgs e)
        {
            var izabrana = dgUsluge.SelectedItem as DodatnaUsluga;
            prodaja.DodatneUsluge.Remove(izabrana);
            obrisaneusluge.Add(izabrana);
            if(dodateusluge.Contains(izabrana)==true)
                dodateusluge.Remove(izabrana);

        }

        private void dgUsluge_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if ((string)e.Column.Header == "Id" || (string)e.Column.Header=="Obrisan")
                e.Cancel = true;
        }

        public bool Provera()
        {
            BindingExpression be1 = tbKupac.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbKupac) == true)
            {
                return true;
            }
            return false;
        }

    }
}
