using BlazorMaui.Shared.Services;
using BlazorMaui.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSharedMasaBlazor();

// Add device-specific services used by the BlazorMaui.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
