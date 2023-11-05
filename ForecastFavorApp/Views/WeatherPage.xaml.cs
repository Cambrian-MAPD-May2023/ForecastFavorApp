using Microsoft.Maui.Controls;
using System;
using ForecastFavorApp.ViewModels;

namespace ForecastFavorApp.Views
{
    public partial class WeatherPage : TabbedPage
    {
        WeatherViewModel _viewModel;

        public WeatherPage()
        {
            InitializeComponent();
            _viewModel = new WeatherViewModel();
            // Assign the ViewModel to the page's BindingContext to enable data binding.
            BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Make sure to handle exceptions appropriately
            try
            {
                // Assuming FetchWeatherData is an async method that makes the API call and updates the ViewModel.
                await _viewModel.FetchWeatherInformation();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the API call.
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }
}
