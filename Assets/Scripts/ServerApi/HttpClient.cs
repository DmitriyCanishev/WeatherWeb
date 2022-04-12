using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServerApi.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace ServerApi
{
    public class HttpClient : IHttpClient
    {
        private readonly IJsonSerializer _jsonSerializer = null;

        public HttpClient(IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
        }
        
        public async Task<TResult> Get<TResult>(Uri uri, CancellationTokenSource cancellationTokenSource = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                return await RunHttpQuery<TResult>(webRequest);
            }
        }
        
        public async Task<TResult> Post<TResult>(Uri uri, object data, CancellationTokenSource cancellationTokenSource = null)
        {
            using (var webRequest = new UnityWebRequest(uri, "POST"))
            {
                var requestBody = _jsonSerializer.Serialize(data).AsString();
                if (Debug.isDebugBuild) Debug.Log("requestBody:" + requestBody);
                byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
                webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                return await RunHttpQuery<TResult>(webRequest);
            }
        }

        private async Task<TResult> RunHttpQuery<TResult>(UnityWebRequest webRequest, CancellationTokenSource cancellationTokenSource = null)
        {
            var request = webRequest.SendWebRequest();

            cancellationTokenSource?.Token.Register(() =>
            {
                Debug.LogWarning("Http request was aborted");
                webRequest.Abort();
            });

            await request.ToTask();

            Exception finallyException = null;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"[{nameof(HttpClient)}] {webRequest.result}: {webRequest.error}");
                    finallyException = new Exception("Failed http request");
                    break;
                case UnityWebRequest.Result.Success:
                    var responseText = webRequest.downloadHandler.text;
                    Debug.Log($"[{nameof(HttpClient)}] {webRequest.result}: {responseText}");
                    try
                    {
                        using (var responseStream = StreamUtils.GenerateStreamFromString(responseText))
                        {
                            return _jsonSerializer.Deserialize<TResult>(responseStream);
                        }
                    }
                    catch (Exception exception)
                    {
                        finallyException = new Exception("Exception during deserializing response", exception);
                        break;
                    }
            }

            throw finallyException;
        }
    }
}
