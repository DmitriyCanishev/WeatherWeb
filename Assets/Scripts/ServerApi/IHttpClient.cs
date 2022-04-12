using System;
using System.Threading;
using System.Threading.Tasks;

namespace ServerApi
{
    public interface IHttpClient
    {
        Task<TResult> Get<TResult>(Uri uri, CancellationTokenSource cancellationTokenSource = null);

        Task<TResult> Post<TResult>(Uri uri, object data, CancellationTokenSource cancellationTokenSource = null);
    }
}