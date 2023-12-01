using Android.App;
using Android.Content.PM;
using Android.OS;

namespace ForecastFavorApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // ...

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                const string channelId = "default_notification_channel_id";
                const string channelName = "Default";
                const string channelDescription = "The default channel for notifications.";

                var channel = new NotificationChannel(channelId, channelName, NotificationImportance.High)
                {
                    Description = channelDescription
                };

                var notificationManager = (NotificationManager)GetSystemService(NotificationService);
                notificationManager.CreateNotificationChannel(channel);
            }

            // ...
        }

    }
}