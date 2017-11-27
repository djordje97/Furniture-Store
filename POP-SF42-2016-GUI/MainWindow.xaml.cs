using PoP.Model;
using PoP.Util;
using POP_SF42_2016_GUI.Model;
using POP_SF42_2016_GUI.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace POP_SF42_2016_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string loggedUser { set; get; }
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            InitializeComponent();
 
        }
      
        private void Login(object sender, RoutedEventArgs e)
        {
            var korisnici = Projekat.Instance.Korisnici;
            foreach (var korisnik in korisnici)
            {
                var userName = tbUsername.Text.Trim();
                var password = pfPassword.Password.Trim();
                if (userName== "" || password == "")
                {
                    MessageBox.Show("Morate uneti sve podatke!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (userName == korisnik.Korisnicko_Ime && password == korisnik.Lozinka)
                {
                    loggedUser = userName;
                    var glavni= new GlavniProzor();
                    this.Close();
                    glavni.ShowDialog();
                    return;
                }

            }
            MessageBox.Show("Uneti podaci nisu tacni", "Greska", MessageBoxButton.OK, MessageBoxImage.Error);
            return;

        }

        private void izadji(object sender, RoutedEventArgs e)
        {

            this.Close();
            
        }

     
    }
}
