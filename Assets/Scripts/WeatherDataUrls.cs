using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "WeatherDataUrls", menuName = "WeatherUrls")]
public class WeatherDataUrls : ScriptableObject
{
    [field:SerializeField] public List<WeatherInfo> WeatherUrls { get; private set; }
    
    private readonly Random _random = new Random();
    
    [Serializable]
    public class WeatherInfo
    {
        [field:SerializeField] public string CityName { get; private set; }
        [field:SerializeField] public string CityUrl { get; private set; }
    }

    public WeatherInfo GetRandomCityUrl() => 
        WeatherUrls[_random.Next(WeatherUrls.Count)];

    public WeatherInfo GetWeatherInfoConfig(int index) => 
        WeatherUrls[Math.Max(0, Math.Min(index, WeatherUrls.Count))];
}