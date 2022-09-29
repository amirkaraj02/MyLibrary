using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;
using MyLibrary.App_Classes;


namespace MyLibrary.Controllers
{
    public class VitrinController : Controller
    {
        // GET: Vitrin
        [HttpGet]
        public ActionResult Index()
        {
            TablesClass tableCl = new TablesClass();
            tableCl.Libri = libraryDb.Connection.tblLibers.ToList();
            tableCl.RrethMeje = libraryDb.Connection.tblRrethNeshes.ToList();
            //var libFoto = libraryDb.Connection.tblLibers.ToList();
            return View(tableCl);
        }

        [HttpPost]
        public ActionResult Index(tblKontaktet kontaktet)
        {
            libraryDb.Connection.tblKontaktets.Add(kontaktet);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}