using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.shared.Models;

namespace wellmanage.clientapp.Shared.Interceptor
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly IAppStorage _appStorage;

        public AuthorizationHandler(IAppStorage appStorage)
        {
            _appStorage = appStorage;
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
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
