using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavor.Models
{
    public class WeatherModel
    {

        public string LocationName { get; set; }


        public string CurrentCondition { get; set; }
        public int CurrentTemperature { get; set; }
        public int Humidity { get; set; }
        public string WindSpeed { get; set; }
        public int ChanceOfPrecipitation { get; set; }
        public string Recommendation { get; set; }


        public List<HourlyForecast> HourlyForecasts { get; set; }


        public List<DailyForecast> ThreeDayForecasts { get; set; }
    }

    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public int Temperature { get; set; }
        public string Condition { get; set; }
    }

    public class DailyForecast
    {
        public DateTime Date { get; set; }
        public string Condition { get; set; }
        public int HighTemperature { get; set; }
        public int LowTemperature { get; set; }
        public int ChanceOfPrecipitation { get; set; }
        public string Recommendation { get; set; }
    }
}