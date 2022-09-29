using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyLibrary.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        db_LibraryEntities db = new db_LibraryEntities();
        public ActionResult Index()
        {
            var ktg = db.tblKategoris.ToList();
            return View(ktg);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(tblKategori kategori)
        {
            db.tblKategoris.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Update(int id)
        {
            var upKtg = db.tblKategoris.Find(id);
            return View(upKtg);
        }

        [HttpPost]
        public ActionResult Update( tblKategori kategori)
        {
            // var ktgUp = db.tblKategoris.Where(x => x.ID == id).SingleOrDefault();
            var ktgUp = db.tblKategoris.Find(kategori.ID);
            ktgUp.Emri = kategori.Emri;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            return View(db.tblKategoris.Find(id));
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var ktgDel = db.tblKategoris.Find(id);
            db.tblKategoris.Remove(ktgDel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}