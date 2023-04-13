using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorProWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Services.AddGlobalForWasmAsync(builder.HostEnvironment.BaseAddress);

await builder.Services
             .AddMasaBlazor(builder =>
             {
                 builder.ConfigureTheme(theme =>
                 {
                     theme.Themes.Light.Primary = "#4318FF";
                     theme.Themes.Light.Accent = "#4318FF";
                 });
             })
             .AddI18nForWasmAsync(Path.Combine(builder.HostEnvironment.BaseAddress, "i18n"));

await builder.Build().RunAsync();
