#if (UseWebAssembly && SampleContent)
using BlazorWebApp.Client.Pages;
#endif
using BlazorWebApp.Components;
#if (!mdi)
using Masa.Blazor;
#endif

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#if (!UseServer && !UseWebAssembly)
builder.Services.AddRazorComponents();
#else
builder.Services.AddRazorComponents()
    #if (UseServer && UseWebAssembly)
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
    #elif (UseServer)
    .AddInteractiveServerComponents();
    #elif (UseWebAssembly)
    .AddInteractiveWebAssemblyComponents();
    #endif
#endif

#if (!InteractiveAtRoot)
builder.Services.AddMasaBlazor(options =>
{
    #if (SampleContent)
    #if (md)
    options.ConfigureIcons(IconSet.MaterialDesign);
    #elif (fa)
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
#elif (mdi)
builder.Services.AddMasaBlazor();
#else
builder.Services.AddMasaBlazor(options =>
{
    #if (fa)
    options.ConfigureIcons(IconSet.FontAwesome);
    #else
    options.ConfigureIcons(IconSet.MaterialDesign);
    #endif
});
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.
#if (UseWebAssembly)
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
#else
if (!app.Environment.IsDevelopment())
#endif
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
#if (HasHttpsProfile)
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
#endif
}

#if (HasHttpsProfile)
app.UseHttpsRedirection();

#endif
app.UseStaticFiles();
app.UseAntiforgery();

#if (UseServer && UseWebAssembly)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
#elif (UseServer)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
#elif (UseWebAssembly)
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
#else
app.MapRazorComponents<App>();
#endif
#if (UseWebAssembly && SampleContent)
    .AddAdditionalAssemblies(typeof(Counter).Assembly);
#elif (UseWebAssembly)
    .AddAdditionalAssemblies(typeof(BlazorWebApp.Client._Imports).Assembly);
#endif

app.Run();
