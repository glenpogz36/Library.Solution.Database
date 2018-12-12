using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    public class PatronsController : Controller
    {
        [HttpGet("/patrons")]
        public ActionResult Index()
        {
            List<Patron> allPatrons= Patron.GetAll();
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
            List<Patron> allPatrons= Patron.GetAll();
            return View(allPatrons);
        }
    }
}
