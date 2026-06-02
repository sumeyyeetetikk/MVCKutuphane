using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Page404()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page403()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Page400()
        {
            Response.StatusCode = 400;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}