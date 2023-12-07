#if (UseWebAssembly && SampleContent)
using BlazorWebApp.Client.Pages;
#endif
using BlazorWebApp.Components;

namespace BlazorWebApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        #if (!UseServer && !UseWebAssembly)
        builder.Services.AddRazorComponents();
        #else
        builder.Services.AddRazorComponents()
          #if (UseServer && UseWebAssembly)
            .AddInteractiveServerComponents()
            .AddInteractiveWebAssemblyComponents();
          #elif(UseServer)
            .AddInteractiveServerComponents();
          #elif(UseWebAssembly)
            .AddInteractiveWebAssemblyComponents();
          #endif
        #endif

        #if (!InteractiveAtRoot)
        builder.Services.AddMasaBlazor(options =>
        {
            options.ConfigureSSR(_ => {});
        });
        #else
        builder.Services.AddMasaBlazor();
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
            app.UseExceptionHandler("/Error");
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
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);
        #endif

        app.Run();
    }
}
