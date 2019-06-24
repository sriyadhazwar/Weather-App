using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json;
using WeatherAPI;

namespace Display
{
    public partial class DisplayPage : Page
    {
        private string cityUrl = "";
        private string prevurl = "";
        private List<CityInfo> cities;
        private List<string> names;
        
        private int temperature;
        private int minTemp;
        private int maxtemp;
        private double pressure;
        private double humidity;

        private string weatherType;
        private string description;

        private int windSpeed;
        private int windDegrees;
        private double gust;

        private int cloudPercentage;

        private string cityName;
        
        public DisplayPage()
        {
            InitializeComponent();
            GetCities();
            ApiHelper.InitializeClient();
        }
        
        private void SetCity(string name)
        {
            double longitude;
            double latitude;
            
            for (int i = 0; i < cities.Count; i++)
            {
                if (cities[i].City.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    longitude = cities[i].Longitude;
                    latitude = cities[i].Latitude;
                    cityUrl = $"http://api.openweathermap.org/data/2.5/weather?lat={latitude}&lon={longitude}&appid=" + ApiKey.ApiKeyCode;
                    return;
                }
            }
        }

        private void DisplayData()
        {
            Block.Text += description + "\n";
            Block.Text += temperature + "F\n";
            Block.Text += maxtemp + " max\n";
            Block.Text += minTemp + " min\n";
            Block.Text += humidity + " humidity\n";
            Block.Text += pressure + " pressure\n";
            Block.Text += weatherType + " weather type\n";
            Block.Text += windSpeed + " wind speed\n";
            Block.Text += windDegrees + " wind degree\n";
            Block.Text += gust + " gust\n";
        }

        private async void LoadInfo()
        {
            var mainInfo = await InfoProcessor.LoadMainResults();
            temperature = ToFahrenheit(mainInfo.Temp);
            minTemp = ToFahrenheit(mainInfo.Temp_Min);
            maxtemp = ToFahrenheit(mainInfo.Temp_Max);
            pressure = mainInfo.Pressure;
            humidity = mainInfo.Humidity;

            var weatherInfo = await InfoProcessor.LoadWeatherResults();
            weatherType = weatherInfo.Main;
            description = weatherInfo.Description;

            var windInfo = await InfoProcessor.LoadWindResults();
            windSpeed = (int) Math.Round(windInfo.Speed);
            windDegrees = (int) Math.Round(windInfo.Deg);
            gust = windInfo.Gust;

            var cloudsInfo = await InfoProcessor.LoadCloudsResults();
            cloudPercentage = cloudsInfo.All;

            var cityNameInfo = await InfoProcessor.LoadCityNameResults();
            cityName = cityNameInfo.Name;
            
            DisplayData();
        }
        
        private int ToFahrenheit(double kelvin)
        {
            kelvin = kelvin - 273;
            kelvin = kelvin * 9 / 5;
            kelvin = kelvin + 32;
            return (int) Math.Round(kelvin);
        }
        
        private void GetWeather_OnClick(object sender, RoutedEventArgs e)
        {
            SetCity(InputText.Text);
            if (cityUrl.Equals(prevurl)) return;
            if (cityUrl.Equals("")) return;
            InfoProcessor.CityUrl = cityUrl;
            prevurl = cityUrl;
            Block.Text = "";
            LoadInfo();
        }

        private void GetCities()
        {
            cities = new List<CityInfo>();
            cities.Sort();
            string jsonString = File.ReadAllText("cities.json");
            cities = JsonConvert.DeserializeObject<List<CityInfo>>(jsonString);
            names = new List<string>();
                    
            foreach (var VARIABLE in cities)
            {
                names.Add(VARIABLE.City);
            }
            names.Sort();
            foreach (var VARIABLE in names)
            {
                if (!InputText.Items.Contains(VARIABLE))
                {
                    InputText.Items.Add(VARIABLE);
                }
            }
        }
    }
}