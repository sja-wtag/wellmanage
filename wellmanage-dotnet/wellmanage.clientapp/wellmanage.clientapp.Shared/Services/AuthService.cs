using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.shared.Models;


namespace wellmanage.clientapp.Shared.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly JwtAuthStateProvider _authStateProvider;

        public AuthService(HttpClient http, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _localStorage = localStorage;
            _authStateProvider = (JwtAuthStateProvider)authStateProvider;
        }

        public async Task<AuthenticatedUser?> Login(LoginRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/v1/auth/login", request);
            var result = await response.Content.ReadFromJsonAsync<ApiResponse<AuthenticatedUser>>();
            var authToken = result?.ResponseData?.AuthenticationToken;
            if (string.IsNullOrEmpty(authToken))
            {
                return null;
            }
            await _authStateProvider.MarkUserAsAuthenticated(result?.ResponseData);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authToken);
            return result.ResponseData;
        }

        public async Task Register(RegistrationRequest request)
        {
            await _http.PostAsJsonAsync("api/auth/register", request);
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _authStateProvider.MarkUserAsLoggedOut();
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }

}
