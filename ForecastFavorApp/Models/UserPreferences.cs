using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForecastFavorApp.Models
{
    public class UserPreferences
    {
        public Guid Id { get; set; } = Guid.NewGuid();


        public List<string> SavedLocations { get; set; } = new List<string>();

        public bool IsCelsius { get; set; } = true;


        public AppTheme Theme { get; set; } = AppTheme.Light;


        public List<NotificationType> NotificationPreferences { get; set; } = new List<NotificationType>();
    }

    public enum AppTheme
    {
        Light,
        Dark

    }

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