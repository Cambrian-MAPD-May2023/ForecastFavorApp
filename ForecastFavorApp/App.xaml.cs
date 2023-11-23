using ForecastFavorApp.Views;
using ForecastFavorApp.Models;
using ForecastFavorApp.ViewModels;
namespace ForecastFavorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new WeatherPage());

        }

        protected override async void OnStart()
        {
            base.OnStart();

            // Use "dennis" as the default username.
            string username = "dennis";

            // Load user preferences for the default user.
            if (MainPage is NavigationPage navigationPage)
            {
                if (navigationPage.CurrentPage.BindingContext is WeatherViewModel viewModel)
                {
                    await viewModel.LoadUserPreferencesAsync(username);
                }
            }

        }

        
    }
}