using Newtonsoft.Json;

namespace ServerApi.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class WeatherData
    {
        [JsonProperty(PropertyName = "main")] public MainInfo Main { get; private set; }
        [JsonProperty(PropertyName = "name")] public string CityName { get; private set; }
        
    }
}