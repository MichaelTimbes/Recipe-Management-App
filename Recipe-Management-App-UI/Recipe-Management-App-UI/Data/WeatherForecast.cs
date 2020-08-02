using System;
using System.Text.Json.Serialization;

namespace Recipe_Management_App_UI.Data
{
    public class WeatherForecast
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("temperatureC")]
        public int TemperatureC { get; set; }

        [JsonPropertyName("temperatureF")]
        public int? TemperatureF { get; set; }

        public int GetTemperatureF()
        {
            if(TemperatureF == null)
            return 32 + (int)(TemperatureC / 0.5556);

            return TemperatureF.GetValueOrDefault(0);
        }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }
    }
}
