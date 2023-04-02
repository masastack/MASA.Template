namespace FrameworkService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEventBus()
            .AddMasaDbContext<MasaFrameworkServiceCqrsDbContext>(opt =>
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
            .AddMasaMinimalAPIs(option => option.MapHttpMethodsForUnmatched = new string[] { "Post" })
            .AddAutoInject();

#if (UseSwagger)
builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "MasaFrameworkServiceCqrsApp", Version = "v1", Contact = new Microsoft.OpenApi.Models.OpenApiContact { Name = "MasaFrameworkServiceCqrsApp", } });
        foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml")) options.IncludeXmlComments(item, true);
        options.DocInclusionPredicate((docName, action) => true);
    });
#endif

        var app = builder.Build();

        app.UseMasaExceptionHandler();

        app.MapMasaMinimalAPIs();

        #region MigrationDb
        using var context = app.Services.CreateScope().ServiceProvider.GetService<MasaFrameworkServiceCqrsDbContext>();
        {
            context!.Database.Migrate();
            if (context!.GetService<IRelationalDatabaseCreator>().HasTables() == false)
            {
                context!.GetService<IRelationalDatabaseCreator>().CreateTables();
            }
        }
        #endregion

#if (UseSwagger)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger().UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "MasaFrameworkServiceCqrsApp"));
}
#endif

        app.Run();
    }
}
