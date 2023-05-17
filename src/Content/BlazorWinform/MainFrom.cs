using Microsoft.AspNetCore.Components.WebView.WindowsForms;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWinform;

public partial class MainFrom : Form
{
    public MainFrom()
    {
        InitializeComponent();

        var services = new ServiceCollection();
        services.AddWindowsFormsBlazorWebView();
        services.AddMasaBlazor();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif

        blazorWebView1.HostPage = "wwwroot/index.html";
        blazorWebView1.Services = services.BuildServiceProvider();
        blazorWebView1.RootComponents.Add<App>("#app");
    }
}
