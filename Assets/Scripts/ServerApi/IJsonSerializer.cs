using System.IO;

namespace ServerApi
{
    public interface IJsonSerializer
    {
        TResult Deserialize<TResult>(Stream stream);
        Stream Serialize(object data);
    }
}