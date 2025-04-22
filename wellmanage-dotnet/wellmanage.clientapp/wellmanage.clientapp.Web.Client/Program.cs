using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using wellmanage.clientapp.Shared.Services;
using wellmanage.clientapp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the wellmanage.clientapp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
