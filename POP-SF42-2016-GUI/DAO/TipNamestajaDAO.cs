﻿using PoP.Model;
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
        public static TipNamestaja DodavanjeTipa(TipNamestaja t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" INSERT INTO TipNamestaja(Naziv,Obrisan) VALUES (@naziv,@Obrisan)", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.CommandText += "SELECT SCOPE_IDENTITY();";

                int newId = int.Parse(cmd.ExecuteScalar().ToString());
                t.Id = newId;
            }
            Projekat.Instance.TipNamestaja.Add(t);
            return t;
        }
        public static bool BrisanjeTipa(TipNamestaja t)
        {
            t.Obrisan = true;
           return  IzmenaTipa(t);
        }
        public static bool IzmenaTipa(TipNamestaja t)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@" UPDATE TipNamestaja SET Naziv=@naziv, Obrisan=@obrisan WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@naziv", t.Naziv));
                cmd.Parameters.Add(new SqlParameter("@Id", t.Id));
                cmd.Parameters.Add(new SqlParameter("@obrisan", t.Obrisan));
                cmd.ExecuteNonQuery();
               
            }
            foreach (var item in Projekat.Instance.TipNamestaja)
            {
                if (item.Id == t.Id)
                {
                    item.Id = t.Id;
                    item.Naziv = t.Naziv;
                    item.Obrisan = t.Obrisan;
                }
            }
            return true;
        }
        public static ObservableCollection<TipNamestaja> PretraziTipove(string tekst)
        {
            ObservableCollection<TipNamestaja> tipovi = new ObservableCollection<TipNamestaja>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan AND Naziv like @tekst", conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                cmd.Parameters.Add(new SqlParameter("@tekst", "%"+tekst+"%"));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1)
                    };
                    tipovi.Add(tip);
                }
            }
            return tipovi;
        }
        public static ObservableCollection<TipNamestaja> SortirajTipove(string tekst)
        {
            ObservableCollection<TipNamestaja> tipovi = new ObservableCollection<TipNamestaja>();

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Konekcija"].ToString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"SELECT * FROM TipNamestaja WHERE Obrisan=@obrisan ORDER BY "+tekst, conn);
                cmd.Parameters.Add(new SqlParameter("@obrisan", '0'));
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TipNamestaja tip = new TipNamestaja()
                    {
                        Id = reader.GetInt32(0),
                        Naziv = reader.GetString(1)
                    };
                    tipovi.Add(tip);
                }
            }
            return tipovi;
        }
    }
}
