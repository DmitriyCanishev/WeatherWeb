using ServerApi.Data;
using TMPro;
using UnityEngine;

namespace Weather
{
    public class WeatherUpdater : MonoBehaviour
    {
        [SerializeField, Space(3)] private WeatherParser _weatherParser = null;

        [SerializeField] private TextMeshProUGUI _cityName;
        [SerializeField] private TextMeshProUGUI _temperatureValue;
    
        private const int CelciusDegree = 273;

        private void Awake()
        {
            _weatherParser.WeatherUpdate += OnWeatherUpdate;
        }

        private void OnWeatherUpdate(WeatherData weather)
        {
            _cityName.text = weather.CityName;
            _temperatureValue.text = (weather.Main.Temp - CelciusDegree).ToString("F0");
        }
    
        private void OnDestroy()
        {
            _weatherParser.WeatherUpdate -= OnWeatherUpdate;
        }
    }
}
