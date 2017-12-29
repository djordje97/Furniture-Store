using PoP.Model;
using POP_SF42_2016_GUI.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace POP_SF42_2016_GUI.DAO
{
    public class ProdajaDAO
    {
        public static ObservableCollection<ProdajaNamestaja> SveProdaje()
        {
            ObservableCollection<ProdajaNamestaja> prodaje = new ObservableCollection<ProdajaNamestaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Id,Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos FROM Prodaja WHERE Obrisan=@obrisan", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ProdajaNamestaja p = new ProdajaNamestaja()
                    { Id = reader.GetInt32(0),
                        Kupac = reader.GetString(1),
                        BrojRacuna = reader.GetInt32(2),
                        DatumProdaje = (DateTime)reader.GetDateTime(3),
                        Obrisan = false
                      
                    };

                    prodaje.Add(p);
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<StavkaProdaje> stavke = new ObservableCollection<StavkaProdaje>();
                    cmd = new SqlCommand(@"SELECT Id, Kolicina,Cena,NamestajId FROM Stavka WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StavkaProdaje s = new StavkaProdaje()
                        {
                            Id = reader.GetInt32(0),
                            Kolicina = reader.GetInt32(1),
                            //NamestajProdaja = NamestajDAO.NametajPoId(reader.GetInt32(3)),
                            NamestajProdajaId = reader.GetInt32(3),
                            Obrisan = false 
                        };
                        stavke.Add(s);
                    }
                    prodaja.StavkeProdaje = stavke;
                    reader.Close();
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<DodatnaUsluga> usluge = new ObservableCollection<DodatnaUsluga>();
                    cmd = new SqlCommand(@"SELECT UslugeId FROM ProdateUsluge WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        //if (UslugeDAO.UslugaPoId(reader.GetInt32(0)) != null)
                        //   usluge.Add(UslugeDAO.UslugaPoId(reader.GetInt32(0)));
                        prodaja.DodatneUslugeId.Add(reader.GetInt32(0));
                        
                    }
                 //prodaja.DodatneUsluge = usluge;
                    reader.Close();
                }
            }

            return prodaje;
        }
        public static ProdajaNamestaja DodajProdaju(ProdajaNamestaja p)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"INSERT INTO Prodaja(Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos,Obrisan) VALUES(@kupac,@brojR,@datumProdaje,@ukupanIznos,@obrisan) ", conn);
                    cmd.CommandText += "SELECT SCOPE_IDENTITY();";
                    cmd.Parameters.Add(new SqlParameter("@kupac", p.Kupac));
                    cmd.Parameters.Add(new SqlParameter("@brojR", p.BrojRacuna));
                    cmd.Parameters.Add(new SqlParameter("@datumProdaje", p.DatumProdaje));
                    cmd.Parameters.Add(new SqlParameter("@ukupanIznos", p.UkupanIznos));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    int newId = int.Parse(cmd.ExecuteScalar().ToString());
                    p.Id = newId;

                    for (int i = 0; i < p.StavkeProdaje.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@"INSERT INTO Stavka(Kolicina,Cena,NamestajId,ProdajaId,Obrisan) VALUES(@kolicina,@cena,@namestajId,@prodajaId,@obrisan) ", conn);
                        cm.Parameters.Add(new SqlParameter("@kolicina", p.StavkeProdaje[i].Kolicina));
                        cm.Parameters.Add(new SqlParameter("@cena", p.StavkeProdaje[i].Cena));
                        cm.Parameters.Add(new SqlParameter("@namestajId", p.StavkeProdaje[i].NamestajProdaja.Id));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                        cm.ExecuteNonQuery();
                        p.StavkeProdaje[i].NamestajProdaja.Kolicina = p.StavkeProdaje[i].NamestajProdaja.Kolicina - p.StavkeProdaje[i].Kolicina;
                        NamestajDAO.IzmenaNamestaja(p.StavkeProdaje[i].NamestajProdaja);
                        foreach (var namestaj in Projekat.Instance.Namestaj)
                        {
                            if (namestaj.Id == p.StavkeProdaje[i].NamestajProdaja.Id)
                                namestaj.Kolicina = p.StavkeProdaje[i].NamestajProdaja.Kolicina;
                            if (namestaj.Kolicina == 0)
                            {
                                namestaj.Obrisan = true;
                                NamestajDAO.BrisanjeNamestaja(namestaj);
                                Projekat.Instance.Namestaj.Remove(namestaj);
                            }
                        }

                    }

                    for (int i = 0; i < p.DodatneUsluge.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@"INSERT INTO ProdateUsluge(UslugeId,ProdajaId,Obrisan) VALUES(@usluge,@prodajaId,@obrisan) ", conn);
                        cm.Parameters.Add(new SqlParameter("@usluge", p.DodatneUsluge[i].Id));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                        cm.ExecuteNonQuery();
                    }

                }
                Projekat.Instance.Prodaja.Add(p);
                return p;
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return null; }

        }
        public static bool IzmenaProdaje(ProdajaNamestaja p)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@" UPDATE Prodaja SET Kupac=@kupac,Datum_Prodaje=@datum WHERE Id=@id", conn);
                    cmd.Parameters.Add(new SqlParameter("@Kupac", p.Kupac));
                    cmd.Parameters.Add(new SqlParameter("@datum", p.DatumProdaje));
                    cmd.Parameters.Add(new SqlParameter("@id", p.Id));
                    cmd.ExecuteNonQuery();

                    foreach (var item in Projekat.Instance.Prodaja)
                    {
                        if (item.Id == p.Id)
                        {
                            item.Kupac = p.Kupac;
                            item.DatumProdaje = p.DatumProdaje;
                            item.StavkeProdaje = p.StavkeProdaje;
                            item.DodatneUsluge = p.DodatneUsluge;
                            item.UkupanIznos = p.UkupanIznos;
                        }
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
           
            
        }
      
        public static bool ObrisiStavku(ProdajaNamestaja p, ObservableCollection<StavkaProdaje> stavke)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    for (int i = 0; i < stavke.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" UPDATE  Stavka SET Obrisan=@obrisan WHERE Id=@id AND ProdajaId=@prodajaId", conn);
                        cm.Parameters.Add(new SqlParameter("@id", stavke[i].Id));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '1'));
                        cm.ExecuteNonQuery();

                        var n = stavke[i].NamestajProdaja;
                        n.Kolicina += stavke[i].Kolicina;
                        foreach (var namestaj in Projekat.Instance.Namestaj)
                        {
                            if (namestaj.Id == n.Id)
                                namestaj.Kolicina = n.Kolicina;
                        }
                        NamestajDAO.IzmenaNamestaja(n);
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            


        }
        public static bool DodajStavku(ProdajaNamestaja p, ObservableCollection<StavkaProdaje> stavke)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    for (int i = 0; i < stavke.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" INSERT INTO Stavka(Kolicina,Cena,NamestajId,ProdajaId,Obrisan) VALUES (@kolicina,@cena,@namestajId,@prodajaId,@obrisan)", conn);
                        cm.Parameters.Add(new SqlParameter("@namestajId", stavke[i].NamestajProdaja.Id));
                        cm.Parameters.Add(new SqlParameter("@kolicina", stavke[i].Kolicina));
                        cm.Parameters.Add(new SqlParameter("@cena", stavke[i].Cena));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                        cm.ExecuteNonQuery();


                        foreach (var namestaj in Projekat.Instance.Namestaj)
                        {
                            if (namestaj.Id == stavke[i].NamestajProdaja.Id)
                            {
                                namestaj.Kolicina = namestaj.Kolicina - stavke[i].Kolicina;
                                NamestajDAO.IzmenaNamestaja(namestaj);
                            }
                            if (namestaj.Kolicina == 0)
                            {
                                namestaj.Obrisan = true;
                                NamestajDAO.BrisanjeNamestaja(namestaj);
                                Projekat.Instance.Namestaj.Remove(namestaj);
                            }

                        }
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            


        }
        public static bool ObrisiUslugu(ProdajaNamestaja p, ObservableCollection<DodatnaUsluga> usluge)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {

                    conn.Open();
                    for (int i = 0; i < usluge.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" UPDATE  ProdateUsluge SET Obrisan=@obrisan WHERE UslugeId=@id AND ProdajaId=@prodajaId", conn);
                        cm.Parameters.Add(new SqlParameter("@id", usluge[i].Id));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '1'));
                        cm.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
            

        }
        public static bool DodajUslugu(ProdajaNamestaja p,ObservableCollection<DodatnaUsluga> usluge)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
                {
                    conn.Open();
                    for (int i = 0; i < usluge.Count; i++)
                    {
                        SqlCommand cm = new SqlCommand(@" INSERT INTO ProdateUsluge(UslugeId,ProdajaId,Obrisan) VALUES (@uslugeId,@prodajaId,@obrisan)", conn);
                        cm.Parameters.Add(new SqlParameter("@uslugeId", usluge[i].Id));
                        cm.Parameters.Add(new SqlParameter("@prodajaId", p.Id));
                        cm.Parameters.Add(new SqlParameter("@obrisan", '0'));
                        cm.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch { MessageBox.Show("Upis u bazu nije uspeo.\nMolimo da pokusate ponovo!", "Greska", MessageBoxButton.OK, MessageBoxImage.Warning); return false; }
          

        }
        public static ObservableCollection<ProdajaNamestaja> PretraziProdaju(string tekst)
        {
            ObservableCollection<ProdajaNamestaja> prodaje = new ObservableCollection<ProdajaNamestaja>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT Id,Kupac,Broj_Racuna,Datum_Prodaje,Ukupan_Iznos FROM Prodaja WHERE Obrisan=@obrisan 
                AND(Kupac LIKE @tekst OR Broj_Racuna LIKE @tekst OR Datum_Prodaje LIKE @tekst OR Ukupan_Iznos LIKE @tekst)", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%"+tekst+"%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    ProdajaNamestaja p = new ProdajaNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Kupac = reader.GetString(1),
                        BrojRacuna = reader.GetInt32(2),
                        DatumProdaje = (DateTime)reader.GetDateTime(3),
                        Obrisan = false

                    };

                    prodaje.Add(p);
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<StavkaProdaje> stavke = new ObservableCollection<StavkaProdaje>();
                    cmd = new SqlCommand(@"SELECT Id, Kolicina,Cena,NamestajId FROM Stavka WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StavkaProdaje s = new StavkaProdaje()
                        {
                            Id = reader.GetInt32(0),
                            Kolicina = reader.GetInt32(1),
                            //NamestajProdaja = NamestajDAO.NametajPoId(reader.GetInt32(3)),
                            NamestajProdajaId = reader.GetInt32(3),
                            Obrisan = false
                        };
                        stavke.Add(s);
                    }
                    prodaja.StavkeProdaje = stavke;
                    reader.Close();
                }
                reader.Close();
                foreach (var prodaja in prodaje)
                {
                    ObservableCollection<DodatnaUsluga> usluge = new ObservableCollection<DodatnaUsluga>();
                    cmd = new SqlCommand(@"SELECT UslugeId FROM ProdateUsluge WHERE ProdajaId=@id AND Obrisan=@obrisan", conn);
                    cmd.Parameters.Add(new SqlParameter("@id", prodaja.Id));
                    cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        //if (UslugeDAO.UslugaPoId(reader.GetInt32(0)) != null)
                        //   usluge.Add(UslugeDAO.UslugaPoId(reader.GetInt32(0)));
                        prodaja.DodatneUslugeId.Add(reader.GetInt32(0));

                    }
                    //prodaja.DodatneUsluge = usluge;
                    reader.Close();
                }
            }

            foreach (var prodaja in prodaje)
            {
                foreach (var stavka in prodaja.StavkeProdaje)
                {
                    stavka.NamestajProdaja = Namestaj.PronadjiNamestaj(stavka.NamestajProdajaId);
                }
                foreach (var u in prodaja.DodatneUslugeId)
                {
                    prodaja.DodatneUsluge.Add(DodatnaUsluga.PronadjiUslugu(u));
                }
            }

            return prodaje;

        }
      
    }
}
