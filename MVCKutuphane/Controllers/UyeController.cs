using MVCKutuphane.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class UyeController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa,5);
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CreateUye()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUye(TBLUYELER p)
        {
            if(!ModelState.IsValid)
            {
                return View("CreateUye");
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var uyeler = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(uyeler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeGetir(int id)

        {
            var uye = db.TBLUYELER.Find(id);
            return View("UyeGetir", uye);
        }
        public ActionResult UYeGuncelle(TBLUYELER p)
        {
            var uye = db.TBLUYELER.Find(p.ID);
            uye.AD = p.AD;
            uye.SOYAD = p.SOYAD;
            uye.MAIL = p.MAIL;
            uye.KULLANICIADI = p.KULLANICIADI;
            uye.SIFRE = p.SIFRE;
            uye.OKUL = p.OKUL;
            uye.FOTOGRAF = p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UyeKitapGecmis(int id)
        {
            var uyemail = (string)Session["MAIL"];

            var uye = db.TBLUYELER.Find(id);
            if (uye != null)
            {
                ViewBag.UyeAdi = uye.AD + " " + uye.SOYAD;
            }

            var ktpgcms = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(ktpgcms);
        }
    }
}