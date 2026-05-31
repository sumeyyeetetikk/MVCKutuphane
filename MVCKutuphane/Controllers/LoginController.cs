using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCKutuphane.Models.Entity;
using System.Web.Security;

namespace MVCKutuphane.Controllers
{
    public class LoginController : Controller
    {
       DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();
        public ActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GirisYap(TBLUYELER p)
        {
            var bilgiler= db.TBLUYELER.FirstOrDefault(x => x.MAIL == p.MAIL && x.SIFRE == p.SIFRE);
            if(bilgiler !=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MAIL, false);
                Session["Ad"] = bilgiler.AD.ToString();
                Session["Soyad"] = bilgiler.SOYAD.ToString();
                Session["KullanıcıAdı"] = bilgiler.KULLANICIADI.ToString();
                Session["Sıfre"] = bilgiler.SIFRE.ToString();
                Session["Okul"] = bilgiler.OKUL.ToString();
                Session["MAIL"] = bilgiler.MAIL.ToString();
                return RedirectToAction("Index", "Panelim");
            }
            else 
            {
                return View();
            }
               
        }
    }
}