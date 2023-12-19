using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#if (!InteractiveAtRoot)
builder.Services.AddMasaBlazor(options =>
{
    #if (SampleContent)
    options.ConfigureSsr(ssr =>
    {
        ssr.Left = 256;
        ssr.Top = 64;
    });
    #else
    options.ConfigureSsr();
    #endif
});
#else
builder.Services.AddMasaBlazor();
#endif

await builder.Build().RunAsync();
