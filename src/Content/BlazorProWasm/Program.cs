using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorProWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

using var httpclient = new HttpClient();
var navList = await httpclient.GetFromJsonAsync<List<NavModel>>(Path.Combine(builder.HostEnvironment.BaseAddress, $"nav/nav.json")) ?? throw new Exception("please configure the Navigation!");
builder.Services.AddNav(navList);
builder.Services.AddScoped<CookieStorage>();
builder.Services.AddScoped<GlobalConfig>();

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
