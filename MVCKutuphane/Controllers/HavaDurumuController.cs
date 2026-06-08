using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MVCKutuphane.Controllers
{
    public class HavaDurumuController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index(string sehir)
        {
            // 1. Türkiye'nin 81 İlini Alfabetik Olarak Diziye Alıyoruz
            var turkiyeSehirleri = new List<string>
    {
        "Adana", "Adiyaman", "Afyonkarahisar", "Agri", "Aksaray", "Amasya", "Ankara", "Antalya", "Ardahan", "Artvin",
        "Aydin", "Balikesir", "Bartin", "Batman", "Bayburt", "Bilecik", "Bingol", "Bitlis", "Bolu", "Burdur",
        "Bursa", "Canakkale", "Cankiri", "Corum", "Denizli", "Diyarbakir", "Duzce", "Edirne", "Elazig", "Erzincan",
        "Erzurum", "Eskisehir", "Gaziantep", "Giresun", "Gumushane", "Hakkari", "Hatay", "Igdir", "Isparta", "Istanbul",
        "Izmir", "Kahramanmaras", "Karabuk", "Karaman", "Kars", "Kastamonu", "Kayseri", "Kilis", "Kirikkale", "Kirklareli",
        "Kirsehir", "Kocaeli", "Konya", "Kutahya", "Malatya", "Manisa", "Mardin", "Mersin", "Mugla", "Mus",
        "Nevsehir", "Nigde", "Ordu", "Osmaniye", "Rize", "Sakarya", "Samsun", "Sanliurfa", "Siirt", "Sinop",
        "Sivas", "Sirnak", "Tekirdag", "Tokat", "Trabzon", "Tunceli", "Usak", "Van", "Yalova", "Yozgat", "Zonguldak"
    };

            // View tarafında dönebilmek için ViewBag'e listeyi veriyoruz
            ViewBag.SehirlerListesi = turkiyeSehirleri;

            // Varsayılan şehir kontrolü
            if (string.IsNullOrEmpty(sehir))
            {
                sehir = "Istanbul";
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://weather-api167.p.rapidapi.com/api/weather/forecast?place={sehir.ToLower()}&cnt=3&units=standard&type=three_hour&mode=json&lang=en"),
                Headers =
        {
            { "x-rapidapi-host", "weather-api167.p.rapidapi.com" },
            { "x-rapidapi-key", "6928a9569dmsh0f5351a0ba61f4ep1ba581jsnbe16e836a03a" }, 
            { "Accept", "application/json" },
        },
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(body);

                    ViewBag.SecilenSehir = sehir;
                    ViewBag.SehirAdi = data["city"]?["name"]?.ToString();

                    var kelvinTemp = Convert.ToDouble(data["list"]?[0]?["main"]?["temprature"]);
                    var celsiusTemp = kelvinTemp - 273.15;
                    ViewBag.Derece = Math.Round(celsiusTemp).ToString();

                    ViewBag.Durum = data["list"]?[0]?["weather"]?[0]?["description"]?.ToString();
                }
            }
            catch (Exception)
            {
                ViewBag.SecilenSehir = sehir;
                ViewBag.SehirAdi = sehir + " (Hata)";
                ViewBag.Derece = "--";
                ViewBag.Durum = "Hava durumu verisi alınamadı.";
            }

            return View();
        }
    }
}


