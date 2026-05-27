using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using BarberShop.Models;
using BarberShop.Services;

namespace BarberShop.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthService _authService;
        private AuthUser? _currentUser;

        public CustomAuthenticationStateProvider(IAuthService authService)
        {
            _authService = authService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            _currentUser = await _authService.GetCurrentUserAsync();

            if (_currentUser == null)
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
                return new AuthenticationState(anonymousPrincipal);
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, _currentUser.RoleId.ToString()),
                new Claim(ClaimTypes.Name, _currentUser.RoleName),
                new Claim(ClaimTypes.Role, _currentUser.RoleName),
                new Claim("Description", _currentUser.Description),
                new Claim("LoginTime", _currentUser.LoginTime.ToString())
            }, "CustomAuth");

            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);
        }

        public async Task LoginAsync(string roleName, string password)
        {
            var (success, message, user) = await _authService.LoginAsync(roleName, password);
            if (success && user != null)
            {
                _currentUser = user;
                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            }
            else
            {
                throw new Exception(message);
            }
        }

        public async Task LogoutAsync()
        {
            await _authService.LogoutAsync();
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}

