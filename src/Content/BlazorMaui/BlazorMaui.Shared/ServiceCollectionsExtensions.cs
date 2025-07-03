namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionsExtensions
{
  public static IMasaBlazorBuilder AddSharedMasaBlazor(this IServiceCollection services)
  {
#if (mdi)
    return services
        .AddMasaBlazor()
        .AddMobileComponents();
#else
    return services
        .AddMasaBlazor(options =>
        {
#if (fa)
            options.ConfigureIcons(IconSet.FontAwesome6);
#else
            options.ConfigureIcons(IconSet.MaterialDesign);
#endif
        })
        .AddMobileComponents();
#endif
  }
}