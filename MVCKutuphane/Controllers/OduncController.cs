using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x => x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD + " " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            List<SelectListItem> deger2 = (from x in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AD,
                                               Value = x.ID.ToString()
                                           }).ToList();

            List<SelectListItem> deger3 = (from x in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.PERSONEL,
                                               Value = x.ID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            var d1= db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();
            var d2= db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3= db.TBLPERSONEL.Where(x => x.ID == p.TBLPERSONEL.ID).FirstOrDefault();
            p.TBLUYELER= d1;
            p.TBLKITAP= d2;
            p.TBLPERSONEL= d3;
            db.TBLHAREKET.Add(p);

            var kitap = db.TBLKITAP.Find(p.KITAP);
            kitap.DURUM = false;

            db.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult OduncIade(int id)
        {
            var odn = db.TBLHAREKET.Find(id);


            if (odn.IADETARIH != null)
            {
                DateTime d1 = Convert.ToDateTime(odn.IADETARIH);
                DateTime d2 = DateTime.Today;

                if (d2 > d1)
                {
                    TimeSpan d3 = d2 - d1;
                    ViewBag.dgr = Math.Floor(d3.TotalDays);
                }
                else
                {
                    ViewBag.dgr = 0;
                }
            }

            return View("OduncIade", odn);
        }


        [HttpPost]
        public ActionResult OduncIade(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);


            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;

            var kitap = db.TBLKITAP.Find(hrk.KITAP);
            if (kitap != null)
            {
                kitap.DURUM = true;
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;


            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}