﻿using PoP.Model;
using PoP.Util;
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
    /// Interaction logic for TipNamestajaDodavanejIzmena.xaml
    /// </summary>
    public partial class TipNamestajaDodavanejIzmena : Window
    {
        public enum Operacija
        {
            DODAVANJE,
            IZMENA
        };
        public TipNamestaja tipNamestaja;
        private Operacija operacija;
        public TipNamestajaDodavanejIzmena(TipNamestaja tipNamestaja,Operacija operacija)
        {
            InitializeComponent();
            this.tipNamestaja = tipNamestaja;
            this.operacija = operacija;
            tbNazivTipa.DataContext = tipNamestaja;
            
        }



        private void Potvrdi(object sender, RoutedEventArgs e)
        {
            if (Provera() == true)
            {
                return;
            }
            this.DialogResult = true;
            
                if (operacija == Operacija.DODAVANJE)
                {

                TipNamestajaDAO.DodavanjeTipa(tipNamestaja);
                }
                else
                    TipNamestajaDAO.IzmenaTipa(tipNamestaja);
            this.Close();
        }
        public bool Provera()
        {
            BindingExpression be1 = tbNazivTipa.GetBindingExpression(TextBox.TextProperty);
            be1.UpdateSource();
            if (Validation.GetHasError(tbNazivTipa) == true)
            {
                return true;
            }
            return false;
        }
    }
}
