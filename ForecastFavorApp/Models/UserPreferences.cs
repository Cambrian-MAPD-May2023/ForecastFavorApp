
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

        public string Username { get; set; } // Username for identifying the user.

        // Saved locations as individual properties for easy binding.
        public string Location1 { get; set; }
        public string Location2 { get; set; }
        public string Location3 { get; set; }

        // Store the temperature unit preference as a boolean
        public bool IsCelsius { get; set; } = true;

        // Theme preference
        public AppTheme Theme { get; set; } = AppTheme.Light;

        // Notification preferences as a dictionary to store user selections for each notification type
        public Dictionary<NotificationType, bool> NotificationPreferences { get; set; } = new Dictionary<NotificationType, bool>
        {
            { NotificationType.RainyDay, false },
            { NotificationType.SunnyDay, false },
            { NotificationType.StormAlert, false },
            { NotificationType.CloudyDay, false },
            { NotificationType.WindyDay, false },
            { NotificationType.SnowyDay, false }
        };
    }
}