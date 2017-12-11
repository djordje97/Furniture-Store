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
   public class TipNamestajaDAO
    {
        public static ObservableCollection<TipNamestaja> SviTipovi()
        {
            ObservableCollection<TipNamestaja> tipovi = new ObservableCollection<TipNamestaja>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv=reader.GetString(1)
                    };
                    tipovi.Add(tip);
                }
            }
            return tipovi;
        }
        public static TipNamestaja TipPoId( int id)
        {

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan and Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1)
                    };
                    return tip;
                }
            }
            return null;
        }
        public static bool DodavanjeTipa(TipNamestaja t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES (@naziv,@Obrisan)", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool BrisanjeTipa(TipNamestaja t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE TipNamestaja SET Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", t.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool IzmenaTipa(TipNamestaja t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE TipNamestaja SET Naziv=@naziv WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Id", t.Id));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
