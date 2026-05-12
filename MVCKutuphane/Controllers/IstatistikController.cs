using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            
            ViewBag.KasadakiTutar = db.TBLKASA.Sum(x => x.TUTAR);

            ViewBag.ToplamUye = db.TBLUYELER.Count();

            ViewBag.KitapSayisi = db.TBLKITAP.Count();

            ViewBag.EmanetKitapSayisi = db.TBLHAREKET
                .Where(x => x.ISLEMDURUM == false)
                .Count();

            
            ViewBag.KategoriSayisi = db.TBLKATEGORI.Count();

            ViewBag.YazarSayisi = db.TBLYAZAR.Count();

            ViewBag.PersonelSayisi = db.TBLPERSONEL.Count();

            ViewBag.EnCokOkunanKitap = db.TBLHAREKET
                .GroupBy(x => x.TBLKITAP.AD)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault();

            ViewBag.EnAktifUye = db.TBLHAREKET
                .GroupBy(x => x.TBLUYELER.AD + " " + x.TBLUYELER.SOYAD)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault();

            ViewBag.EnCokKitabiOlanYazar = db.TBLKITAP
                .GroupBy(x => x.TBLYAZAR.AD + " " + x.TBLYAZAR.SOYAD)
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault();

            return View();
        }
        public ActionResult HavaRaporu() //RapidApi ile konuma göre hava durumu çek

        {
            return View();
        }
        public ActionResult Galeri()
        {
            return View();
        }
    }
}