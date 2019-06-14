using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public class InfoProcessor
    {
        public static async Task<MainModel> LoadResults()
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
    }
}