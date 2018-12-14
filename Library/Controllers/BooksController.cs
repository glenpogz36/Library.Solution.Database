using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class BooksController : Controller
    {
        [HttpGet("/books")]
        public ActionResult Index()
        {
            List<Book> allBooks= Book.GetAll();
            return View(allBooks);
        }
        [HttpGet("/books/new")]
        public ActionResult New()
        {
             List<Author> allAuthors= Author.GetAll();
            return View(allAuthors);
        }
        [HttpPost("/books")]
        public ActionResult Index(string booktitle, int bookauthor, int copies)
        {
            Book books = new Book(booktitle);
            books.Save();
            books.AddAuthor(bookauthor);
            books.AddCopies(copies);
            List<Book> allBooks= Book.GetAll();

            return View(allBooks);
        }
        [HttpGet("/books/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model = new Dictionary<string,object>();
            Book book= Book.Find(id);
            Author author= book.GetAuthor();
            List<Patron> patrons = book.GetPatrons();
            model.Add("book",book);
            model.Add("author",author);
            model.Add("patrons",patrons);
            return View(model);
        }
        [HttpGet("/books/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Book book = Book.Find(id);
            return View(book);
        }
        [HttpPost("/books/{id}")]
        public ActionResult Show(string newTitle, int id)
        {
            Book book = Book.Find(id);
            book.Edit(newTitle);
            Dictionary<string,object> model = new Dictionary<string,object>();
            Author author= book.GetAuthor();
            List<Patron> patrons = book.GetPatrons();
            model.Add("book",book);
            model.Add("author",author);
            model.Add("patrons",patrons);
            return View(model);
        }
    }
}
