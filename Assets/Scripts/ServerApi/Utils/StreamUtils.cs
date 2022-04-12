using System.IO;

namespace ServerApi.Utils
{
    public static class StreamUtils
    {
        public static string AsString(this Stream stream)
        {
            return new StreamReader(stream).ReadToEnd();
        }
        
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}