namespace WeatherAPI
{
    public class ResultsModel
    {
        public CloudsModel Clouds { get; set; }
        public MainModel Main { get; set; }
        public WeatherModel[] Weather { get; set; }
        public WindModel Wind { get; set; }
    }
}