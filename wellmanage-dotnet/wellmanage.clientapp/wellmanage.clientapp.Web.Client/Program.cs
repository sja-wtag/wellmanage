using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using wellmanage.clientapp.Shared.Interceptor;
using wellmanage.clientapp.Shared.Interfaces;
using wellmanage.clientapp.Shared.Services;
using wellmanage.clientapp.Web.Client.Services;
using wellmanage.clientapp.Web.Client.Services.Storage;
using Microsoft.Extensions.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the wellmanage.clientapp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddMudServices();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<AuthorizationHandler>();

builder.Services.AddHttpClient("MyApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7270/api");
})
.AddHttpMessageHandler<AuthorizationHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("MyApi"));

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<IAppStorage, WebAppStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddScoped<AttendenceService>();
builder.Services.AddAuthorizationCore();

await builder.Build().RunAsync();
