using PoP.Model;
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
    /// Interaction logic for DodatneUslugeDodavanjeIzmena.xaml
    /// </summary>
    public partial class DodatneUslugeDodavanjeIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
       private DodatnaUsluga dodatneUsluga;
        private Operacija operacija;


        public DodatneUslugeDodavanjeIzmena(DodatnaUsluga dodatneUsluga,Operacija operacija)

        {
            
            InitializeComponent();
            this.operacija = operacija;
            this.dodatneUsluga = dodatneUsluga;
            tbNazivUsluge.DataContext = dodatneUsluga;
            tbCenaUsluge.DataContext = dodatneUsluga;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            var lista = Projekat.Instance.DodatneUsluge;

            if (operacija == Operacija.DODAVANJE)
            {
                dodatneUsluga.Id = lista.Count + 1;
                lista.Add(dodatneUsluga);
            }


            GenericSerializer.Serialize("dodatne_usluge.xml", lista);
            Close();
        }
    }
}
