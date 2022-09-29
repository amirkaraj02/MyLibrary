using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLibrary.Models;

namespace MyLibrary.App_Classes
{
    public class TablesClass
    {
        public IEnumerable<tblLiber> Libri { get; set; }
        public IEnumerable<tblRrethNesh> RrethMeje { get; set; }
    }
}