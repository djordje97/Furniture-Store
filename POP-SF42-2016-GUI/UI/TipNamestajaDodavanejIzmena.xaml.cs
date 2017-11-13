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
    /// Interaction logic for TipNamestajaDodavanejIzmena.xaml
    /// </summary>
    public partial class TipNamestajaDodavanejIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        private TipNamestaja tipNamestaja;
        private Operacija operacija;
        public TipNamestajaDodavanejIzmena(TipNamestaja tipNamestaja,Operacija operacija)
        {
            InitializeComponent();
            InicijalizujVrednosti(tipNamestaja, operacija);
        }

        private void InicijalizujVrednosti(TipNamestaja tipNamestaj, Operacija operacija)
        {
            this.tipNamestaja = tipNamestaj;
            this.operacija = operacija;
            tbNazivTipa.Text = tipNamestaj.Naziv;
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            var lista = Projekat.Instance.TipNamestaja;
            switch (operacija)
            {
                case Operacija.DODAVANJE:
                    var noviTip = new TipNamestaja()
                    {
                        ID = lista.Count + 1,
                        Naziv = tbNazivTipa.Text,
                       
                    };
                    lista.Add(noviTip);
                    break;
                case Operacija.IZMENA:
                    foreach (var n in lista)
                    {
                        if (n.ID == tipNamestaja.ID)
                        {
                            n.Naziv = tbNazivTipa.Text;
                           
                        }
                    }
                    break;
            }
            Projekat.Instance.TipNamestaja = lista;
            Close();

        }
    }
}
