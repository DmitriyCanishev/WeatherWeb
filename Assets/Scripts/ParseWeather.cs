using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerApi;
using ServerApi.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using JsonSerializer = ServerApi.JsonSerializer;

public class ParseWeather : MonoBehaviour
{
    private IServerApi _serverApi = null;
    private readonly JsonSerializer _jsonSerializer = new JsonSerializer();
    
    private readonly string _spbWeatherUrl = "Saint Petersburg, RU&appid=6401b1c3fba71804f1de07a693053ca3";
    private readonly string _timashevskWeatherUrl = "Timashëvsk, RU&appid=6401b1c3fba71804f1de07a693053ca3";
    private readonly string _moscowWeatherUrl ="Moscow, RU&appid=6401b1c3fba71804f1de07a693053ca3";
    private readonly string _krasnodarWeatherUrl = "Krasnodar, RU&appid=6401b1c3fba71804f1de07a693053ca3";

    private void Awake()
    {
        DontDestroyOnLoad(this);
        _serverApi = new ServerApi.ServerApi(new HttpClient(_jsonSerializer));
    }

    public async void Start()
    {
        await LoadWeatherState();
    }

    private async Task LoadWeatherState()
    {
        try
        {
            await _serverApi.GetWeatherData(_spbWeatherUrl);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        await SceneManager.LoadSceneAsync("Weather", LoadSceneMode.Single).ToTask();
    }

}
