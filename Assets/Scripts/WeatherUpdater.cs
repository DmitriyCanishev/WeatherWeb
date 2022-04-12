using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WeatherUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private TextMeshProUGUI _temperatureValue;
    [SerializeField] private UnityEvent _updatingWeather = null;

    public void UpdateWeatherInfo()
    {
        _updatingWeather.Invoke();
        
        // _cityName.text = _weatherData.CityName;
        // _temperatureValue.text = (_weatherData.Main.Temp - 273).ToString("F0");
    }
}
