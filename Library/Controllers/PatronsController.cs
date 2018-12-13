using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        [HttpGet("/patrons")]
        public ActionResult Index()
        {
            List<Patron> allPatrons = Patron.GetAll();
            return View(allPatrons);
        }
        [HttpGet("/patrons/new")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost("/patrons")]
        public ActionResult Index(string patronname)
        {
            Patron patron = new Patron(patronname);
            patron.Save();
            List<Patron> allPatrons = Patron.GetAll();
            return View(allPatrons);
        }
        [HttpGet("/patrons/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            Patron patron = Patron.Find(id);
            List<Book> patronBooks= patron.GetBooks();
            model.Add("patron",patron);
            model.Add("patronBooks",patronBooks);
            return View(model);
        }
        [HttpGet("/patrons/{id}/checkout")]
        public ActionResult Checkout(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Patron patron = Patron.Find(id);
            List<Book> availablebooks = Book.GetAvailableBooks();
            model.Add("patron", patron);
            model.Add("availableBooks", availablebooks);
            return View(model);
        }
        [HttpPost("/patrons/{id}")]
        public ActionResult Show(int checkoutBook, DateTime dueDate, int id)
        {
            Dictionary<string,object> model= new Dictionary<string,object>();
            int copyNumber = Book.FindCopy(checkoutBook)-1;
            Book.Checkout(checkoutBook,copyNumber);
            Patron patron = Patron.Find(id);
            patron.AddCopiesPatrons(checkoutBook, dueDate);
            List<Book> patronBooks= patron.GetBooks();
            
            model.Add("patron",patron);
            model.Add("patronBooks",patronBooks);
            return View(model);
        }

    }
}
