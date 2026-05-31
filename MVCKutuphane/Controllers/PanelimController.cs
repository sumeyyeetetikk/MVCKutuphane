using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]
        [Authorize]
        public ActionResult Index()
        {
            var uyeMail = (string)Session["MAIL"];
            var degerler = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyeMail);
            return View(degerler);
        }

        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["MAIL"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);
            uye.SIFRE = p.SIFRE;
            uye.AD = p.AD;
            uye.FOTOGRAF = p.FOTOGRAF;
            uye.SOYAD = p.SOYAD;
            uye.OKUL = p.OKUL;
            uye.KULLANICIADI = p.KULLANICIADI;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["MAIL"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(y => y.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }
    }
}