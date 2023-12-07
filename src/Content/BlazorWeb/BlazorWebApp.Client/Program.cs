using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

#if (!InteractiveAtRoot)
builder.Services.AddMasaBlazor(options =>
{
    options.ConfigureSSR(_ => {});
});
#else
builder.Services.AddMasaBlazor();
#endif

await builder.Build().RunAsync();
