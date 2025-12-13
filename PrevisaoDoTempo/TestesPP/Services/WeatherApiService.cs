using System.Globalization;
using System.Net.Http.Json;
using TestesPP.Models;

namespace TestesPP.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _http = new HttpClient();

      
        private readonly string apiKey = "385428c4033949f883703305251312";

        public async Task<List<Clima>> GetForecast(string city)
        {
            var lista = new List<Clima>();

            try
            {
                var url =
                    $"https://api.weatherapi.com/v1/forecast.json" +
                    $"?key={apiKey}&q={city}&days=4&lang=pt";

                var result = await _http.GetFromJsonAsync<WeatherApiResponse>(url);

                
                if (result?.forecast?.forecastday == null)
                    return lista;

                int index = 0;

                foreach (var day in result.forecast.forecastday)
                {
                   
                    DateTime data = DateTime.Now.AddDays(index);

                    lista.Add(new Clima
                    {
                        Condicao = day.day.condition.text,
                        TempMax = day.day.maxtemp_c,
                        TempMin = day.day.mintemp_c,
                        Icon = "https:" + day.day.condition.icon,

                        
                        DiaSemana = data.ToString("dddd", new CultureInfo("pt-BR"))
                    });

                    index++;
                }
            }
            catch
            {
                
            }

            return lista;
        }
    }
}
