using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavor.Models
{
    /// <summary>
    /// Represents a comprehensive weather model for a specific location.
    /// 
    /// Fields:
    /// - LocationName: The name of the location for which the weather details are provided.
    /// - CurrentCondition: Describes the current weather condition (e.g., Rainy, Sunny).
    /// - CurrentTemperature: The current temperature for the location.
    /// - Humidity: The current humidity percentage.
    /// - WindSpeed: The speed of the wind.
    /// - ChanceOfPrecipitation: Percentage chance that precipitation will occur.
    /// - Recommendation: Provides user-friendly advice based on the weather conditions.
    /// - HourlyForecasts: List of hourly weather forecasts.
    /// - ThreeDayForecasts: List of daily weather forecasts for the next three days.
    /// </summary>
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
    /// <summary>
    /// Represents the weather forecast for a specific hour.
    /// </summary>
    public class HourlyForecast
    {
        public DateTime Time { get; set; }
        public int Temperature { get; set; }
        public string Condition { get; set; }
    }

    /// <summary>
    /// Represents the weather forecast for a specific day.
    /// 
    /// Fields:
    /// - Date: The date of the forecast.
    /// - Condition: Describes the weather condition for the day.
    /// - HighTemperature: The highest expected temperature for the day.
    /// - LowTemperature: The lowest expected temperature for the day.
    /// - ChanceOfPrecipitation: Percentage chance that precipitation will occur.
    /// - Recommendation: Provides user-friendly advice based on the weather forecast.
    /// </summary>
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