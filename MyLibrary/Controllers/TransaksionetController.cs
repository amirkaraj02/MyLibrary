using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyLibrary.Models;
using MyLibrary.App_Classes;

namespace MyLibrary.Controllers
{
    public class TransaksionetController : Controller
    {
        // GET: Transaksionet
        public ActionResult Index()
        {
            var libRikthyer = libraryDb.Connection.tblLevizjets.Where(x => x.StatusiPuneve == true).ToList();
            return View(libRikthyer);
        }
    }
}