using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
#if (!mdi)
using Masa.Blazor;
#endif

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#if (!InteractiveAtRoot)
builder.Services.AddMasaBlazor(options =>
{
    #if (SampleContent)
    #if (md)
    options.ConfigureIcons(IconSet.MaterialDesign);
    #elif (fa)
    options.ConfigureIcons(IconSet.FontAwesome6);
    #endif
    options.ConfigureSsr(ssr =>
    {
        ssr.Left = 256;
        ssr.Top = 64;
    });
    #else
    options.ConfigureSsr();
    #endif
});
#elif (mdi)
builder.Services.AddMasaBlazor();
#else
    #if (fa)
    options.ConfigureIcons(IconSet.FontAwesome6);
    #else
    options.ConfigureIcons(IconSet.MaterialDesign);
    #endif
#endif

await builder.Build().RunAsync();
