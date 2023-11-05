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

        [ObservableProperty]
        private string tomorrowTemperature;

        [ObservableProperty]
        private string tomorrowWeatherIcon;

        [ObservableProperty]
        private string tomorrowWeatherDescription;

        [ObservableProperty]
        private string tomorrowHumidity;

        [ObservableProperty]
        private Forecast forecast;

        [ObservableProperty]
        private ForecastDayDetail tomorrowForecast;

        [ObservableProperty]
        private ObservableCollection<ForecastDayDetail> threeDayForecastDetails;



        [RelayCommand]
        public async Task FetchWeatherInformation()
        {
            var weatherData = await _weatherService.GetWeatherInformation(LocationInput,3);
            if (weatherData?.Forecast?.ForecastDay != null && weatherData.Forecast.ForecastDay.Count >= 2)
            {
            
                //Assign values to ViewModel properties accordingly

                WeatherIcon = "http:" + weatherData.Current.Condition.Icon;
                Temperature = $"{weatherData.Current.TemperatureCelsius}°C / {weatherData.Current.TemperatureFahrenheit}°F";
                WeatherDescription = weatherData.Current.Condition.Text;
                LocationOutput = $"{weatherData.Location.Name}, {weatherData.Location.Region}, {weatherData.Location.Country}";
                Humidity = $"{weatherData.Current.Humidity}%";
                CloudCoverLevel = $"{weatherData.Current.Cloud}%";
                IsDay = weatherData.Current.IsDay == 1 ? "Day" : "Night";

               // Assign tomorrow's forecast values
                 TomorrowForecast = weatherData.Forecast.ForecastDay[1].Day;
                TomorrowTemperature = $"{TomorrowForecast.MaxTempC}°C / {TomorrowForecast.MaxTempF}°F";
                TomorrowWeatherIcon = "http:" + TomorrowForecast.Condition.Icon;
                TomorrowWeatherDescription = TomorrowForecast.Condition.Text;
                TomorrowHumidity = $"{TomorrowForecast.AvgHumidity}%";

                // Assign 3-day forecast values
                ThreeDayForecastDetails = new ObservableCollection<ForecastDayDetail>(
                    weatherData.Forecast.ForecastDay.Take(3).Select(fd => fd.Day));



            }

        }

       

    }
}