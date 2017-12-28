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
        public static DodatnaUsluga DodavanjeUsluge(DodatnaUsluga du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO DodatneUsluge(Naziv,Cena,Obrisan) VALUES (@naziv,@cena,@Obrisan)", conn);
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                cmd.Parameters.Add(new SqlParameter("@naziv", du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                du.Id = newId;
            }
            Projekat.Instance.DodatneUsluge.Add(du);
            return du;
        }
        public static bool BrisanjeUsluge(DodatnaUsluga du)
        {
            du.Obrisan = true;
            return IzmenaUsluge(du);
        }
        public static bool IzmenaUsluge(DodatnaUsluga du)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE DodatneUsluge SET Naziv=@naziv,Cena=@cena,Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv",du.Naziv));
                cmd.Parameters.Add(new SqlParameter("@cena", du.Cena));
                cmd.Parameters.Add(new SqlParameter("@Id", du.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", du.Obrisan));
                cmd.ExecuteNonQuery();
           
            }
            foreach (var item in Projekat.Instance.DodatneUsluge)
            {
                if (item.Id == du.Id)
                {
                    item.Id = du.Id;
                    item.Naziv = du.Naziv;
                    item.Cena = du.Cena;
                    item.Obrisan = du.Obrisan;
                }
            }
            return true;
        }
        public static ObservableCollection<DodatnaUsluga> PretraziUsluge(string tekst)
        {
            ObservableCollection<DodatnaUsluga> usluge = new ObservableCollection<DodatnaUsluga>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM DodatneUsluge WHERE Obrisan=@obrisan AND (Naziv LIKE @tekst OR Cena LIKE @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%"+tekst+"%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DodatnaUsluga du = new DodatnaUsluga()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1),
                        Cena = (double)reader.GetDecimal(2)
                    };
                    usluge.Add(du);

                }
            }
            return usluge;
        }

  
    }
}
