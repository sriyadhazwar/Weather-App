using System;
using WeatherAPI;

namespace Display
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
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
        
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            LoadInfo();
        }

        private void DisplayData()
        {
        }

        private async void LoadInfo()
        {
            var mainInfo = await InfoProcessor.LoadMainResults();
            temperature = ToFahrenheit(mainInfo.Temp);
            minTemp = ToFahrenheit(mainInfo.Min_Temp);
            maxtemp = ToFahrenheit(mainInfo.Max_Temp);
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
    }
}