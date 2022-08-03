using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasaBlazor().AddI18nForServer("wwwroot/locale");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseRequestLocalization(opts =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("zh-CN"),
        new CultureInfo("en-US")
    };

    opts.SupportedCultures = supportedCultures;
    opts.SupportedUICultures = supportedCultures;
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();