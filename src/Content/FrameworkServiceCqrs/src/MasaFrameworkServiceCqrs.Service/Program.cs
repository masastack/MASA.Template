var builder = WebApplication.CreateBuilder(args);

var app = builder.Services
#if (UseSwagger)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MasaFrameworkServiceCqrsApp", Version = "v1", Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "MasaFrameworkServiceCqrsApp", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml")) options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    })
#endif
    .AddEventBus()
    .AddMasaDbContext<ExampleDbContext>(opt =>
    {
#if (HasMSSQL)
        opt.UseSqlServer();
#elif (HasSqlite)
        opt.UseSqlite();
#elif (HasCosmos)
        opt.UseCosmos();
#elif (HasPostgreSql)
        opt.UseNpgsql();
#elif (HasPomeloMySql)
        opt.UseMySql(new MySqlServerVersion("5.7.26"));
#elif (HasMySql)
        opt.UseMySQL();
#elif (HasMemory)
        opt.UseInMemoryDatabase();
#elif (HasOracle)
        opt.UseOracle();
#endif
    })
    .AddAutoInject()
    .AddServices(builder, option => option.MapHttpMethodsForUnmatched = new string[] { "Post" });

app.UseMasaExceptionHandler();

if (app.Environment.IsDevelopment())
{
#if (UseSwagger)
    app.UseSwagger().UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MasaFrameworkServiceCqrsApp"));
#endif

    #region MigrationDb
    using var context = app.Services.CreateScope().ServiceProvider.GetService<ExampleDbContext>();
    {
        context!.Database.EnsureCreated();
    }
    #endregion
}

app.Run();
