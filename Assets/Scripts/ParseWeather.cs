using System;
using System.Threading.Tasks;
using ServerApi;
using ServerApi.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using JsonSerializer = ServerApi.JsonSerializer;
using Random = UnityEngine.Random;

public class ParseWeather : MonoBehaviour
{
    public readonly ObservableVariable<WeatherData> Weather = new ObservableVariable<WeatherData>();
    
    private IServerApi _serverApi = null;
    private readonly JsonSerializer _jsonSerializer = new JsonSerializer();
    private WeatherUpdater _weatherUpdater;
    
    private readonly string _spbWeatherUrl = "Saint Petersburg, RU&appid=6401b1c3fba71804f1de07a693053ca3";

    private static readonly string[] WeatherUrl =
    {
        "Timashëvsk, RU&appid=6401b1c3fba71804f1de07a693053ca3",
        "Moscow, RU&appid=6401b1c3fba71804f1de07a693053ca3",
        "Krasnodar, RU&appid=6401b1c3fba71804f1de07a693053ca3"
    };

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _serverApi = new ServerApi.ServerApi(new HttpClient(_jsonSerializer));
    }

    public async void Start()
    {
        await LoadWeatherState();
        _weatherUpdater.WeatherUpdate += GetNewWeatherState;
    }

    private async Task LoadWeatherState()
    {
        await SceneManager.LoadSceneAsync("Weather", LoadSceneMode.Single).ToTask();
        
        try
        {
            Weather.Value = await _serverApi.GetWeatherData(_spbWeatherUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        if (SceneManagerUtils.IsSceneOpened("Weather"))
        {
            _weatherUpdater = FindObjectOfType<WeatherUpdater>();
            _weatherUpdater.UpdateWeatherInfo(Weather.Value);
        }
    }
    
    private async void GetNewWeatherState()
    {
        Weather.Value = await _serverApi.GetWeatherData(GetRandomWeatherUrl());
        _weatherUpdater.UpdateWeatherInfo(Weather.Value);
    }
    
    private string GetRandomWeatherUrl()
    {
        var index = Random.Range(0, WeatherUrl.Length);
        return WeatherUrl[index];
    }

    private void OnDestroy()
    {
        _weatherUpdater.WeatherUpdate -= GetNewWeatherState;
    }
}
