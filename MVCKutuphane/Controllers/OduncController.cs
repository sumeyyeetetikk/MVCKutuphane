using MVCKutuphane.Models.Entity;
using System.Linq;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x=> x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
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
            return View("OduncIade", odn);
        }

        [HttpPost]
        public ActionResult OduncIade(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);

            hrk.UYEGETIRTARIH = p.UYEGETIRTARIH;
            hrk.ISLEMDURUM = true;

            var kitap = db.TBLKITAP.Find(hrk.KITAP);
            kitap.DURUM = true;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}