﻿using BlazorMaui.Services;
using BlazorMaui.Shared.Services;
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
        builder.Services.AddSharedMasaBlazor();

        // Add device-specific services used by the BlazorMaui.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

//-:cnd:noEmit
#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif
//+:cnd:noEmit

        return builder.Build();
    }
}
