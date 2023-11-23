using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavorApp.Models
{
    /// <summary>
    /// Represents user-specific Alert and Notifications for the application.
    /// 
    /// Fields:
    /// - Id: A unique identifier for the user's preferences.
    /// - NotificationPreferences: List of weather conditions the user wants to be notified about.
    /// - ReceiveStormNotifications: Indicates whether the user wants to receive storm notifications.
    /// - ReceiveSunnyDayNotifications: Indicates whether the user wants to receive sunny day notifications.
    /// - ReceiveRainyDayNotifications: Indicates whether the user wants to receive rainy day notifications.
    /// </summary>

    public class AlertsAndNotifications
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public List<NotificationType> NotificationPreferences { get; set; } = new List<NotificationType>();

        public bool ReceiveStormNotifications { get; set; } = false;

        public bool ReceiveSunnyDayNotifications { get; set; } = false;

        public bool ReceiveRainyDayNotifications { get; set; } = false;
    }

    // Make sure the NotificationType enum is defined in the same namespace or included via a using directive
    public enum NotificationType
    {
        RainyDay,
        SunnyDay,
        StormAlert,
        CloudyDay,
        WindyDay,
        SnowyDay,

       
    }
}