using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using wellmanage.clientapp.Services;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.clientapp.Shared.Services;

namespace wellmanage.clientapp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Add device-specific services used by the wellmanage.clientapp.Shared project
            builder.Services.AddSingleton<IFormFactor, FormFactor>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            builder.Services.AddMudServices();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7270/api")
            });
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<IAppStorage, MauiAppStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
            builder.Services.AddAuthorizationCore();
            return builder.Build();
        }
    }
}
