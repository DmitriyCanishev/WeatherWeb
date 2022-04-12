using System.IO;
using Newtonsoft.Json;

namespace ServerApi
{
    public class JsonSerializer : IJsonSerializer
    {
        private readonly Newtonsoft.Json.JsonSerializer _jsonSerializer = new Newtonsoft.Json.JsonSerializer();

        public TResult Deserialize<TResult>(Stream stream)
        {
            using (stream)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonTextWriter = new JsonTextReader(streamReader))
                    {
                        return _jsonSerializer.Deserialize<TResult>(jsonTextWriter);
                    }
                }
            }
        }

        public Stream Serialize(object data)
        {
            var sw = new StreamWriter(new MemoryStream()) {AutoFlush = true};
            JsonWriter writer = new JsonTextWriter(sw);
            _jsonSerializer.Serialize(writer, data);
            sw.BaseStream.Position = 0;
            return sw.BaseStream;
        }
    }
}