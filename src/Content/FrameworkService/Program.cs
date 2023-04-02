var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMasaMinimalAPIs(option => option.MapHttpMethodsForUnmatched = new string[] { "Post" });

#if (UseSwagger)
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MasaFrameworkServiceApp", Version = "v1", Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "MasaFrameworkServiceApp", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml")) options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    });
#endif

var app = builder.Build();

app.MapMasaMinimalAPIs();

#if (UseSwagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MasaFrameworkServiceApp"));
}
#endif

app.Run();
