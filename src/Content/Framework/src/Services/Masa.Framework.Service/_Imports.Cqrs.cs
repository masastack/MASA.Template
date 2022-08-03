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
global using Masa.BuildingBlocks.ReadWriteSpliting.Cqrs.Commands;
global using Masa.BuildingBlocks.ReadWriteSpliting.Cqrs.Queries;
global using Masa.Contrib.Data.EntityFrameworkCore;
global using Masa.Contrib.Data.UoW.EF;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.Events.Enums;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EF;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Commands;
global using Masa.Contrib.ReadWriteSpliting.Cqrs.Queries;
global using Microsoft.EntityFrameworkCore;
global using Masa.Framework.Service.Application.Orders.Commands;
global using Masa.Framework.Service.Application.Orders.Queries;
global using Masa.Framework.Service.Infrastructure;
global using Masa.Framework.Service.Infrastructure.Entities;
global using Masa.Framework.Service.Infrastructure.Middleware;
global using Masa.Framework.Service.Infrastructure.Repositories;