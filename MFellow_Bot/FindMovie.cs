using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFellow_Bot
{
    public class FindMovie
    {
        //This method is getting info about movies. String message is an actual
        //user message that we can use for a specific call. Dependency injection forever=)
        public static async Task<D> MovieInform(string message)
        {
            var movie = new D();

            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://imdb8.p.rapidapi.com/auto-complete?q=" + message),
                Headers =
             {
                { "X-RapidAPI-Key", "d88e8c27fcmshb8e6dc3fb83c531p1e0167jsn1b7b135aa64a" },
                { "X-RapidAPI-Host", "imdb8.p.rapidapi.com" },
             },
            };

            //Converting JSON into root object and initializing a movie class.
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var root = JsonConvert.DeserializeObject<Root>(body);
                movie = root.d.FirstOrDefault();
            }

            return movie;
        }
    }
}
