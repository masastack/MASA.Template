var builder = WebApplication.CreateBuilder(args);

#if (UseDapr)
// If this service does not need to call other services, you can delete the following line.
builder.Services.AddDaprClient();
#endif
#if (AddActor)
builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<OrderActor>();
});
#endif
#if (AddAuthorize)
builder.Services
    .AddAuthorization()
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = "";
        options.RequireHttpsMetadata = false;
        options.Audience = "";
    });
#endif
#if(!HasDdd)
builder.Services.AddAutoInject();
#endif
var app = builder.Services
#if (EnableOpenAPI)
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    .AddEndpointsApiExplorer()
#if (AddAuthorize)
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer xxxxxxxxxxxxxxx\"",
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    })
#else
    .AddSwaggerGen()
#endif
#endif
#if (UseFluentValidation)
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    })
#endif
#if (HasDdd)
    .AddDomainEventBus(dispatcherOptions =>
    {
        dispatcherOptions
            .UseIntegrationEventBus<IntegrationEventLogService>(options => options.UseDapr().UseEventLog<ShopDbContext>())
            .UseEventBus(eventBusBuilder =>
            {
#if (UseFluentValidation)
                eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
                eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
            })
            .UseUoW<ShopDbContext>(dbOptions => dbOptions.UseSqlite("DataSource=:memory:"))
            .UseRepository<ShopDbContext>();
    })
#elif (UseCqrsMode)
    .AddIntegrationEventBus<IntegrationEventLogService>(options =>
    {
        options.UseDapr();
        options.UseEventBus(eventBusBuilder =>
                {
#if (UseFluentValidation)
                    eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
                    eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
                })
               .UseUoW<ShopDbContext>(dbOptions => dbOptions.UseSqlite("DataSource=:memory:"));

        options.UseEventLog<ShopDbContext>();
    })
#elif (UseBasicMode)
    .AddEventBus(eventBusBuilder =>
    {
#if (UseFluentValidation)
        eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
        eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
    })
    .AddMasaDbContext<ShopDbContext>(options =>
    {
        options.UseSqlite("DataSource=:memory:");
    })
#endif
    .AddServices(builder, option => option.MapHttpMethodsForUnmatched = new string[] { "Post" });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

#region MigrationDb
using var context = app.Services.CreateScope().ServiceProvider.GetService<ShopDbContext>();
{
    if (context!.GetService<IRelationalDatabaseCreator>().HasTables() == false)
    {
        context!.GetService<IRelationalDatabaseCreator>().CreateTables();
    }
}
#endregion

#if (AddAuthorize)
app.UseAuthentication();
app.UseAuthorization();

#endif
#if (UseDapr)
// Used for Dapr Pub/Sub.
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
#if (AddActor)
    // Used for Dapr Actor
    endpoints.MapActorsHandlers();
#endif
});
#endif
app.UseHttpsRedirection();

app.Run();