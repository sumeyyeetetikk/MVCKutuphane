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
            // İlk açılışta boş kalmasın, varsayılan olarak İstanbul gelsin
            if (string.IsNullOrEmpty(sehir))
            {
                sehir = "Istanbul";
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                // Görseldeki parametrelere sadık kalarak URL'i dinamik yaptık
                RequestUri = new Uri($"https://weather-api167.p.rapidapi.com/api/weather/forecast?place={sehir.ToLower()}&cnt=3&units=standard&type=three_hour&mode=json&lang=en"),
                Headers =
                {
                    { "x-rapidapi-host", "weather-api167.p.rapidapi.com" },
                    { "x-rapidapi-key", "6928a9569dmsh0f5351a0ba61f4ep1ba581jsnbe16e836a03a" }, // Kod panelinden veya networkten aldığın key
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

                    // Seçilen şehri dropdown'da korumak için ViewBag'e atıyoruz
                    ViewBag.SecilenSehir = sehir;
                    ViewBag.SehirAdi = data["city"]?["name"]?.ToString();

                    // Görseldeki JSON ağacına göre: list -> 0. eleman -> main -> temprature
                    var kelvinTemp = Convert.ToDouble(data["list"]?[0]?["main"]?["temprature"]);

                    // Kelvin'den Santigrat'a çevrim yapıyoruz ve yuvarlıyoruz
                    var celsiusTemp = kelvinTemp - 273.15;
                    ViewBag.Derece = Math.Round(celsiusTemp).ToString();

                    // Görseldeki JSON ağacına göre durum açıklaması: list -> 0. eleman -> weather -> 0. eleman -> description
                    ViewBag.Durum = data["list"]?[0]?["weather"]?[0]?["description"]?.ToString();
                }
            }
            catch (Exception ex)
            {
                ViewBag.SecilenSehir = sehir;
                ViewBag.SehirAdi = sehir + " (Bağlantı Hatası)";
                ViewBag.Derece = "--";
                ViewBag.Durum = "Hava durumu verisi çözümlenemedi.";
            }

            return View();
        }
    }
}
