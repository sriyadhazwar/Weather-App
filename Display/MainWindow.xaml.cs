using System;
using System.Windows;
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
        
        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();
            LoadInfo();
        }

        private void DisplayData()
        {
            MessageBox.Show(description);
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