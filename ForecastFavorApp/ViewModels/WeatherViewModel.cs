using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Threading.Tasks;
using ForecastFavorApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Azure.Data.Tables;
using Azure;
using Newtonsoft.Json;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.Shapes;
using System.Linq;
using System.Threading.Channels;


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
        private string _weatherNotification0;
        public string WeatherNotification0
        {
            get { return _weatherNotification0; }
            set
            {
                if (_weatherNotification0 != value)
                {
                    _weatherNotification0 = value;
                    OnPropertyChanged(nameof(WeatherNotification0));
                }
            }
        }

        private string _weatherNotification1;
        public string WeatherNotification1
        {
            get { return _weatherNotification1; }
            set
            {
                if (_weatherNotification1 != value)
                {
                    _weatherNotification1 = value;
                    OnPropertyChanged(nameof(WeatherNotification1));
                }
            }
        }

        private string _weatherNotification2;
        public string WeatherNotification2
        {
            get { return _weatherNotification2; }
            set
            {
                if (_weatherNotification2 != value)
                {
                    _weatherNotification2 = value;
                    OnPropertyChanged(nameof(WeatherNotification2));
                }
            }
        }

        [ObservableProperty]
        private UserPreferences userPreferences;


        private const string UserPreferencesTableName = "userpreferences";
        string accountName = "forecastfavorstorage";
        string accountKey = "fZ/2jTsX0VVeFK4hZNG/ulU60TaHR3bhVmXjHCoXIp2OAuDbBmzvJzNxEz36H6UaOOeSOLItg6X8+AStyBN5VQ==";

        // PreferencesViewModel Commands
        public ICommand SaveUserPreferencesCommand { get; }
        /// <summary>
        /// Initializes a new instance of the WeatherViewModel class.
        /// </summary>
        public WeatherViewModel()
        {
            // Instantiates the weather service to fetch weather data.
            _weatherService = new WeatherService();
            CurrentDate = DateTime.Now;// Sets the current date.

            // Initialize commands.
            SaveUserPreferencesCommand = new AsyncRelayCommand<string>(SaveUserPreferencesToCloudAsync);


        }

        public async Task InitializeAsync(string username)
        {
            await LoadUserPreferencesAsync(username);
            // Any other initialization logic
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
            if (string.IsNullOrWhiteSpace(LocationInput))
            {

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
                // After processing weather data
                CheckAndSendNotificationsBasedOnPreferences();
                WeatherNotificationLabel();
            }
        }

        // This method checks weather conditions and user preferences to send notifications accordingly.
        private void CheckAndSendNotificationsBasedOnPreferences()
        {
            // Example: Check if it's sunny and user wants sunny notifications
            if (WeatherDescription.Contains("Sunny") && SunnyNotificationEnabled)
            {
                // If it's sunny and sunny notifications are enabled, send a notification about the weather.
                SendNotification("Glorious Sunshine Awaits", "It's a perfect day for a picnic or a leisurely walk in the park. Don't forget your sunscreen!");
            }

            // Check for rainy conditions and user preferences.
            if (WeatherDescription.Contains("rain") && RainyNotificationEnabled)
            {
                // If it's rainy and rainy notifications are enabled, send a notification about the weather.
                SendNotification("Rainy Day Alert!", "It looks like it's time to grab your umbrella. A cozy coffee shop visit might be just the thing!");
            }

            // Check for snowy conditions and user preferences.
            if (WeatherDescription.Contains("snow") && SnowyNotificationEnabled)
            {
                // If it's snowy and snowy notifications are enabled, send a notification about the weather.
                SendNotification("Snowflakes Are Falling!", "The world is your snow globe! A good day for building a snowman or enjoying hot chocolate by the fire.");
            }

            // Check for thunderstorm conditions and user preferences.
            if (WeatherDescription.Contains("thunder") && StormNotificationEnabled)
            {
                // If there's a thunderstorm and storm notifications are enabled, send a notification about the weather.
                SendNotification("Storm Brewing!", "Best to stay indoors today. It's a great opportunity to catch up on a book or binge-watch your favorite show.");
            }

            // Check for overcast, cloudy, or foggy conditions and user preferences.
            if ((WeatherDescription.Contains("Overcast") || WeatherDescription.Contains("cloud") || WeatherDescription.Contains("Fog")) && CloudyNotificationEnabled)
            {
                // If it's overcast, cloudy, or foggy and cloudy notifications are enabled, send a notification about the weather.
                SendNotification("Overcast Skies Today", "A moody sky sets the stage. Perfect for a trip to the museum or a relaxed day at home.");
            }
        }



        private void WeatherNotificationLabel()
        {
            string forecastText0 = GetForecastText(0);
            string forecastText1 = GetForecastText(1);
            string forecastText2 = GetForecastText(2);

            // WeatherNotification0
            WeatherNotification0 = GetWeatherNotificationText(forecastText0);

            // WeatherNotification1
            WeatherNotification1 = GetWeatherNotificationText(forecastText1);

            // WeatherNotification2
            WeatherNotification2 = GetWeatherNotificationText(forecastText2);

            // Additional WeatherNotification for the current day
            WeatherNotificationToday = GetWeatherNotificationText(WeatherDescription);

        }

        private string GetWeatherNotificationText(string forecastText)
        {
            switch (true)
            {
                case var _ when ContainsPartialMatch(forecastText, "Sunny", "sun"):
                    return "Glorious Sunshine Awaits. It's a perfect day for a picnic or a leisurely walk in the park. Don't forget your sunscreen!";

                case var _ when ContainsPartialMatch(forecastText, "rain"):
                    return "Rainy Day Alert! It looks like it's time to grab your umbrella. A cozy coffee shop visit might be just the thing!";

                case var _ when ContainsPartialMatch(forecastText, "snow"):
                    return "Snowflakes Are Falling! The world is your snow globe! A good day for building a snowman or enjoying hot chocolate by the fire.";

                case var _ when ContainsPartialMatch(forecastText, "Overcast", "cloud", "Fog"):
                    return "Overcast Skies Today A moody sky sets the stage. Perfect for a trip to the museum or a relaxed day at home.";

                case var _ when ContainsPartialMatch(forecastText, "thunder"):
                    return "Storm Brewing! Best to stay indoors today. It's a great opportunity to catch up on a book or binge-watch your favorite show.";

                default:
                    return WeatherDescription;
            }
        }

        private bool ContainsPartialMatch(params string[] texts)
        {
            foreach (var text in texts)
            {
                if (text != null && ContainsPartialMatch(text, "Sunny", "sun", "rain", "snow", "Overcast", "cloud", "Fog", "thunder"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool ContainsPartialMatch(string text, params string[] keywords)
        {
            return keywords.Any(keyword => text.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) != -1);
        }

        private string GetForecastText(int index)
        {
            return ThreeDayForecastDetails[index].Condition.Text;
        }

        // This method sends a notification with the specified title and message.
        private void SendNotification(string title, string message)
        {
            // Create a new notification request.
            var notification = new NotificationRequest
            {
                NotificationId = new Random().Next(),
                Title = title,
                Description = message,
                ReturningData = "DetailInfo", // Custom data
                BadgeNumber = 42,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(5),
                }
            };

            // Show the notification using the local notification center.
            LocalNotificationCenter.Current.Show(notification);

            // Add a console print statement to confirm the notification is being sent.
            Console.WriteLine($"Notification Sent: Title='{title}', Message='{message}'");
        }
        private string _weatherNotificationToday;
        public string WeatherNotificationToday
        {
            get { return _weatherNotificationToday; }
            set
            {
                if (_weatherNotificationToday != value)
                {
                    _weatherNotificationToday = value;
                    OnPropertyChanged(nameof(WeatherNotificationToday));
                }
            }
        }


        [ObservableProperty]
        private string unit;

        [ObservableProperty]
        private string theme;

        [ObservableProperty]
        private Dictionary<NotificationType, bool> notificationPreferences;

        [ObservableProperty]
        private string location1;

        [ObservableProperty]
        private string location2;

        [ObservableProperty]
        private string location3;

        [ObservableProperty]
        private bool stormNotificationEnabled;

        [ObservableProperty]
        private bool sunnyNotificationEnabled;

        [ObservableProperty]
        private bool snowyNotificationEnabled;

        [ObservableProperty]
        private bool rainyNotificationEnabled;

        [ObservableProperty]
        private bool cloudyNotificationEnabled;

        /// <summary>
        /// Loads the user preferences, potentially from local storage or a remote database.
        /// </summary>
        public async Task LoadUserPreferencesAsync(string username)
        {
            Uri accountUri = new Uri("https://forecastfavorstorage.table.core.windows.net");
            var tableClient = new TableClient(accountUri, UserPreferencesTableName, new TableSharedKeyCredential(accountName, accountKey));
            await tableClient.CreateIfNotExistsAsync();

            string partitionKey = username; // Use username as the partition key to identify the user

            try
            {
                var response = await tableClient.GetEntityAsync<TableEntity>(username, "UserPreferences");
                if (response.Value != null)
                {
                    var entity = response.Value;

                    // Load individual locations from separate columns
                    Location1 = entity.GetString("Location1");
                    Location2 = entity.GetString("Location2");
                    Location3 = entity.GetString("Location3");

                    // Assuming these fields exist in your table storage
                    StormNotificationEnabled = entity.GetBoolean("StormNotificationEnabled") ?? false;
                    SunnyNotificationEnabled = entity.GetBoolean("SunnyNotificationEnabled") ?? false;
                    RainyNotificationEnabled = entity.GetBoolean("RainyNotificationEnabled") ?? false;
                    SnowyNotificationEnabled = entity.GetBoolean("SnowyNotificationEnabled") ?? false;
                    CloudyNotificationEnabled = entity.GetBoolean("SnowyNotificationEnabled") ?? false;

                    // Update the ViewModel properties directly from the TableEntity
                    Unit = entity.GetString("Unit");
                    Theme = entity.GetString("Theme");

                   

                   
                    
                }
            }
            catch (RequestFailedException ex) when (ex.Status == 404)
            {
                Unit = ""; // Default to Celsius
                Theme = ""; // Enum value for default theme
                Location1 = ""; // Initialize with empty string for location 1
                Location2 = ""; // Initialize with empty string for location 2
                Location3 = ""; // Initialize with empty string for location 3
                StormNotificationEnabled = false; // Default to false for storm notifications
                SunnyNotificationEnabled = false; // Default to false for sunny notifications
                RainyNotificationEnabled = false; // Default to false for rainy notifications 
                SnowyNotificationEnabled = false; // Default to false for snow notifications 
                CloudyNotificationEnabled = false; // Default to false for cloud notifications 
            }

            catch (Exception ex)
            {
                // Handle other exceptions that might occur
                // Consider logging the exception
            }
        }

        /// <summary>
        /// Saves the user preferences to Azure Table Storage.
        /// </summary>
        public async Task SaveUserPreferencesToCloudAsync(string username)
        {
            // Logic to save the preferences to Azure Table Storage.
            Uri accountUri = new Uri("https://forecastfavorstorage.table.core.windows.net");
            var tableClient = new TableClient(accountUri, UserPreferencesTableName, new TableSharedKeyCredential(accountName, accountKey));
            await tableClient.CreateIfNotExistsAsync();

            // Collect individual locations into a list
            var savedLocations = string.Join(",", new[] { Location1, Location2, Location3 }
                                       .Where(location => !string.IsNullOrWhiteSpace(location)));


            // Create the entity to save
            var entity = new TableEntity
            {
                PartitionKey = username,
                RowKey = "UserPreferences",
                ["Unit"] = Unit.ToString(),
                ["Theme"] = Theme.ToString(),
                // Save individual locations into separate columns
                ["Location1"] = Location1 ?? "", // Using ?? to handle possible null values
                ["Location2"] = Location2 ?? "",
                ["Location3"] = Location3 ?? "",
                ["StormNotificationEnabled"] = StormNotificationEnabled,
                ["SunnyNotificationEnabled"] = SunnyNotificationEnabled,
                ["SnowyNotificationEnabled"] = SunnyNotificationEnabled,
                ["RainyNotificationEnabled"] = RainyNotificationEnabled,
                ["CloudyNotificationEnabled"] = CloudyNotificationEnabled
            };


            await tableClient.UpsertEntityAsync(entity, TableUpdateMode.Replace);
        }
    }
}
