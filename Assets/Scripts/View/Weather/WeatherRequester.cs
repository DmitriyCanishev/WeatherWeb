namespace View.Weather
{
    public class WeatherRequester : ViewController
    {
        public async void RequestCityWeather()
        {
            await AppState.LoadWeatherState();
        }
    }
}