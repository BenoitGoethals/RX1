using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

using Rx.Blazor.Services.Base;
using IClient = Rx.Blazor.Services.Base.IClient;

namespace Rx.Blazor.Services.Authentication
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) 
            : base(httpClient, localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        //public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel)
        //{
        //    Response<AuthResponse> response;
        //    try
        //    {
        //        var result = await _httpClient.LoginAsync(loginModel);
        //        response = new Response<AuthResponse>
        //        {
        //            Data = result,
        //            Success = true,
        //        };
        //        //Store Token
        //        await localStorage.SetItemAsync("accessToken", result.Token);

        //        //Change auth state of app
        //        await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();
        //    }
        //    catch (ApiException exception)
        //    {
        //        response = ConvertApiExceptions<AuthResponse>(exception);
        //    }

        //    return response;
        //}

        public async Task Logout()
        {
         //   await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }
    }

}
