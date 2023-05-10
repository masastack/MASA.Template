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
global using Masa.BuildingBlocks.Data.UoW;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
global using Masa.Contrib.Data.UoW.EFCore;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.Events.Enums;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Contrib.ReadWriteSplitting.Cqrs.Commands;
global using Masa.Contrib.ReadWriteSplitting.Cqrs.Queries;
global using Masa.Framework.Service.Application.Orders.Commands;
global using Masa.Framework.Service.Application.Orders.Queries;
global using Masa.Framework.Service.Infrastructure;
global using Masa.Framework.Service.Infrastructure.Entities;
global using Masa.Framework.Service.Infrastructure.Middleware;
global using Masa.Framework.Service.Infrastructure.Repositories;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Storage;