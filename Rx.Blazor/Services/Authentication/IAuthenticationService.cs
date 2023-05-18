using Rx.Blazor.Services.Base;

namespace Rx.Blazor.Services.Authentication
{
    public interface IAuthenticationService
    {
    //    Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginModel);
        public Task Logout();
    }
}
