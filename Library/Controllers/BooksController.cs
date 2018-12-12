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
    }
}
