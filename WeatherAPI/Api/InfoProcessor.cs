using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WeatherAPI
{
    public static class InfoProcessor
    {
        public static string CityUrl { get; set; } 
        
        public static async Task<MainModel> LoadMainResults()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(CityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultsModel results = await response.Content.ReadAsAsync<ResultsModel>();
                    return results.Main;
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
        
        public static async Task<WeatherModel> LoadWeatherResults()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(CityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultsModel results = await response.Content.ReadAsAsync<ResultsModel>();
                    return results.Weather[0];
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
        
        public static async Task<WindModel> LoadWindResults()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(CityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultsModel results = await response.Content.ReadAsAsync<ResultsModel>();
                    return results.Wind;
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
        
        public static async Task<CloudsModel> LoadCloudsResults()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(CityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    ResultsModel results = await response.Content.ReadAsAsync<ResultsModel>();
                    return results.Clouds;
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
        
        public static async Task<CityNameModel> LoadCityNameResults()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(CityUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    CityNameModel result = await response.Content.ReadAsAsync<CityNameModel>();
                    return result;
                }
                
                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}