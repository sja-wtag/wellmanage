using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.clientapp.Shared.Services;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Interceptor
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly IAppStorage _appStorage;
        private readonly NavigationManager _navigation;
        private readonly JwtAuthStateProvider _authStateProvider;

        public AuthorizationHandler(IAppStorage appStorage, AuthenticationStateProvider authStateProvider)
        {
            _appStorage = appStorage;
            _authStateProvider = (JwtAuthStateProvider)authStateProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var jsonState = await _appStorage.GetAsync("sessionState");
            if(jsonState != null)
            {
                var authUser = System.Text.Json.JsonSerializer.Deserialize<AuthenticatedUser>(jsonState);
                var token = authUser.AuthenticationToken;

                if (!string.IsNullOrEmpty(token))
                {
                    // Set the Authorization header
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                }
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await _authStateProvider.MarkUserAsLoggedOut();
                _navigation.NavigateTo("/login", true);
            }

            return response;
        }
    }

}
