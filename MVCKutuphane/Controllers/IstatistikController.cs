using MVCKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
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

            ViewBag.KasadakiTutar = db.TBLCEZALAR.Sum(x => x.PARA);

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

        [HttpPost]
        public ActionResult resimyukle(HttpPostedFileBase dosya)
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler/"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }
        public ActionResult LinqKart()
        {
            var deger1= db.TBLKITAP.Count();
            var deger2= db.TBLUYELER.Count();
            var deger3= db.TBLCEZALAR.Sum(x=>x.PARA);
            var deger4= db.TBLKITAP.Where(x=>x.DURUM == false).Count();
            var deger5= db.EnFazlaKitapYazar().FirstOrDefault();
            var deger6= db.TBLKITAP.GroupBy(x=>x.YAYIMEVI).OrderByDescending(z=>z.Count()).Select(y=> new {y.Key}).FirstOrDefault();
            var deger7 = db.TBLHAREKET.GroupBy(x => x.TBLPERSONEL.PERSONEL).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            DateTime bugun = DateTime.Today;
            var deger8 = db.TBLHAREKET.Where(x => System.Data.Entity.DbFunctions.TruncateTime(x.ALISTARIH) == bugun).Count();
            var deger9 = db.TBLHAREKET.GroupBy(x => x.TBLKITAP.AD).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();
            var deger10 = db.TBLHAREKET.GroupBy(x => x.TBLUYELER.AD + " " + x.TBLUYELER.SOYAD).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();                                   
            var deger11 = db.TBLYAZAR.Count();
           

            ViewBag.dgr8 = deger8;
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            ViewBag.dgr4 = deger4;
            ViewBag.dgr5 = deger5;
            ViewBag.dgr6 = deger6;
            ViewBag.dgr7 = deger7;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 = deger9;
            ViewBag.dgr10 = deger10;
            ViewBag.dgr11 = deger11;
            return View();
        }
    }
}