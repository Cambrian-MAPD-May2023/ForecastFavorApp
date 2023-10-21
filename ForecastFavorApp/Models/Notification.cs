using System;
namespace ForecastFavorApp.Models
{
    /// <summary>
    /// Represents a notification sent to the user.
    /// 
    /// Properties:
    /// - Id: A unique identifier for each notification.
    /// - NotificationType: The type of the notification (e.g., Weather Alert, Reminder).
    /// - Message: Content/message of the notification.
    /// - Time: Timestamp when the notification was created/sent.
    /// </summary>

    public class Notification
	{
        public Guid Id { get; set; } = Guid.NewGuid();

        public string NotificationType { get; set; }

        public string Message { get; set; }

        public DateTime Time { get; set; }
    }
}

