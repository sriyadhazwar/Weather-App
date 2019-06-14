using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WeatherAPI
{
    public class InfoProcessor
    {
        public static async Task<MainModel> LoadMainResults()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Cleveland,us" +
                         "&APPID=18e0836ea2ae7d4ee1a6d43dfb38e393";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    MainModelResults results = await response.Content.ReadAsAsync<MainModelResults>();
                    return results.Main;
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
        
        public static async Task<WeatherModel> LoadWeatherResults()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Cleveland,us" +
                         "&APPID=18e0836ea2ae7d4ee1a6d43dfb38e393";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    WeatherResultsModel results = await response.Content.ReadAsAsync<WeatherResultsModel>();
                    return results.Weather[0];
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}