using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class LibratController : Controller
    {
        // GET: Librat
        db_LibraryEntities db = new db_LibraryEntities();
        public ActionResult Index(string libri)
        {
            // var librat = db.tblLibers.ToList();
            var librat = from lib in db.tblLibers select lib;
            if (!string.IsNullOrEmpty(libri))
            {
                librat = librat.Where(l => l.Emri.Contains(libri));
            }
            return View(librat.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            //linq sorgusu yazilacak
            List<SelectListItem> deger1 = (from kategoriList in db.tblKategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = kategoriList.Emri,
                                               Value = kategoriList.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from autorList in db.tblAutors.ToList()
                                           select new SelectListItem
                                           {
                                               Text = autorList.Emri + " " + autorList.Mbiemri,
                                               Value = autorList.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View();
        }
        [HttpPost]
        public ActionResult Create(tblLiber liber)
        {
            var kategori = db.tblKategoris.Where(k => k.ID == liber.tblKategori.ID).FirstOrDefault();
            var autor = db.tblAutors.Where(a => a.ID == liber.tblAutor.ID).FirstOrDefault();
            liber.tblKategori = kategori;
            liber.tblAutor = autor;
            liber.GjendjaLibrit = true;
            db.tblLibers.Add(liber);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var liberDel = db.tblLibers.Find(id);
            db.tblLibers.Remove(liberDel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            List<SelectListItem> value1 = (from kategoriList in db.tblKategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = kategoriList.Emri,
                                               Value = kategoriList.ID.ToString()
                                           }).ToList();
            ViewBag.val1 = value1;

            List<SelectListItem> autoret = (from autorList in db.tblAutors.ToList()
                                            select new SelectListItem
                                            {
                                                Text = autorList.Emri + " " + autorList.Mbiemri,
                                                Value = autorList.ID.ToString()
                                            }).ToList();
            ViewBag.val2 = autoret;

            return View(db.tblLibers.Find(id));
        }

        [HttpPost]
        public ActionResult Update(tblLiber liber)
        {
            var liberUp = db.tblLibers.Find(liber.ID);
            liberUp.Emri = liber.Emri;
            liberUp.VitiShtypjes = liber.VitiShtypjes;
            liberUp.ShtepiaBotuse = liber.ShtepiaBotuse;
            liberUp.Faqe = liber.Faqe;
            var kategori = db.tblKategoris.Where(kat => kat.ID == liber.tblKategori.ID).FirstOrDefault();
            var autori = db.tblAutors.Where(au => au.ID == liber.tblAutor.ID).FirstOrDefault();
            liberUp.KategoriID = kategori.ID;
            liberUp.AutorID = autori.ID;
            liberUp.GjendjaLibrit = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}