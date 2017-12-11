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
   public class UslugeDAO
    {
        public static ObservableCollection<DodatnaUsluga> SveUsluge()
        {
            ObservableCollection<DodatnaUsluga> usluge = new ObservableCollection<DodatnaUsluga>();
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatnaUsluga du = new DodatnaUsluga()
                    {
                        Id=reader.GetInt32(0),
                        Naziv=reader.GetString(1),
                        Cena=(double)reader.GetDecimal(2)
                    };
                    usluge.Add(du);
                    
                }
            }
            return usluge ;
        }
        public static bool DodavanjeUsluge(DodatnaUsluga du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan) VALUES (@naziv,@cena,@Obrisan)", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool BrisanjeUsluge(DodatnaUsluga du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE DodatneUsluge SET Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", du.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool IzmenaUsluge(DodatnaUsluga du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE DodatneUsluge SET Naziv=@naziv,Cena=@cena WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv",du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@Id", du.Id));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
    }
}
