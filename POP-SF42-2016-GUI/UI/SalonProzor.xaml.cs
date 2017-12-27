using PoP.Model;
using POP_SF42_2016_GUI.DAO;
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
    /// Interaction logic for SalonProzor.xaml
    /// </summary>
    public partial class SalonProzor : Window
    {
        
        private Salon s;
        private TipKorisnika t;
        public SalonProzor(Salon salon,TipKorisnika tip)
        {
            InitializeComponent();
            this.s = salon;
            this.t = tip;
            tbNaziv.DataContext = salon;
            tbAdresa.DataContext = salon;
            tbTelefon.DataContext = salon;
            tbEmail.DataContext = salon;
            tbSajt.DataContext = salon;
            tbPIB.DataContext = salon;
            tbMaticni.DataContext = salon;
            tbZiroRacun.DataContext = salon;
            if (t == TipKorisnika.Administrator)
            {
                tbcIspis.Visibility = Visibility.Hidden;
            }
            else
            {
                tbNaziv.Visibility = Visibility.Hidden;
                tbAdresa.Visibility = Visibility.Hidden;
                tbTelefon.Visibility = Visibility.Hidden;
                tbEmail.Visibility = Visibility.Hidden;
                tbSajt.Visibility = Visibility.Hidden;
                tbPIB.Visibility = Visibility.Hidden;
                tbMaticni.Visibility = Visibility.Hidden;
                tbZiroRacun.Visibility = Visibility.Hidden;
                btnPotvrdi.Content = "Zatvori";
                btnOdustani.Visibility = Visibility.Hidden;
                tbcIspis.Text = IspisPodataka();
            }
        }
        private string IspisPodataka()
        {
            string ispis = "";
            ispis += "Naziv: " + s.Naziv + "\n" +
                "Adresa: " + s.Adresa + "\n" +
                "Kontakt telefon: " + s.Broj_telefona + "\n" +
                "Email: " + s.Email + "\n" +
                "Sajt: " + s.Adresa_sajta + "\n" +
                "PIB: " + s.PIB + "\n" +
                "Maticni broj: " + s.Maticni_broj.ToString() + "\n" +
                "Ziro racun: " + s.Broj_ziro_racuna + "\n";
            return ispis;
             
                
        }

        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (t == TipKorisnika.Administrator)
            {
                SalonDAO.IzmenaSalona(s);
            }
            this.Close();
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
