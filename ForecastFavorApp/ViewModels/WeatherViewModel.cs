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
    /// <summary>
    /// The WeatherViewModel class, responsible for handling the UI logic for weather data presentation.
    /// It inherits from ObservableObject to enable UI updates through data binding.
    /// </summary>
    internal partial class WeatherViewModel : ObservableObject
    {
        // Private field to hold the weather service instance.
        private readonly WeatherService _weatherService;

        /// <summary>
        /// Initializes a new instance of the WeatherViewModel class.
        /// </summary>
        public WeatherViewModel()
        {
            // Instantiates the weather service to fetch weather data.
            _weatherService = new WeatherService();
            CurrentDate = DateTime.Now;// Sets the current date.
        }

        // Observable properties. These properties are bound to the UI and notify it of any changes to their values.
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
        private string precipitationMm; // Current precipitation

        [ObservableProperty]
        private string wind; // Current wind

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
        private DateTime currentDate; // Holds the current date. It is automatically set to the current system date upon initialization of the ViewModel.


        [ObservableProperty]
        private string todayDayOfWeek;// Represents the day of the week for the current date as a string. This is useful for displaying today's day name in the UI.

        [ObservableProperty]
        private string tomorrowDayOfWeek; // Contains the day of the week for the day following the current date. This property can be used to display the name of the next day in the UI.


        [ObservableProperty]
        private string dayAfterTomorrowDayOfWeek;// Stores the day of the week for two days after the current date, allowing the UI to display "the day after tomorrow's" day name.

        [ObservableProperty]
        private string dayAfterAfterTomorrowDayOfWeek; // Holds the day of the week for three days after the current date, enabling the UI to show the name of this future day


        [ObservableProperty]
        private string tomorrowDate;// Contains the date for the day following the current date in a string format, often used for displaying the date in a user-friendly format.

        [ObservableProperty]
        private string tomorrowTempRange;
        // Contains the date for the day following the current date in a string format, often used for displaying the date in a user-friendly format.

        [ObservableProperty]
        private ObservableCollection<ForecastDayDetail> threeDayForecastDetails; // Collection of detailed forecasts for the next three days.

        [ObservableProperty]
        private ObservableCollection<ForecastHour> hourlyForecast; // Collection of hourly forecast data for the current day.
        [ObservableProperty]
        private ObservableCollection<ForecastHour> hourlyForecast2; // Collection of hourly forecast data for the second day
        [ObservableProperty]
        private ObservableCollection<ForecastHour> hourlyForecast3; // Collection of hourly forecast data for the third day

        /// <summary>
        /// Asynchronously fetches weather information based on the user's location input.
        /// </summary>
        [RelayCommand]
        public async Task FetchWeatherInformation()
        {
            // Default location to "Sudbury" if no input is given.
            if (string.IsNullOrWhiteSpace(LocationInput)) {

                LocationInput = "Sudbury";
            }
            // Calls the weather service to retrieve weather data.
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
                PrecipitationMm = $"{weatherData.Current.PrecipitationMm}mm";
                Wind = $"{weatherData.Current.WindKph}kph";
                CloudCoverLevel = $"{weatherData.Current.Cloud}%";
                IsDay = weatherData.Current.IsDay == 1 ? "Day" : "Night";
              

                // Tomorrow's weather forecast assignments
                TomorrowForecast = weatherData.Forecast.ForecastDay[0].Day;
                TomorrowTemperature = $"{TomorrowForecast.AvgTempC}°C / {TomorrowForecast.AvgTempF}°F";
                TomorrowWeatherIcon = "http:" + TomorrowForecast.Condition.Icon;
                TomorrowWeatherDescription = TomorrowForecast.Condition.Text;
                TomorrowHumidity = $"{TomorrowForecast.AvgHumidity}%";
                TomorrowTempRange = $"{TomorrowForecast.MinTempC}°C / {TomorrowForecast.MaxTempC}°C";

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

                // Initializes the ThreeDayForecastDetails collection with forecast details for the next three days.
                ThreeDayForecastDetails = new ObservableCollection<ForecastDayDetail>(
                  weatherData.Forecast.ForecastDay.Take(3).Select(fd => fd.Day));

                // Extract and store the hourly forecast for the current day.
                HourlyForecast = new ObservableCollection<ForecastHour>(weatherData.Forecast.ForecastDay[0].Hour);
                // Extract and store the hourly forecast for the second day.
                HourlyForecast2 = new ObservableCollection<ForecastHour>(weatherData.Forecast.ForecastDay[1].Hour);
                // Extract and store the hourly forecast for the third day.
                HourlyForecast3 = new ObservableCollection<ForecastHour>(weatherData.Forecast.ForecastDay[2].Hour);

                // Variables to hold the day of the week for each of the three days
                string dayOfWeekToday = CurrentDate.DayOfWeek.ToString();
                string dayOfWeekTomorrow = CurrentDate.AddDays(1).DayOfWeek.ToString();
                string dayOfWeekDayAfterTomorrow = CurrentDate.AddDays(2).DayOfWeek.ToString();
                string dayOfWeekDayAfterAfterTomorrow = CurrentDate.AddDays(3).DayOfWeek.ToString();
                string tomorrowDate = CurrentDate.AddDays(1).Date.ToString("MMMM dd");

                // Assigning the formatted day names to the respective observable properties.
                TodayDayOfWeek = dayOfWeekToday;
                TomorrowDayOfWeek = dayOfWeekTomorrow;
                DayAfterTomorrowDayOfWeek = dayOfWeekDayAfterTomorrow;
                DayAfterAfterTomorrowDayOfWeek = dayOfWeekDayAfterAfterTomorrow;
                TomorrowDate = tomorrowDate;

            }
        }
    }
}
