using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
     public class Patron
    {
    private int _id;
    private string _name;

        public Patron (string name, int id = 0)
        {
        _name = name;
         _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public  int GetId()
        {
            return _id;
        }

               public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO patrons (name) VALUES (@name);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);
            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
          public static List<Patron> GetAll()
        {
            List<Patron> allPatrons = new List<Patron> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Patron newPatron = new Patron(name, id);
                allPatrons.Add(newPatron);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allPatrons;
        }

    } 
}  