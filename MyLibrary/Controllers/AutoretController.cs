using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class AutoretController : Controller
    {
        // GET: Autoret
        db_LibraryEntities db = new db_LibraryEntities();
        public ActionResult Index()
        {
            var autoret = db.tblAutors.ToList();
            return View(autoret);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Create(tblAutor autoret)
        {
            db.tblAutors.Add(autoret);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete( int id)
        {
            var autorDel = db.tblAutors.Find(id);
            db.tblAutors.Remove(autorDel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Select(int id)
        {
            var autorUp = db.tblAutors.Find(id);
            return View("Select", autorUp);
        }

        public ActionResult Update(tblAutor autor)
        {
            var autorUp = db.tblAutors.Find(autor.ID);
            autorUp.Emri = autor.Emri;
            autorUp.Mbiemri = autor.Mbiemri;
            autorUp.Detaje = autor.Detaje;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}