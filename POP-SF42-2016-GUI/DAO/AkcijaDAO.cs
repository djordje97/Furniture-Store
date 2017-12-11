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
    public class AkcijaDAO
    {
        public static ObservableCollection<Akcija> SveAkcije()
        {
            ObservableCollection<Akcija> akcije = new ObservableCollection<Akcija>();
            ObservableCollection<Namestaj> namestaj = new ObservableCollection<Namestaj>();
            ObservableCollection<Namestaj> namestajAkcija = new ObservableCollection<Namestaj>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM Akcija join Namestaj on Akcija.Namestaj=Namestaj.Id WHERE Akcija.Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Namestaj n = new Namestaj()
                    {
                        Id=reader.GetInt32(6),
                        Naziv=reader.GetString(7),
                        Kolicina=reader.GetInt32(8),
                        Sifra=reader.GetString(9),
                        TipNamestaja=TipNamestajaDAO.TipPoId(reader.GetInt32(10)),
                        Cena=(double)reader.GetDecimal(11)
                    };
                    namestaj.Add(n);
                    Akcija a = new Akcija()
                    {
                        Id = reader.GetInt32(0),
                        PocetakAkcije = (DateTime)reader.GetDateTime(1),
                        KrajAkcije = (DateTime)reader.GetDateTime(2),
                        NamestajPopust = namestaj,
                        Popust = (double)reader.GetDecimal(4)
                    };
                    akcije.Add(a);
                }
            }
            return akcije;
        }
        public static bool BrisanjeAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE Akcija SET Obrisan=@obrisan WHERE DatP=@datP AND DatK=@datK ", conn);
                cmd.Parameters.Add(new SqlParameter("@datP", a.PocetakAkcije));
                cmd.Parameters.Add(new SqlParameter("@datK", a.KrajAkcije));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '1'));
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        public static bool DodavanjeAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {

                for (int i = 0; i < a.NamestajPopust.Count; i++)
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" INSERT INTO Akcija(DatP,DatK,Namestaj,Popust,Obrisan) VALUES (@datP,@datK,@namestaj,@popust,@Obrisan)", conn);
                    cmd.Parameters.Add(new SqlParameter("@datP", a.PocetakAkcije));
                    cmd.Parameters.Add(new SqlParameter("@datK", a.KrajAkcije));
                    cmd.Parameters.Add(new SqlParameter("@namestaj", a.NamestajPopust[i].Id));
                    cmd.Parameters.Add(new SqlParameter("@popust", a.Popust));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
        }
        public static bool IzmenaAkcije(Akcija a)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                for (int i = 0; i < a.NamestajPopust.Count; i++)
                {
                    SqlCommand cmd = new SqlCommand(@" UPDATE Akcija SET DatP=@datP,DatK=@datK,Namestaj=@namestaj,Popust=@popust WHERE DatP=@datP AND DatK=@datK", conn);
                    cmd.Parameters.Add(new SqlParameter("@datp", a.PocetakAkcije));
                    cmd.Parameters.Add(new SqlParameter("@datK", a.KrajAkcije));
                    cmd.Parameters.Add(new SqlParameter("@Namestaj", a.NamestajPopust[i].Id));
                    cmd.Parameters.Add(new SqlParameter("@popust", a.Popust));
                    cmd.Parameters.Add(new SqlParameter("@id", a.Id));
                    cmd.ExecuteNonQuery();
                }
        
                return true;
            }
        }
    }
}
