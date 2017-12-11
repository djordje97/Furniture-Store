using PoP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POP_SF42_2016_GUI.DAO
{
    public class NamestajDAO
    {
        public static ObservableCollection<Namestaj> SavNamestaj()
        {
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = (TipNamestaja)TipNamestajaDAO.TipPoId(reader.GetInt32(4)),
                        Cena=(double)reader.GetDecimal(5)
                    };
                    namestaj.Add(n);
                }
            }
            return namestaj;
        }
        public static bool DodavanjeNamestaja(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO Namestaj(Naziv,Kolicina,Sifra,TipNamestaja,Cena,Obrisan) VALUES (@naziv,@kolicina,@sifra,@tip,@cena,@Obrisan)", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                cmd.Parameters.Add(new SqlParameter("@sifra",n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@tip", n.TipNamestaja.Id));
                cmd.Parameters.Add(new SqlParameter("@cena", n.Cena));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool BrisanjeNamestaja(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Namestaj SET Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id",n.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool IzmenaNamestaja(Namestaj n)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Namestaj SET Naziv=@naziv,Kolicina=@kolicina,Sifra=@sifra,TipNamestaja=@tip,Cena=@cena WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", n.Naziv));
                cmd.Parameters.Add(new SqlParameter("@kolicina", n.Kolicina));
                cmd.Parameters.Add(new SqlParameter("@sifra",n.Sifra));
                cmd.Parameters.Add(new SqlParameter("@tip",n.TipNamestaja.Id));
                cmd.Parameters.Add(new SqlParameter("@cena",n.Cena));
                cmd.Parameters.Add(new SqlParameter("@id", n.Id));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static Namestaj NametajPoId(int id)
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Namestaj WHERE Obrisan=@obrisan AND Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id",id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Kolicina = reader.GetInt32(2),
                        Sifra = reader.GetString(3),
                        TipNamestaja = (TipNamestaja)TipNamestajaDAO.TipPoId(reader.GetInt32(4)),
                        Cena = (double)reader.GetDecimal(5)
                    };
                    return n;
                }
            }
            return null;
        }

    }
}
