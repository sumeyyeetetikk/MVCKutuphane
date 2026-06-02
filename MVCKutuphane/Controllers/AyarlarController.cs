using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult Index()
        {
            var kullanicilar = db.TBLADMIN.ToList();
            return View(kullanicilar);
        }

        public ActionResult YeniAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniAdmin(TBLADMIN p)
        {
            db.TBLADMIN.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            db.TBLADMIN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AdminGüncelle(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            return View("AdminGüncelle",admin);
        }
        [HttpPost]
        public ActionResult AdminGüncelle(TBLADMIN p)
        {
            var admin = db.TBLADMIN.Find(p.ID);
            admin.Kullanici = p.Kullanici;
            admin.Sifre = p.Sifre;
            admin.Yetki = p.Yetki;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
