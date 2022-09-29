using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;
using MyLibrary.App_Classes;
using System.Web.Helpers;
using System.IO;
using PagedList;

namespace MyLibrary.Controllers
{
    public class PerdoruesitController : Controller
    {
        // GET: Perdoruesit
        public ActionResult Index(int pages = 1)
        {
            //var perdoruesit = libraryDb.Connection.tblPerdoruesits.ToList();
            var perdoruesit = libraryDb.Connection.tblPerdoruesits.ToList().ToPagedList(pages, 5);
            return View(perdoruesit);
        }

        [HttpGet]
        public ActionResult Create()
        {
            // data anotation ile yapicagiz
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(tblPerdoruesit perdoruesit, HttpPostedFileBase Fotografia)
        {
            if (Fotografia!=null)
            {
                WebImage image = new WebImage(Fotografia.InputStream);
                string perdoruesiFoto = Guid.NewGuid() + Path.GetExtension(Fotografia.FileName);
                
                image.Save("~/Uploads/Perdoruesit/" + perdoruesiFoto);

                perdoruesit.Fotografia = "/Uploads/Perdoruesit/" + perdoruesiFoto;
            }

            libraryDb.Connection.tblPerdoruesits.Add(perdoruesit);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Update(int? id)
        {
            return View(libraryDb.Connection.tblPerdoruesits.Find(id));
        }

        [HttpPost]
        public ActionResult Update(int? id, tblPerdoruesit perdoruesit, HttpPostedFileBase Fotografia)
        {
            if (ModelState.IsValid)
            {
                var UpPerdor = libraryDb.Connection.tblPerdoruesits.Where(x => x.ID == id).SingleOrDefault();

                if (Fotografia != null)
                {
                    //Daha Once kaydetigimiz bir dosya var mi yok mu kontrollunu yapabilmek icin kullanilan bir metod
                    if (System.IO.File.Exists(Server.MapPath(UpPerdor.Fotografia)))
                    {
                        System.IO.File.Delete(Server.MapPath(UpPerdor.Fotografia));
                    }

                    WebImage image = new WebImage(Fotografia.InputStream);
                    string perdoruesiFoto = Guid.NewGuid() + Path.GetExtension(Fotografia.FileName);

                    image.Save("~/Uploads/Perdoruesit/" + perdoruesiFoto);

                    UpPerdor.Fotografia = "/Uploads/Perdoruesit/" + perdoruesiFoto;
                }

                UpPerdor.Emri = perdoruesit.Emri;
                UpPerdor.Mbiemri = perdoruesit.Mbiemri;
                UpPerdor.EMail = perdoruesit.EMail;
                UpPerdor.UserName = perdoruesit.UserName;
                UpPerdor.Password = perdoruesit.Password;
                UpPerdor.Telefoni = perdoruesit.Telefoni;
                UpPerdor.Shkolla = perdoruesit.Shkolla;

                libraryDb.Connection.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(perdoruesit);
        }

        public ActionResult Delete(int? id)
        {
           
            var delPerdo = libraryDb.Connection.tblPerdoruesits.Find(id);
            if (System.IO.File.Exists(Server.MapPath(delPerdo.Fotografia)))
            {
                System.IO.File.Delete(Server.MapPath(delPerdo.Fotografia));
            }
            libraryDb.Connection.tblPerdoruesits.Remove(delPerdo);
            libraryDb.Connection.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}