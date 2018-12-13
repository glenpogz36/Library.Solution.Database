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

        public Patron(string name, int id = 0)
        {
            _name = name;
            _id = id;
        }
        public string GetName()
        {
            return _name;
        }
        public int GetId()
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
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Patron> GetAll()
        {
            List<Patron> allPatrons = new List<Patron> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
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
        public static Patron Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM patrons WHERE id = @patronId;";

            MySqlParameter patronId = new MySqlParameter();
            patronId.ParameterName = "@patronId";
            patronId.Value = Id;
            cmd.Parameters.Add(patronId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string patronname = "";
            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                patronname = rdr.GetString(1);
            }
            Patron foundPatron = new Patron(patronname, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundPatron;
        }
        public void AddCopiesPatrons(int bookId, DateTime dueDate)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO copies_patrons (book_id,patron_id,due) VALUES (@bookId,@patronId,@dueDate);";

            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@bookId";
            book_id.Value = bookId;
            cmd.Parameters.Add(book_id);

            MySqlParameter patron_id = new MySqlParameter();
            patron_id.ParameterName = "@patronId";
            patron_id.Value = _id;
            cmd.Parameters.Add(patron_id);

            MySqlParameter due_date = new MySqlParameter();
            due_date.ParameterName = "@dueDate";
            due_date.Value = dueDate;
            cmd.Parameters.Add(due_date);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public List<Book> GetBooks()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT books.* FROM patrons
                JOIN copies_patrons ON (patrons.id = copies_patrons.patron_id)
                JOIN books ON (copies_patrons.book_id = books.id)
                WHERE patrons.id = @patronId;";
            MySqlParameter patronIdParameter = new MySqlParameter();
            patronIdParameter.ParameterName = "@patronId";
            patronIdParameter.Value = _id;
            cmd.Parameters.Add(patronIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Book> books = new List<Book>{};
            while(rdr.Read())
            {
            int bookId = rdr.GetInt32(0);
            string bookTitle = rdr.GetString(1);

            Book newBook = new Book(bookTitle, bookId);
            books.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
            conn.Dispose();
            }
            return books;
            }

    }
}