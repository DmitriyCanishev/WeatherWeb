using ServerApi.Data;
using TMPro;
using UnityEngine;

namespace View.Weather
{
    public class WeatherUpdater : ViewController
    {
        [SerializeField] private TextMeshProUGUI _cityName;
        [SerializeField] private TextMeshProUGUI _temperatureValue;
    
        private const int CelciusDegree = 273;

        private void Awake()
        {
            AppState.Weather.OnAfterUpdate += OnWeatherUpdate;
        }

        private void OnWeatherUpdate(WeatherData weather)
        {
            _cityName.text = weather.CityName;
            _temperatureValue.text = (weather.Main.Temp - CelciusDegree).ToString("F0");
        }
    
        private void OnDestroy()
        {
            AppState.Weather.OnAfterUpdate -= OnWeatherUpdate;
        }
    }
}
