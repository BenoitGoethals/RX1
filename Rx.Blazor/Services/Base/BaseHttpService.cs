using System.Net.Http.Headers;
using Blazored.LocalStorage;

namespace Rx.Blazor.Services.Base
{
    public class BaseHttpService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            this._client = client;
            this._localStorage = localStorage;
        }

        //protected Response<TGuid> ConvertApiExceptions<TGuid>(ApiException apiException)
        //{
        //    if(apiException.StatusCode == 400)
        //    {
        //        return new Response<TGuid>() { Message = "Validation errors have occured.", ValidationErrors = apiException.Response, Success = false };
        //    }
        //    if (apiException.StatusCode == 404)
        //    {
        //        return new Response<TGuid>() { Message = "The requested item could not be found.", Success = false };
        //    }
        //    if (apiException.StatusCode == 401)
        //    {
        //        return new Response<TGuid>() { Message = "Invalid Credentials, Please Try Again", Success = false };
        //    }

        //    if (apiException.StatusCode >= 200 && apiException.StatusCode <= 299)
        //    {
        //        return new Response<TGuid>() { Message = "Operation Reported Success", Success = true };
        //    }

        //    return new Response<TGuid>() { Message = "Something went wrong, please try again.", Success = false };
        //}

        protected async Task GetBearerToken()
        {
            var token = await _localStorage.GetItemAsync<string>("accessToken");
            if (token != null)
            {
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
