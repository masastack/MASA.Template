var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
builder.Services.AddControllers();

#if (EnableOpenAPI)
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
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
    });
#else
    .AddSwaggerGen();
#endif
#if (UseFluentValidation)
builder.Services
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssemblyContaining<Program>();
    });
#endif
#endif
builder.Services.AddMasaDbContext<ShopDbContext>(options =>
{
    options.UseSqlite("DataSource=masaApp.db");
});
#if (HasDdd)
builder.Services.AddDomainEventBus(dispatcherOptions =>
{
    dispatcherOptions.UseIntegrationEventBus<IntegrationEventLogService>(options => options.UseDapr().UseEventLog<ShopDbContext>())
        .UseEventBus(eventBusBuilder =>
        {
#if (UseFluentValidation)
            eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
            eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
        })
        .UseUoW<ShopDbContext>()
        .UseRepository<ShopDbContext>(); 
});
#elif (UseCqrsMode)
builder.Services.AddIntegrationEventBus<IntegrationEventLogService>(options =>
{
    options.UseDapr();
    options.UseEventBus(eventBusBuilder =>
            {
#if (UseFluentValidation)
                eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
                eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
            })
            .UseUoW<ShopDbContext>();

    options.UseEventLog<ShopDbContext>();
});
#elif (UseBasicMode)
builder.Services.AddEventBus(eventBusBuilder =>
{
#if (UseFluentValidation)
    eventBusBuilder.UseMiddleware(typeof(ValidatorMiddleware<>));
#endif
    eventBusBuilder.UseMiddleware(typeof(LogMiddleware<>));
});
#endif

#if(!HasDdd)
builder.Services.AddAutoInject();
#endif
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
#if (EnableOpenAPI)
    app.UseSwagger();
    app.UseSwaggerUI();
#endif

    #region MigrationDb
    using var context = app.Services.CreateScope().ServiceProvider.GetService<ShopDbContext>();
    {
        context!.Database.EnsureCreated();
    }
    #endregion
}

app.UseHttpsRedirection();
app.UseRouting();

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

app.MapControllers();

app.Run();