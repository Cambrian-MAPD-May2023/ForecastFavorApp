using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavorApp.Models
{
    /// <summary>
    /// Represents user-specific settings and preferences for the application.
    /// 
    /// Fields:
    /// - Id: A unique identifier for each user's preferences.
    /// - SavedLocations: List of locations the user is interested in tracking weather for.
    /// - IsCelsius: Preference for displaying temperature in Celsius. True for Celsius, False for Fahrenheit.
    /// - Theme: The preferred app theme (Light or Dark).
    /// - NotificationPreferences: List of weather conditions the user wants to be notified about.
    /// </summary>
    public class UserPreferences
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public List<string> SavedLocations { get; set; } = new List<string>();

        public bool IsCelsius { get; set; } = true;


        public AppTheme Theme { get; set; } = AppTheme.Light;


        public List<NotificationType> NotificationPreferences { get; set; } = new List<NotificationType>();
    }
    /// <summary>
    /// Enumerates the possible themes for the app's appearance.
    /// </summary>
    public enum AppTheme
    {
        Light,
        Dark

    }
    /// <summary>
    /// Enumerates the types of weather notifications a user can opt to receive.
    /// 
    /// Values:
    /// - RainyDay: Notification for rainy days.
    /// - SunnyDay: Notification for sunny days.
    /// - StormAlert: Notification for stormy weather.
    /// - CloudyDay: Notification for cloudy days.
    /// - WindyDay: Notification for windy days.
    /// - SnowyDay: Notification for snowy days.
    /// </summary>
    public enum NotificationType
    {
        RainyDay,
        SunnyDay,
        StormAlert,
        CloudyDay,
        WindyDay,
        SnowyDay

    }
}