using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Services
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;
        private readonly AuthenticationState _anonymous;

        public JwtAuthStateProvider(
            ILocalStorageService localStorage,
            NavigationManager navigationManager,
            IJSRuntime jsRuntime)
        {
            _localStorage = localStorage;
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (!IsJavaScriptAvailable())
            {
                return _anonymous;
            }

            try
            {
                var authModel = await _localStorage.GetItemAsync<AuthenticatedUser>("sessionState");
                var identity = authModel == null
                    ? new ClaimsIdentity()
                    : GetClaimsIdentity(authModel.AuthenticationToken);

                var user = new ClaimsPrincipal(identity);
                return new AuthenticationState(user);
            }
            catch
            {
                await MarkUserAsLoggedOut();
                return _anonymous;
            }
        }

        public async Task MarkUserAsAuthenticated(AuthenticatedUser authUser)
        {
            if (!IsJavaScriptAvailable())
            {
                return;
            }

            await _localStorage.SetItemAsync("sessionState", authUser);
            var identity = GetClaimsIdentity(authUser.AuthenticationToken);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            if (!IsJavaScriptAvailable())
            {
                return;
            }

            await _localStorage.RemoveItemAsync("sessionState");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        private bool IsJavaScriptAvailable()
        {
            try
            {
                // Attempt to cast to IJSInProcessRuntime – will fail during prerendering
                var _ = (IJSInProcessRuntime)_jsRuntime;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
