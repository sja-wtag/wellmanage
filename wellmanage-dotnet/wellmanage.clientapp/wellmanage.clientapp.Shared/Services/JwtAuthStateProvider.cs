using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Services
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAppStorage _storage;
        private readonly NavigationManager _navigationManager;
        private readonly AuthenticationState _anonymous;
        private AuthenticatedUser? _user = null;
        public JwtAuthStateProvider(
            IAppStorage storage,
            NavigationManager navigationManager)
        {
            _storage = storage;
            _navigationManager = navigationManager;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var authModel = await _storage.GetAsync("sessionState");
                if (string.IsNullOrWhiteSpace(authModel))
                    return _anonymous;

                var userData = System.Text.Json.JsonSerializer.Deserialize<AuthenticatedUser>(authModel);
                _user = userData;
                var identity = GetClaimsIdentity(userData?.AuthenticationToken);
                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
            catch
            {
                await MarkUserAsLoggedOut();
                return _anonymous;
            }
        }
        
        public async Task MarkUserAsAuthenticated(AuthenticatedUser authUser)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(authUser);
            await _storage.SetAsync("sessionState", json);

            var identity = GetClaimsIdentity(authUser.AuthenticationToken);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _storage.RemoveAsync("sessionState");
            _user = null;
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return new ClaimsIdentity();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var claims = jwtToken.Claims;
            return new ClaimsIdentity(claims, "jwt");
        }

        private async Task<AuthenticatedUser> GetAuthenticatedUserFromStorage()
        {
            var authModel = await _storage.GetAsync("sessionState");
            var userData = System.Text.Json.JsonSerializer.Deserialize<AuthenticatedUser>(authModel);
            return userData;
        }
        public async Task<AuthenticatedUser> GetAuthenticatedUser()
        {
            return _user ?? await GetAuthenticatedUserFromStorage();
        }
    }
}
