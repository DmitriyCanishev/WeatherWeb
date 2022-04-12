using System.Threading.Tasks;
using ServerApi.Data;

namespace ServerApi
{
    public interface IServerApi
    {
        Task<WeatherData> GetWeatherData(string cityUrl);
    }
}