using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using PoP.Model;

namespace POP_SF42_2016_GUI.DAO
{
    public class KorisnikDAO { 
    
        public static ObservableCollection<Korisnik> SviKorisnici()
        {
            ObservableCollection<Korisnik> korisnici = new ObservableCollection<Korisnik>();
            int id = 1;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Korisnik WHERE Obrisan=@obrisan",conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Korisnik k = new Korisnik()
                    {
                        Id = id++,
                        Ime = reader.GetString(0),
                        Prezime = reader.GetString(1),
                        Korisnicko_Ime = reader.GetString(2),
                        Lozinka = reader.GetString(3),
                        TipKorisnika=(TipKorisnika)Enum.Parse(typeof(TipKorisnika),reader.GetString(4)),
                        Obrisan = false

                    };
                    korisnici.Add(k);
                }
            }
            return korisnici;
        }
        public static bool DodavanjeKorisnika(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO Korisnik(Ime,Prezime,Korisnicko_Ime,Lozinka,Tip_Korisnika,Obrisan) VALUES (@ime,@prezime,@kIme,@lozinka,@tip,@Obrisan)", conn);
                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@kIme", k.Korisnicko_Ime));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tip", k.TipKorisnika.ToString()));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                cmd.ExecuteNonQuery();
            return true;
            }
        }
        public static bool BrisanjeKorisnika(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Korisnik SET Obrisan=@obrisan WHERE Korisnicko_Ime=@kIme", conn);
                cmd.Parameters.Add(new SqlParameter("@kIme", k.Korisnicko_Ime));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool IzmenaKorisnika(Korisnik k)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Korisnik SET Ime=@ime,Prezime=@prezime,Korisnicko_Ime=@kIme,Lozinka=@lozinka,Tip_Korisnika=@tip WHERE Korisnicko_Ime=@kIme", conn);
                cmd.Parameters.Add(new SqlParameter("@ime", k.Ime));
                cmd.Parameters.Add(new SqlParameter("@prezime", k.Prezime));
                cmd.Parameters.Add(new SqlParameter("@kIme", k.Korisnicko_Ime));
                cmd.Parameters.Add(new SqlParameter("@lozinka", k.Lozinka));
                cmd.Parameters.Add(new SqlParameter("@tip", k.TipKorisnika.ToString()));
                

                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
