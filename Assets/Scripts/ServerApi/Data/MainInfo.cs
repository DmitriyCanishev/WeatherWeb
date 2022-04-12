using Newtonsoft.Json;

namespace ServerApi.Data
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MainInfo
    {
        [JsonProperty(PropertyName = "temp")] public float Temp { get; private set; }
    }
}