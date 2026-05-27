using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class KayitOlController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Kayit()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(TBLUYELER p)
        {

            p.FOTOGRAF = "https://images.placeholder.com/150";

            if (!ModelState.IsValid)
            {
                return View("Kayit");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}