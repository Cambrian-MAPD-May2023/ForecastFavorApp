using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using ForecastFavorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ForecastFavorApp.ViewModels
{
    // The WeatherViewModel class, which inherits from ObservableObject, allows the UI to be notified of property changes.
    internal partial class WeatherViewModel : ObservableObject
    {
        private readonly WeatherService _weatherService; // Service for fetching weather data.

        // Constructor initializes a new instance of the WeatherService.
        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
        }

        // Observable properties that the UI can bind to. They will notify the UI when their values change.
        [ObservableProperty]
        private string locationInput; // Input for the location to get weather information for.

        [ObservableProperty]
        private string weatherIcon; // URL for the current weather condition icon.

        [ObservableProperty]
        private string temperature; // Current temperature.

        [ObservableProperty]
        private string weatherDescription; // Textual description of the current weather.

        [ObservableProperty]
        private string locationOutput; // Formatted string representing the location information.

        [ObservableProperty]
        private string humidity; // Current humidity percentage.

        [ObservableProperty]
        private string cloudCoverLevel; // Current cloud cover percentage.

        [ObservableProperty]
        private string isDay; // String representing whether it is currently day or night.

        [ObservableProperty]
        private string tomorrowTemperature; // Forecasted temperature for tomorrow.

        [ObservableProperty]
        private string tomorrowWeatherIcon; // URL for tomorrow's weather condition icon.

        [ObservableProperty]
        private string tomorrowWeatherDescription; // Textual description of tomorrow's weather.

        [ObservableProperty]
        private string tomorrowHumidity; // Forecasted humidity for tomorrow.

        [ObservableProperty]
        private Forecast forecast; // Complete forecast object.

        [ObservableProperty]
        private ForecastDayDetail tomorrowForecast; // Detailed forecast for tomorrow.

        [ObservableProperty]
        private ObservableCollection<ForecastDayDetail> threeDayForecastDetails; // Collection of detailed forecasts for the next three days.

        // Relay Command attribute that defines an asynchronous method for fetching weather information.
        [RelayCommand]
        public async Task FetchWeatherInformation()
        {
            // Calls the weather service to get weather information for the next three days.
            var weatherData = await _weatherService.GetWeatherInformation(LocationInput, 3);
            if (weatherData?.Forecast?.ForecastDay != null && weatherData.Forecast.ForecastDay.Count >= 2)
            {
                // If the data is successfully retrieved, assigns values to the ViewModel properties.
                // Current weather data assignments
                WeatherIcon = "http:" + weatherData.Current.Condition.Icon;
                Temperature = $"{weatherData.Current.TemperatureCelsius}°C / {weatherData.Current.TemperatureFahrenheit}°F";
                WeatherDescription = weatherData.Current.Condition.Text;
                LocationOutput = $"{weatherData.Location.Name}, {weatherData.Location.Region}, {weatherData.Location.Country}";
                Humidity = $"{weatherData.Current.Humidity}%";
                CloudCoverLevel = $"{weatherData.Current.Cloud}%";
                IsDay = weatherData.Current.IsDay == 1 ? "Day" : "Night";

                // Tomorrow's weather forecast assignments
                TomorrowForecast = weatherData.Forecast.ForecastDay[1].Day;
                TomorrowTemperature = $"{TomorrowForecast.MaxTempC}°C / {TomorrowForecast.MaxTempF}°F";
                TomorrowWeatherIcon = "http:" + TomorrowForecast.Condition.Icon;
                TomorrowWeatherDescription = TomorrowForecast.Condition.Text;
                TomorrowHumidity = $"{TomorrowForecast.AvgHumidity}%";

                // Assigns the 3-day weather forecast details.
                ThreeDayForecastDetails = new ObservableCollection<ForecastDayDetail>(
                    weatherData.Forecast.ForecastDay.Take(3).Select(fd => fd.Day));
            }
        }
    }
}
