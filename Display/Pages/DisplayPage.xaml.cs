using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        private double humidity;
        private string weatherType;
        private string description;
        private int windSpeed;
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
            CityNameLabel.Text = "City";
            DescriptionLabel.Text = "Description";
            TempLabel.Text = "Temperature";
            MinMaxLabel.Text = "Min/Max";
            CloudPercentLabel.Text = "Clouds";
            HumidityLabel.Text = "Humidity";
            WindSpeedLabel.Text = "Wind";
            
            CityNameTextBlock.Text = cityName;
            DescriptionTextBlock.Text = description;
            TemperatureTextBlock.Text = temperature + "°F";
            MaxMinTextBlock.Text = $"{minTemp}/{maxtemp}°F";
            CloudPercentTextBlock.Text = $"{cloudPercentage}%";
            HumidityTextBlock.Text = $"{humidity}%";
            WindSpeedTextBlock.Text = $"{windSpeed}mph";

            CityNameLabel.Width = 275;
            DescriptionLabel.Width = 275;
            TempLabel.Width = 275;
            MinMaxLabel.Width = 275;
            HumidityLabel.Width = 275;
            CloudPercentLabel.Width = 275;
            WindSpeedLabel.Width = 275;
            
            CityNameTextBlock.Background = new SolidColorBrush(Color.FromRgb(174,231,232));
            DescriptionTextBlock.Background = new SolidColorBrush(Color.FromRgb(40,195,212));
            TemperatureTextBlock.Background = new SolidColorBrush(Color.FromRgb(174,231,232));
            MaxMinTextBlock.Background = new SolidColorBrush(Color.FromRgb(40,195,212));
            CloudPercentTextBlock.Background = new SolidColorBrush(Color.FromRgb(174,231,232));
            HumidityTextBlock.Background = new SolidColorBrush(Color.FromRgb(40,195,212));
            WindSpeedTextBlock.Background = new SolidColorBrush(Color.FromRgb(174,231,232));
            
            SetWeatherImage(weatherType);
        }

        private void SetWeatherImage(string weatherType)
        {
            switch (weatherType)
            {
                case "Clear":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/clear_sky.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Clouds":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/cloudy.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Few clouds":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/few_clouds.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Scattered clouds":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/cloudy.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Broken clouds":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/few_clouds.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Shower rain":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/shower_rain.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Rain":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/rain.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Thunderstorm":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/thunderstorm.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Snow":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/snow.png", UriKind.RelativeOrAbsolute));
                    break;
                case "Mist":
                    WeatherImage.Source = new BitmapImage(new Uri("/bin/Debug/Images/mist.png", UriKind.RelativeOrAbsolute));
                    break;
            }
        }
        

        private async void LoadInfo()
        {
            var mainInfo = await InfoProcessor.LoadMainResults();
            temperature = ToFahrenheit(mainInfo.Temp);
            minTemp = ToFahrenheit(mainInfo.Temp_Min);
            maxtemp = ToFahrenheit(mainInfo.Temp_Max);
            humidity = mainInfo.Humidity;

            var weatherInfo = await InfoProcessor.LoadWeatherResults();
            weatherType = weatherInfo.Main;
            description = weatherInfo.Description;

            var windInfo = await InfoProcessor.LoadWindResults();
            windSpeed = (int) Math.Round(windInfo.Speed);

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
            CityNameTextBlock.Text = "";
            DescriptionTextBlock.Text = "";
            TemperatureTextBlock.Text = "";
            MaxMinTextBlock.Text = "";
            WindSpeedTextBlock.Text = "";
            HumidityTextBlock.Text = "";
            CityNameLabel.Text = "";
            DescriptionLabel.Text = "";
            TempLabel.Text = "";
            MinMaxLabel.Text = "";
            HumidityLabel.Text = "";
            WindSpeedLabel.Text = "";
            CloudPercentTextBlock.Text = "";
            CloudPercentLabel.Text = "";
            WeatherImage.Source = null;
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

        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GetWeather_OnClick(sender, e);
            }
        }
    }
}