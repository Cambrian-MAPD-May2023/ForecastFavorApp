using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using ForecastFavorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ForecastFavorApp.ViewModels
{
    internal partial class WeatherViewModel : ObservableObject

    {
        private readonly WeatherService _weatherService;

        public WeatherViewModel()
        {
            _weatherService = new WeatherService();
        }
        [ObservableProperty]
        private string locationInput;

        [ObservableProperty]
        private string weatherIcon;

        [ObservableProperty]
        private string temperature;

        [ObservableProperty]
        private string weatherDescription;

        [ObservableProperty]
        private string locationOutput;

        [ObservableProperty]
        private string humidity;

        [ObservableProperty]
        private string cloudCoverLevel;

        [ObservableProperty]
        private string isDay;

        [RelayCommand]
        public async Task FetchWeatherInformation()
        {
            var weatherData = await _weatherService.GetWeatherInformation(LocationInput);
            if (weatherData != null)
            {
                //Assign values to ViewModel properties accordingly

                WeatherIcon = weatherData.Current.Condition.Icon;
                Temperature = $"{weatherData.Current.TemperatureCelsius}°C / {weatherData.Current.TemperatureFahrenheit}°F";
                WeatherDescription = weatherData.Current.Condition.Text;
                LocationOutput = $"{weatherData.Location.Name}, {weatherData.Location.Region}, {weatherData.Location.Country}";
                Humidity = $"{weatherData.Current.Humidity}%";
                CloudCoverLevel = $"{weatherData.Current.Cloud}%";
                IsDay = weatherData.Current.IsDay == 1 ? "Day" : "Night";

            }

        }

    }
}