using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Library;

namespace Library.Models
{
    public class Book
    {
        private int _id;
        private string _title;

        public Book(string title, int id = 0)
        {
            _title = title;
            _id = id;
        }
        public string GetTitle()
        {
            return _title;
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
            cmd.CommandText = @"INSERT INTO books (title) VALUES (@title);";

            MySqlParameter title = new MySqlParameter();
            title.ParameterName = "@title";
            title.Value = this._title;
            cmd.Parameters.Add(title);
            cmd.ExecuteNonQuery();
            _id = (int)cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<Book> GetAll()
        {
            List<Book> allBooks = new List<Book> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM books;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int id = rdr.GetInt32(0);
                string title = rdr.GetString(1);
                Book newBook = new Book(title, id);
                allBooks.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allBooks;
        }
        public void AddAuthor(int authorId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO authors_books (author_id, book_id) VALUES (@authorsId, @bookId);";

            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@bookId";
            book_id.Value = _id;
            cmd.Parameters.Add(book_id);

            MySqlParameter author_id = new MySqlParameter();
            author_id.ParameterName = "@authorsId";
            author_id.Value = authorId;
            cmd.Parameters.Add(author_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public void AddCopies(int copyNumbers)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO copies (copies_number, book_id) VALUES (@copyNumber, @bookId);";

            MySqlParameter book_id = new MySqlParameter();
            book_id.ParameterName = "@bookId";
            book_id.Value = _id;
            cmd.Parameters.Add(book_id);

            MySqlParameter copy = new MySqlParameter();
            copy.ParameterName = "@copyNumber";
            copy.Value = copyNumbers;
            cmd.Parameters.Add(copy);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static List<int> GetAllAvailable()
        {
            List<int> allBookIds = new List<int> { };
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM copies WHERE copies_number>0;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                int book_id = rdr.GetInt32(1);
                allBookIds.Add(book_id);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allBookIds;
        }
        public static List<Book> GetAvailableBooks()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT books.* FROM books
                JOIN copies ON (books.id = copies.book_id)
                WHERE copies.copies_number > 0;";
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Book> availableBooks = new List<Book> { };
            while (rdr.Read())
            {
                int bookId = rdr.GetInt32(0);
                string bookName = rdr.GetString(1);
                Book newBook = new Book(bookName, bookId);
                availableBooks.Add(newBook);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return availableBooks;
        }
        public static void Checkout(int bookId)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE copies SET copies_number = copies_number-1   WHERE id = @searchId;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = bookId;
            cmd.Parameters.Add(searchId);
            cmd.ExecuteNonQuery();

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
        public static Book Find(int Id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM `books` WHERE id = @bookdId;";

            MySqlParameter bookdId = new MySqlParameter();
            bookdId.ParameterName = "@bookdId";
            bookdId.Value = Id;
            cmd.Parameters.Add(bookdId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id = 0;
            string title = "";



            while (rdr.Read())
            {
                id = rdr.GetInt32(0);
                title = rdr.GetString(1);



            }
            Book foundBook = new Book(title, id);

            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return foundBook;
        }

    }
}