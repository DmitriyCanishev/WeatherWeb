using System;
using System.Threading.Tasks;
using ServerApi.Data;

namespace ServerApi
{
    public class ServerApi : IServerApi
    {
        private IHttpClient _httpClient = null;
        
        private const string BaseUrl = "api.openweathermap.org/data/2.5/weather?q=";

        public ServerApi(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<WeatherData> GetWeatherData(string cityUrl)
        {
            var uriBuilder = new UriBuilder(BaseUrl + cityUrl);
            var profile = await _httpClient.Get<WeatherData>(uriBuilder.Uri);
            return profile;
        }

    }
}