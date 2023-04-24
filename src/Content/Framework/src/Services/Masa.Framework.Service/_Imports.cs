#if (AddActor)
global using Dapr.Actors;
global using Dapr.Actors.Runtime;
global using Dapr.Actors.Client;
global using Masa.Framework.Service.Actors;
#endif
#if (UseFluentValidation)
global using FluentValidation.AspNetCore;
global using FluentValidation;
#endif
#if (UseControllers)
global using Microsoft.AspNetCore.Mvc;
#else
global using Masa.Contrib.Service.MinimalAPIs;
#endif
#if (AddAuthorize)
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.OpenApi.Models;
#endif
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.Events;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Storage;
global using Masa.Framework.Service.Infrastructure;
global using Masa.Framework.Service.Infrastructure.Entities;
global using Masa.Framework.Service.Infrastructure.Events;
global using Masa.Framework.Service.Infrastructure.Middleware;
global using Masa.Framework.Service.Infrastructure.Repositories;