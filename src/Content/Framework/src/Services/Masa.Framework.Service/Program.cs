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
               .UseUoW<ShopDbContext>(dbOptions => dbOptions.UseSqlite("DataSource=:memory:"))
               .UseEventLog<ShopDbContext>()
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
               .UseUoW<ShopDbContext>(dbOptions => dbOptions.UseSqlite("DataSource=:memory:"))
               .UseEventLog<ShopDbContext>();
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
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
#endif
var app = builder.Build();
// Configure the HTTP request pipeline.
#if (EnableOpenAPI)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endif

app.UseHttpsRedirection();
app.UseRouting();

#if (AddAuthorize)
app.UseAuthentication();
app.UseAuthorization();
#endif

#if (UseDapr)
app.UseCloudEvents();
app.UseEndpoints(endpoints =>
{
    endpoints.MapSubscribeHandler();
#if (AddActor)
    endpoints.MapActorsHandlers();
#endif
});
#endif

app.MapControllers();

app.Run();