using Microsoft.Maui.Controls;
using System;
using ForecastFavorApp.ViewModels;

namespace ForecastFavorApp.Views
{
    // WeatherPage inherits from TabbedPage, representing a page with tabs in the application.
    public partial class WeatherPage : TabbedPage
    {
        WeatherViewModel _viewModel; // ViewModel instance for the WeatherPage.

        // Constructor for the WeatherPage.
        public WeatherPage()
        {
            InitializeComponent(); // Method call to initialize the page's components.
            _viewModel = new WeatherViewModel(); // Instantiate the WeatherViewModel.
            // Assign the ViewModel to the page's BindingContext to enable data binding between the View and ViewModel.
            BindingContext = _viewModel;
        }

        // OnAppearing is an event handler called when the page is about to appear on the screen.
        protected override async void OnAppearing()
        {
            base.OnAppearing(); // Call the base class implementation.
            // Exception handling for the FetchWeatherInformation operation.
            try
            {
                // Async method call to fetch weather information and update the ViewModel.
                await _viewModel.FetchWeatherInformation();
            }
            catch (Exception ex)
            {
                // If an exception occurs, display an alert to the user with the error message.
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
