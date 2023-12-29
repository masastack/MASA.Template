using BlazorMaui.Rcl;
#if (!mdi)
using Masa.Blazor;
#endif
using Microsoft.Extensions.Logging;

namespace BlazorMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
          .UseMauiApp<App>()
          .ConfigureFonts(fonts =>
          {
              fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
          });

        builder.Services.AddMauiBlazorWebView();
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

//-:cnd:noEmit
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
//+:cnd:noEmit

        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}
