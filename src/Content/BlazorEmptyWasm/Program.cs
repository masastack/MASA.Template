using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
#if (!mdi)
using Masa.Blazor;
#endif
using BlazorEmptyWasm;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

#if (mdi)
builder.Services.AddMasaBlazor();
#else
builder.Services.AddMasaBlazor(options =>
{
    #if (fa)
    options.ConfigureIcons(IconSet.FontAwesome6);
    #else
    options.ConfigureIcons(IconSet.MaterialDesign);
    #endif
});
#endif

await builder.Build().RunAsync();
