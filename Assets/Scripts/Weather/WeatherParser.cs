using System;
using App;
using ServerApi.Data;
using UnityEngine;

namespace Weather
{
    public class WeatherParser : MonoBehaviour
    {
        [SerializeField] private DropDownHandler _dropDownHandler = null;
        
        public event Action<WeatherData> WeatherUpdate = null;

        private AppState _appState = null;
        private int _cityIndex = 0;
        
        private void Awake()
        {
            _appState = FindObjectOfType<AppState>();
            _dropDownHandler.CitySelected += OnCitySelected;
            
            LoadCityWeather();
        }

        private void OnCitySelected(int index)
        {
            _cityIndex = index;
        }

        public async void LoadCityWeather()
        {
            await _appState.LoadWeatherState(_cityIndex);
            WeatherUpdate?.Invoke(_appState.Weather.Value);
        }

        private void OnDestroy()
        {
            _dropDownHandler.CitySelected -= OnCitySelected;
        }
    }
}