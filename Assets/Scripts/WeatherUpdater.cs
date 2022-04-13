using System;
using ServerApi.Data;
using TMPro;
using UnityEngine;

public class WeatherUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _cityName;
    [SerializeField] private TextMeshProUGUI _temperatureValue;

    public event Action WeatherUpdate;
    
    public void UpdateWeatherInfo(WeatherData weather)
    {
        _cityName.text = weather.CityName;
        _temperatureValue.text = (weather.Main.Temp - 273).ToString("F0");
    }

    public void GetNewWeatherState()
    {
        WeatherUpdate?.Invoke();
    }
}
