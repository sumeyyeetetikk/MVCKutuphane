using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCKutuphane.Controllers
{
 
    public class PanelimController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        [HttpGet]

     
        public ActionResult Index()
        {
            var uyeMail = (string)Session["MAIL"];
            if (string.IsNullOrEmpty(uyeMail))
            {
                return RedirectToAction("Index", "Login");
            }

            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyeMail);
            if (uye == null) return HttpNotFound();
            int toplamOkunan = db.TBLHAREKET.Count(x => x.UYE == uye.ID);
            ViewBag.ToplamKitap = toplamOkunan;
            // Toplam Sayı
            int elindekiKitapSayisi = db.TBLHAREKET.Count(x => x.UYE == uye.ID && x.ISLEMDURUM == false);
            ViewBag.ElindekiKitap = elindekiKitapSayisi;

            // Kitap Listesi
            var elindekiKitaplar = db.TBLHAREKET
                                      .Where(x => x.UYE == uye.ID && x.ISLEMDURUM == false)
                                      .ToList();
            ViewBag.KitapListesi = elindekiKitaplar;

            return View(uye);
        }

        [HttpPost]
        public ActionResult Index2(TBLUYELER p)
        {
            var kullanici = (string)Session["MAIL"];
            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);

            if (uye != null)
            {
                uye.AD = p.AD;
                uye.SOYAD = p.SOYAD;
                uye.KULLANICIADI = p.KULLANICIADI;
                uye.SIFRE = p.SIFRE;
                uye.OKUL = p.OKUL;
                uye.FOTOGRAF = p.FOTOGRAF?.Trim();

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
         
        public ActionResult Kitaplarim()
        {
            var kullanici = (string)Session["MAIL"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici.ToString()).Select(y => y.ID).FirstOrDefault();
            var degerler = db.TBLHAREKET.Where(x => x.UYE == id).ToList();
            return View(degerler);
        }

        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
            return View(duyurulistesi);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GirisYap", "Login");
        }
    }
}