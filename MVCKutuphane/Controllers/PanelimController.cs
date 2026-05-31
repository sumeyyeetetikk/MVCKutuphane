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

        [Authorize]
        public ActionResult Index()
        {
            var uyeMail = User.Identity.Name;

            var uyeBilgisi = db.TBLUYELER.FirstOrDefault(x => x.MAIL == uyeMail);

            return View(uyeBilgisi);
        }
    }
}