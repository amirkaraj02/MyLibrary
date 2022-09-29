using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;
using MyLibrary.App_Classes;

namespace MyLibrary.Controllers
{
    public class HuazoController : Controller
    {
        // GET: Huazo
        public ActionResult Index()
        {
            //linq sorgusu
            var valueLev = libraryDb.Connection.tblLevizjets.Where(x => x.StatusiPuneve == false).ToList();
            return View(valueLev);
        }


        [HttpGet]
        public ActionResult HuazoLibrin()
        {
            List<SelectListItem> deger1 = (from librat in libraryDb.Connection.tblLibers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = librat.Emri,
                                               Value = librat.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from perdoruesit in libraryDb.Connection.tblPerdoruesits.ToList()
                                           select new SelectListItem
                                           {
                                               Text = perdoruesit.Emri + " " + perdoruesit.Mbiemri,
                                               Value = perdoruesit.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;

            List<SelectListItem> deger3 = (from personeli in libraryDb.Connection.tblPersonelis.ToList()
                                           select new SelectListItem
                                           {
                                               Text = personeli.Emri + " " + personeli.Mbiemri,
                                               Value = personeli.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View();
        }

        [HttpPost]
        public ActionResult HuazoLibrin(tblLevizjet levizjet)
        {
            var libri = libraryDb.Connection.tblLibers.Where(k => k.ID == levizjet.tblLiber.ID).FirstOrDefault();
            var personeli = libraryDb.Connection.tblPersonelis.Where(k => k.ID == levizjet.tblPersoneli.ID).FirstOrDefault();
            var un = libraryDb.Connection.tblPerdoruesits.Where(k => k.ID == levizjet.tblPerdoruesit.ID).FirstOrDefault();

            levizjet.tblLiber = libri;
            levizjet.tblPersoneli = personeli;
            levizjet.tblPerdoruesit = un;
            levizjet.StatusiPuneve = false;

            libraryDb.Connection.tblLevizjets.Add(levizjet);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult RikthimiLibrit(int id)
        {
            var riktheLibrin = libraryDb.Connection.tblLevizjets.Find(id);
            return View(riktheLibrin);
        }

        [HttpPost]
        public ActionResult RikthimiLibrit(tblLevizjet levizjet)
        {
            var lv = libraryDb.Connection.tblLevizjets.Find(levizjet.ID);
            lv.DataRikthimitPerdoruesit = levizjet.DataRikthimitPerdoruesit;
            lv.StatusiPuneve = true;
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}