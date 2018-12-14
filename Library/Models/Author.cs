using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
     public class Author
    {
    private int _id;
    private string _name;

        public Author (string name, int id = 0)
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
            cmd.CommandText = @"INSERT INTO authors (name) VALUES (@name);";

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
          public static List<Author> GetAll()
        {
            List<Author> allAuthors = new List<Author> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM authors;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string name = rdr.GetString(1);
                Author newAuthor = new Author(name, id);
                allAuthors.Add(newAuthor);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allAuthors;
        }
        public static Author Find(int Id)
        {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM `authors` WHERE id = @authorId;";

        MySqlParameter authorId = new MySqlParameter();
        authorId.ParameterName = "@authorId";
        authorId.Value = Id;
        cmd.Parameters.Add(authorId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int id = 0;
        string authorNAme = "";
        


        while (rdr.Read())
        {
            id = rdr.GetInt32(0);
            authorNAme = rdr.GetString(1);


        }
        Author foundAuthor = new Author(authorNAme, id);

        conn.Close();
        if(conn != null)
            {
            conn.Dispose();
            }
            return foundAuthor;
        }

        public List<Book> GetBooks()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT books.* FROM authors
                JOIN authors_books ON (authors.id = authors_books.author_id)
                JOIN books ON (authors_books.book_id = books.id)
                WHERE authors.id = @authorId;";
            MySqlParameter authurIdParameter = new MySqlParameter();
            authurIdParameter.ParameterName = "@authorId";
            authurIdParameter.Value = _id;
            cmd.Parameters.Add(authurIdParameter);
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


        public void Edit(string newName)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"UPDATE authors SET name = @newName WHERE id = @searchId;";
         MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = _id;
        cmd.Parameters.Add(searchId);
         MySqlParameter name = new MySqlParameter();
        name.ParameterName = "@newName";
        name.Value = newName;
        cmd.Parameters.Add(name);
         cmd.ExecuteNonQuery();
        _name = newName;
         conn.Close();
         if (conn != null)
        {
          conn.Dispose();
        }

      } 
    } 
}  