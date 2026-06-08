using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace MVCKutuphane.Controllers
{
    public class AIService
    {
        private readonly string _apiKey = "Your_ApiKey"; // Buraya API anahtarını koyacaksın

        // Bu satırı şu şekilde değiştirip dene:
        private readonly string _apiUrl ="https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key=";

        public async Task<string> GetBookRecommendationAsync(string userPrompt, string bookListJson)
        {
            using (var client = new HttpClient())
            {
                string systemPrompt = $@"
Sen akıllı bir kütüphane asistanısın.
Kullanıcının ruh haline göre SADECE aşağıdaki kitap listesinden öneri yap.
Listede olmayan kitap önerme.

Kitap listesi:
{bookListJson}

Cevabında:
- 2 veya 3 kitap öner
- Kitap adını yaz
- Yazarını yaz
- Neden bu ruh haline uygun olduğunu samimi şekilde açıkla
- Türkçe cevap ver
";

                var requestBody = new
                {
                    contents = new[]
                    {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = systemPrompt + "\n\nKullanıcının mesajı: " + userPrompt
                        }
                    }
                }
            }
                };

                var serializer = new JavaScriptSerializer();
                string jsonRequest = serializer.Serialize(requestBody);

                var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(_apiUrl + _apiKey, content);
                    string responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic result = serializer.Deserialize<dynamic>(responseString);
                        return result["candidates"][0]["content"]["parts"][0]["text"];
                    }

                    return "API Hatası: " + responseString;
                }
                catch (Exception ex)
                {
                    return "Sistem Hatası: " + ex.Message;
                }
            }
        }
    }
}