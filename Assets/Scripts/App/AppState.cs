using System;
using System.Threading.Tasks;
using ScriptableObjects;
using ServerApi;
using ServerApi.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace App
{
    public class AppState : MonoBehaviour
    {
        [SerializeField] private WeatherDataUrls _weatherDataUrls = null;

        public readonly ObservableVariable<WeatherData> Weather = new ObservableVariable<WeatherData>();

        private IServerApi _serverApi = null;
        private readonly JsonSerializer _jsonSerializer = new JsonSerializer();

        private int _cityIndex;

        public void SetCityIndex(int cityIndex)
        {
            _cityIndex = cityIndex;
        }

        public async Task LoadWeatherState()
        {
            try
            {
                Weather.Value = await _serverApi.
                    GetWeatherData(_weatherDataUrls.GetWeatherInfoConfig(_cityIndex).CityUrl);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _serverApi = new ServerApi.ServerApi(new HttpClient(_jsonSerializer));
        }

        private async void Start()
        {
            await SceneManager.LoadSceneAsync("Weather", LoadSceneMode.Single).ToTask();
        }
    }
}