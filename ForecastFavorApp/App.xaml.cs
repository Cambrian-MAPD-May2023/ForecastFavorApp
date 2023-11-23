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

            // Attempt to load users from the cloud database.
            var users = await LoadUsersFromCloud();
            if (users.Any())
            {
                // If users exist, initialize the view model with the first user's preferences.
                string defaultUsername = users.First().Username;
                var mainPage = MainPage as NavigationPage;
                // Make sure the current page is WeatherPage and it's loaded
                if (mainPage?.CurrentPage is WeatherPage weatherPage)
                {
                    // Get the view model from the WeatherPage
                    if (weatherPage.BindingContext is WeatherViewModel viewModel)
                    {
                        await viewModel.InitializeAsync(defaultUsername);
                    }
                }
            }
            // If no users exist, the WeatherPage will prompt for new user details.
        }

        // Method to load users from the cloud database.
        private async Task<IEnumerable<UserPreferences>> LoadUsersFromCloud()
        {
            // Implement logic to fetch user preferences from the cloud.
            // This is a placeholder for actual cloud storage fetching logic.
            return await Task.FromResult(Enumerable.Empty<UserPreferences>());
        }
    }
}