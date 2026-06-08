using MVCKutuphane.Models.Entity;
using MVCKutuphane.Models.Sınıflarım;
using PagedList;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        DBKUTUPHANEEntities db = new DBKUTUPHANEEntities();

        [HttpGet]
       
        public ActionResult Index(int? sayfa, int? kategoriId)
        {
            VitrinViewModel model = new VitrinViewModel();

            var kitaplar = db.TBLKITAP.AsQueryable();

            if (kategoriId != null)
            {
                kitaplar = kitaplar.Where(x => x.KATEGORI == kategoriId);
            }

            model.Kitaplar = kitaplar
                .OrderBy(x => x.ID)
                .ToPagedList(sayfa ?? 1, 9);

            model.Hakkimizda = db.TBLHAKKIMIZDA.ToList();

            ViewBag.Kategoriler = db.TBLKATEGORI.ToList();
            ViewBag.SeciliKategori = kategoriId;

            return View(model);
        }
        [HttpPost]
        public ActionResult Index(TBLILETISIM t)
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> OneriGetir(string ruhHali)
        {
            if (string.IsNullOrEmpty(ruhHali))
            {
                return Json(new { success = false, message = "Lütfen modunuzu belirtin." });
            }

            var kitaplar = db.TBLKITAP.Select(k => new
            {
                KitapAdi = k.AD,
                Yazar = k.TBLYAZAR.AD + " " + k.TBLYAZAR.SOYAD,
                YazarDetay = k.TBLYAZAR.Detay,
                Kategori = k.TBLKATEGORI.AD
            }).ToList();

            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string kitaplarJson = jsonSerializer.Serialize(kitaplar);

            AIService aiService = new AIService();
            string aiCevabi = await aiService.GetBookRecommendationAsync(ruhHali, kitaplarJson);

            return Json(new { success = true, cevap = aiCevabi });
        }
    }
}