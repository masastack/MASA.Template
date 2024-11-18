using BlazorMaui.Shared.Services;
using BlazorMaui.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

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

// Add device-specific services used by the BlazorMaui.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
