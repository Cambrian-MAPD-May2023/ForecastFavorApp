using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Diagnostics;
using ForecastFavorApp.Models;
using ForecastFavorApp;
using System.Threading.Tasks;
using System.Net.Http.Json;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri(Constants.API_BASE_URL);
    }

    public async Task<weatherData> GetWeatherInformation(string location)
    {
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
            return null;

        try
        {
            string url = $"{Constants.API_BASE_URL}/current.json?key={Constants.API_KEY}&q={location}&aqi=yes";
            var response = await _httpClient.GetFromJsonAsync<weatherData>(url);
            return response;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}
