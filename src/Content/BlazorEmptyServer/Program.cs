using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
#if (!mdi)
using Masa.Blazor;
#endif

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
#if (mdi)
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

#if (HasHttpsProfile)
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
#endif

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
