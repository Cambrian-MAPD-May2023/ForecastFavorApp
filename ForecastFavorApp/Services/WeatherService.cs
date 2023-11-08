using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Diagnostics;
using ForecastFavorApp.Models;
using ForecastFavorApp;
using System.Threading.Tasks;
using System.Net.Http.Json;

// Did code reveiew - Sreenath
public class WeatherService
{
    private readonly HttpClient _httpClient; // HttpClient instance used to make web requests.

    // Constructor initializes the HttpClient and sets the base address to the API's base URL.
    public WeatherService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.API_BASE_URL); // Sets the base URL for API requests.
    }

    // Asynchronous method to get weather information for a specified location and number of days.
    // 'location' is a string representing the location for the weather query.
    // 'days' is an optional parameter that defaults to 1 if not specified, representing the forecast period in days.
    public async Task<weatherData> GetWeatherInformation(string location, int days = 1)
    {
        // Check if there is an internet connection before making a call.
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return null; // Return null if there is no internet connection.

        try
        {
            // Constructs the URL with the API key, location query, number of days for the forecast, and flags for air quality and alerts.
            string url = $"{Constants.API_BASE_URL}/forecast.json?key={Constants.API_KEY}&q={location}&days={days}&aqi=no&alerts=no";

            // Makes an asynchronous GET request to the constructed URL and awaits the response, deserializing the JSON into a weatherData object.
            var response = await _httpClient.GetFromJsonAsync<weatherData>(url);

            return response; // Returns the deserialized weatherData object.
        }
        catch (Exception ex)
        {
            // If an exception occurs, it logs the error message and returns null.
            Debug.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}
