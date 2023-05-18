using Rx.Blazor.Services.Base;

namespace Rx.Blazor.Services.Base
{
    public  class Client : IClient
    {
        public Client(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        public HttpClient HttpClient { get; }
    }
}
