using Microsoft.Maui.Controls;
using System;
using ForecastFavorApp.ViewModels;

// Did code reveiew - Sreenath

namespace ForecastFavorApp.Views
{
    // WeatherPage inherits from TabbedPage, representing a page with tabs in the application.
    public partial class WeatherPage : TabbedPage
    {
        WeatherViewModel _viewModel;

        public WeatherPage()
        {
            InitializeComponent();
            // ViewModel is not instantiated here anymore.
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel == null)
            {
                _viewModel = new WeatherViewModel();
                BindingContext = _viewModel;

                string username = await GetUsernameAsync(); // Implement this method to retrieve the username
                await _viewModel.InitializeAsync(username);

                try
                {
                    await _viewModel.FetchWeatherInformation();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
                }
            }
        }

        private async void OnSavePreferencesClicked(object sender, EventArgs e)
        {
            // Make sure to check for null or invalid username.
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text))
            {
                await _viewModel.SaveUserPreferencesToCloudAsync(usernameEntry.Text);
            }
            else
            {
                await DisplayAlert("Validation", "You must enter a username.", "OK");
            }
        }

        // Implement this method to retrieve the username
        private Task<string> GetUsernameAsync()
        {
            // You can replace this with the actual logic to obtain the username
            // For instance, checking a local cache or prompting the user
            return Task.FromResult("defaultUser");
        }
    }

}
