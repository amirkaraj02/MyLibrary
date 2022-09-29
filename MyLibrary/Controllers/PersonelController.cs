using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;
using MyLibrary.App_Classes;

namespace MyLibrary.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        public ActionResult Index()
        {
            var personel = libraryDb.Connection.tblPersonelis.ToList();
            return View(personel);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblPersoneli personeli)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            libraryDb.Connection.tblPersonelis.Add(personeli);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            return View(libraryDb.Connection.tblPersonelis.Find(id));
        }

        [HttpPost]
        public ActionResult Update(tblPersoneli personeli)
        {
            var upPersonel = libraryDb.Connection.tblPersonelis.Find(personeli.ID);
            upPersonel.Emri = personeli.Emri;
            upPersonel.Mbiemri = personeli.Mbiemri;
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Delete(int? id)
        {
            var delPer = libraryDb.Connection.tblPersonelis.Find(id);
            libraryDb.Connection.tblPersonelis.Remove(delPer);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}