using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class AuthorsController : Controller
    {
        [HttpGet("/authors")]
        public ActionResult Index()
        {
            List<Author> allAuthors= Author.GetAll();
            return View(allAuthors);
        }
        [HttpGet("/authors/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/authors")]
        public ActionResult Index(string authorsname)
        {
            Author author = new Author(authorsname);
            author.Save();
            List<Author> allAuthors= Author.GetAll();
            return View(allAuthors);
        }
        [HttpGet("/authors/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            Author author= Author.Find(id);
            List<Book> authorBooks= author.GetBooks();
            model.Add("author",author);
            model.Add("authorBooks",authorBooks);
            return View(model);
        }
        [HttpGet("/authors/{id}/edit")]
        public ActionResult Edit(int id)
        {
            Author author = Author.Find(id);
            return View(author);
        }
        [HttpPost("/authors/{id}")]
        public ActionResult Show(string newAuthor, int id)
        {
            Author author = Author.Find(id);
            author.Edit(newAuthor);
            Dictionary<string,object> model= new Dictionary<string,object>();
            List<Book> authorBooks= author.GetBooks();
            model.Add("author",author);
            model.Add("authorBooks",authorBooks);
            return View(model);
        }

    }
}
