using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotification;
using Android.Content;
using Android.Gms.Ads;

namespace ForecastFavorApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // ...
            LocalNotificationCenter.CreateNotificationChannel(); // This sets up the notification channel

            MobileAds.Initialize(this); // Initialize the MobileAds SDK, enabling the application to use AdMob services.
        }
        // Ensure this override is included to handle notification taps
        protected override void OnNewIntent(Intent intent)
        {
            LocalNotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

    }
}