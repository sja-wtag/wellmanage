using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using wellmanage.clientapp.Shared.Services;
using wellmanage.clientapp.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the wellmanage.clientapp.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
