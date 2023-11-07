using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ForecastFavorApp.Models
{
    // The main class representing weather data that includes location, current weather, and forecast information.
    public class weatherData
    {
        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("current")]
        public Current Current { get; set; }

        [JsonPropertyName("forecast")]
        public Forecast Forecast { get; set; }
    }

    // Class representing the forecast, which includes a list of forecast days.
    public class Forecast
    {
        [JsonPropertyName("forecastday")]
        public List<ForecastDay> ForecastDay { get; set; }
    }

    // Represents the weather forecast for a single day.
    public class ForecastDay
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("date_epoch")]
        public long DateEpoch { get; set; }

        [JsonPropertyName("day")]
        public ForecastDayDetail Day { get; set; }

        [JsonPropertyName("astro")]
        public Astronomy Astro { get; set; }

        [JsonPropertyName("hour")]
        public List<ForecastHour> Hour { get; set; }
    }

    //Daily forecast data for a specific day
    public class ForecastDayDetail
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxTempC { get; set; }

        [JsonPropertyName("maxtemp_f")]
        public double MaxTempF { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double MinTempC { get; set; }

        [JsonPropertyName("mintemp_f")]
        public double MinTempF { get; set; }

        [JsonPropertyName("avgtemp_c")]
        public double AvgTempC { get; set; }

        [JsonPropertyName("avgtemp_f")]
        public double AvgTempF { get; set; }

        [JsonPropertyName("maxwind_mph")]
        public double MaxWindMph { get; set; }

        [JsonPropertyName("maxwind_kph")]
        public double MaxWindKph { get; set; }

        [JsonPropertyName("totalprecip_mm")]
        public double TotalPrecipMm { get; set; }

        [JsonPropertyName("totalprecip_in")]
        public double TotalPrecipIn { get; set; }

        [JsonPropertyName("avgvis_km")]
        public double AvgVisKm { get; set; }

        [JsonPropertyName("avgvis_miles")]
        public double AvgVisMiles { get; set; }

        [JsonPropertyName("avghumidity")]
        public double AvgHumidity { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

        [JsonPropertyName("uv")]
        public double UV { get; set; }

      
    }

    //Astronomical data for a specific day
    public class Astronomy
    {
        [JsonPropertyName("sunrise")]
        public string Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public string Sunset { get; set; }

        
    }
    // Represents the weather forecast for a specific hour.
    public class ForecastHour
    {
        [JsonPropertyName("time_epoch")]
        public long TimeEpoch { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        // ... properties similar to the `Current` class for the hourly forecast
        [JsonPropertyName("temp_c")]
        public double TemperatureCelsius { get; set; }

        // ... other relevant properties
    }


    // Location data including the name of the location, region, country, latitude, longitude, timezone ID, local time in epoch and readable format.
    public class Location
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region")]
        public string Region { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("tz_id")]
        public string TimeZoneId { get; set; }

        [JsonPropertyName("localtime_epoch")]
        public long LocalTimeEpoch { get; set; }

        [JsonPropertyName("localtime")]
        public string LocalTime { get; set; }
    }
    // Current weather condition data including the last updated time, temperature, whether it is day or night, weather condition, wind details, pressure, precipitation, humidity, cloud cover, 'feels like' temperature, visibility, UV index, gust speed, and air quality.
    public class Current
    {
        [JsonPropertyName("last_updated_epoch")]
        public long LastUpdatedEpoch { get; set; }

        [JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }

        [JsonPropertyName("temp_c")]
        public double TemperatureCelsius { get; set; }

        [JsonPropertyName("temp_f")]
        public double TemperatureFahrenheit { get; set; }

        [JsonPropertyName("is_day")]
        public int IsDay { get; set; }

        [JsonPropertyName("condition")]
        public Condition Condition { get; set; }

        [JsonPropertyName("wind_mph")]
        public double WindMph { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindKph { get; set; }

        [JsonPropertyName("wind_degree")]
        public int WindDegree { get; set; }

        [JsonPropertyName("wind_dir")]
        public string WindDirection { get; set; }

        [JsonPropertyName("pressure_mb")]
        public double PressureMb { get; set; }

        [JsonPropertyName("pressure_in")]
        public double PressureIn { get; set; }

        [JsonPropertyName("precip_mm")]
        public double PrecipitationMm { get; set; }

        [JsonPropertyName("precip_in")]
        public double PrecipitationIn { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }

        [JsonPropertyName("cloud")]
        public int Cloud { get; set; }

        [JsonPropertyName("feelslike_c")]
        public double FeelsLikeCelsius { get; set; }

        [JsonPropertyName("feelslike_f")]
        public double FeelsLikeFahrenheit { get; set; }

        [JsonPropertyName("vis_km")]
        public double VisibilityKm { get; set; }

        [JsonPropertyName("vis_miles")]
        public double VisibilityMiles { get; set; }

        [JsonPropertyName("uv")]
        public double UV { get; set; }

        [JsonPropertyName("gust_mph")]
        public double GustMph { get; set; }

        [JsonPropertyName("gust_kph")]
        public double GustKph { get; set; }

        [JsonPropertyName("air_quality")]
        public AirQuality AirQuality { get; set; }
    }
    // Class representing the weather condition with a textual description, icon URL, and a condition code.
    public class Condition
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }
    }

    // Air quality data including measurements of CO, NO2, O3, SO2, particulate matter, and indexes from US EPA and GB DEFRA.
    public class AirQuality
    {
        [JsonPropertyName("co")]
        public double CO { get; set; }

        [JsonPropertyName("no2")]
        public double NO2 { get; set; }

        [JsonPropertyName("o3")]
        public double O3 { get; set; }

        [JsonPropertyName("so2")]
        public double SO2 { get; set; }

        [JsonPropertyName("pm2_5")]
        public double PM25 { get; set; }

        [JsonPropertyName("pm10")]
        public double PM10 { get; set; }

        [JsonPropertyName("us-epa-index")]
        public int USEPAIndex { get; set; }

        [JsonPropertyName("gb-defra-index")]
        public int GBDEFRAIndex { get; set; }
    }

}
