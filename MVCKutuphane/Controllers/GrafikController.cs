using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models;
using MVCKutuphane.Models.Entity; 

namespace MVCKutuphane.Controllers
{
    public class GrafikController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VisualizeKitapResult()
        {
            return Json(KitapListesi(), JsonRequestBehavior.AllowGet);
        }


        public List<Class1> KitapListesi()
        {
            List<Class1> liste = new List<Class1>();

            liste = db.TBLKITAP
                      .GroupBy(x => x.YAYIMEVI)
                      .Select(g => new Class1
                      {
                          yayinevi = g.Key,
                          sayi = g.Count()
                      }).ToList();

            return liste;
        }
    }
}