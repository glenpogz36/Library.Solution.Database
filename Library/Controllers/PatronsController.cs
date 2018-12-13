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
            Patron patron = Patron.Find(id);

            return View(patron);
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
            Book.Checkout(checkoutBook);
            Patron patron = Patron.Find(id);
            patron.AddCopiesPatrons(checkoutBook, dueDate);

            // Dictionary<string, object> model = new Dictionary<string, object>();
            // Patron patron = Patron.Find(id);
            // List<int> allAvailableBooksIds = Book.GetAvailableBooks();

            // model.Add("patron", patron);
            // model.Add("availableBooks", availablebooks);
            return View();
        }

    }
}
