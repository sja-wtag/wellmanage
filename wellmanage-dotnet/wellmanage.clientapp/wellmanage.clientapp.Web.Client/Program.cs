using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.clientapp.Shared.Services;
using wellmanage.clientapp.Web.Client.Services;
using wellmanage.clientapp.Web.Client.Services.Storage;


var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the wellmanage.clientapp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7270/api")
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAppStorage, WebAppStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
