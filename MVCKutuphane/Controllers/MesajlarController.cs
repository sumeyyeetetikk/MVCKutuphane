using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var uyemail=(string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();
            return View(mesajlar);
        }
        public ActionResult YeniMesaj()
        {
             return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["MAIL"].ToString();
            t.GONDEREN = uyemail.ToString();
            t.TARIH = DateTime.Now;
            db.TBLMESAJLAR.Add(t);
            db.SaveChanges();
            return RedirectToAction("Giden","Mesajlar");
        }
        [HttpGet]
        [Authorize]
        public ActionResult Giden()
        {

            var uyemail = (string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR
                                  .Where(x => x.GONDEREN == uyemail)
                                  .OrderByDescending(x => x.ID)
                                  .ToList();
            return View(mesajlar);
        }
    }
}