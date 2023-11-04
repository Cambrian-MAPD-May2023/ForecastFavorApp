using ForecastFavorApp.Views;
namespace ForecastFavorApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new WeatherPage();
        }
    }
}