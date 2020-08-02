using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Recipe_Management_App_UI.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private const string GetForcastAPI = "https://localhost:44377/weatherforecast";

        private readonly IHttpClientFactory _clientFactory;
        public WeatherForecastService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        {
            return MakeRequestForWeather();
        }
        private async Task<WeatherForecast[]> MakeRequestForWeather()
        {
            try
            {
                var client = _clientFactory.CreateClient();
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                var response = await client.GetAsync(GetForcastAPI);

                if (response.IsSuccessStatusCode)
                {
                    string contentAsStr = response.Content.ReadAsStringAsync().Result;
                  return
                        JsonSerializer.Deserialize<WeatherForecast[]>(contentAsStr);
                }
                else
                {
                    return await DefaultForcast();
                }
            }
            catch(Exception e)
            {
                return await DefaultForcast();
            }
        }

        private Task<WeatherForecast[]> DefaultForcast()
        {
            var rng = new Random();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Today.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
