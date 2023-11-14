using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using ForecastFavorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

// Did code reveiew - Sreenath

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
        private string secondDayTemperature; // Forecasted temperature for 2nd day

        [ObservableProperty]
        private string secondDayWeatherIcon; // URL for 2nd day weather condition icon.

        [ObservableProperty]
        private string secondDayWeatherDescription; // Textual description of 2nd's day weather.

        [ObservableProperty]
        private string secondDayHumidity; // Forecasted humidity for 3rd day

        [ObservableProperty]
        private string thirdDayTemperature; // Forecasted temperature for 3rd day

        [ObservableProperty]
        private string thirdDayWeatherIcon; // URL for 3rd day weather condition icon.

        [ObservableProperty]
        private string thirdDayWeatherDescription; // Textual description of 3rd's day weather.

        [ObservableProperty]
        private string thirdDayHumidity; // Forecasted humidity for 3rd day

        [ObservableProperty]
        private Forecast forecast; // Complete forecast object.

        [ObservableProperty]
        private ForecastDayDetail todayForecast; // Detailed forecast for today

        [ObservableProperty]
        private ForecastDayDetail tomorrowForecast; // Detailed forecast for tomorrow.

        [ObservableProperty]
        private ForecastDayDetail secondDayForecast; // Detailed forecast for the second day.

        [ObservableProperty]
        private ForecastDayDetail thirdDayForecast; // Detailed forecast for the third day.

        [ObservableProperty]
        private DateTime currentDate;

        [ObservableProperty]
        private ObservableCollection<ForecastDayDetail> threeDayForecastDetails; // Collection of detailed forecasts for the next three days.

        [ObservableProperty]
        private ObservableCollection<ForecastHour> hourlyForecast; // Collection of hourly forecast data for the current day.

        // Relay Command attribute that defines an asynchronous method for fetching weather information.
        [RelayCommand]
        public async Task FetchWeatherInformation()
        {
            // Calls the weather service to get weather information for the next three days.
            var weatherData = await _weatherService.GetWeatherInformation(LocationInput, 3);
            if (weatherData?.Forecast?.ForecastDay != null && weatherData.Forecast.ForecastDay.Count >= 3)
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
                TomorrowForecast = weatherData.Forecast.ForecastDay[0].Day;
                TomorrowTemperature = $"{TomorrowForecast.AvgTempC}°C / {TomorrowForecast.AvgTempF}°F";
                TomorrowWeatherIcon = "http:" + TomorrowForecast.Condition.Icon;
                TomorrowWeatherDescription = TomorrowForecast.Condition.Text;
                TomorrowHumidity = $"{TomorrowForecast.AvgHumidity}%";

                // 2nd Day's weather forecast assignments
                SecondDayForecast = weatherData.Forecast.ForecastDay[1].Day;
                SecondDayTemperature = $"{SecondDayForecast.AvgTempC}°C / {SecondDayForecast.AvgTempF}°F";
                SecondDayWeatherIcon = "http:" + SecondDayForecast.Condition.Icon;
                SecondDayWeatherDescription = SecondDayForecast.Condition.Text;
                SecondDayHumidity = $"{SecondDayForecast.AvgHumidity}%";

                // 3rd Day's weather forecast assignments
                ThirdDayForecast = weatherData.Forecast.ForecastDay[2].Day;
                ThirdDayTemperature = $"{ThirdDayForecast.AvgTempC}°C / {ThirdDayForecast.AvgTempF}°F";
                ThirdDayWeatherIcon = "http:" + ThirdDayForecast.Condition.Icon;
                ThirdDayWeatherDescription = ThirdDayForecast.Condition.Text;
                ThirdDayHumidity = $"{ThirdDayForecast.AvgHumidity}%";

                ThreeDayForecastDetails = new ObservableCollection<ForecastDayDetail>(
                  weatherData.Forecast.ForecastDay.Take(3).Select(fd => fd.Day));

                // Extract and store the hourly forecast for the current day.
                HourlyForecast = new ObservableCollection<ForecastHour>(weatherData.Forecast.ForecastDay[0].Hour);





            }
        }
    }
}
