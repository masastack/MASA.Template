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
    #elseif (fa)
    options.ConfigureIcons(IconSet.FontAwesome);
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
#elseif (mdi)
builder.Services.AddMasaBlazor();
#else
    #if (fa)
    options.ConfigureIcons(IconSet.FontAwesome);
    #else
    options.ConfigureIcons(IconSet.MaterialDesign);
    #endif
#endif

await builder.Build().RunAsync();
