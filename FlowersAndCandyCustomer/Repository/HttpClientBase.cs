using System;
using System.Net.Http;
using System.Threading;

namespace FlowersAndCandyCustomer.Repository
{
    public class HttpClientBase : HttpClient
    {
        private static readonly HttpClientBase _instance = new HttpClientBase();
        CancellationTokenSource cts;

        public HttpClientBase() : base()
        {
            TimeSpan time = new TimeSpan(0, 0, 60);
            Timeout = time;
            cts = new CancellationTokenSource();
            cts.CancelAfter(time);
        }

        public static HttpClientBase Instance
        {
            get
            {
                return _instance;
            }
        }


    }
}
