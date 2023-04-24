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
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.Ddd.Domain.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Repositories;
global using Masa.BuildingBlocks.Ddd.Domain.Services;
global using Masa.BuildingBlocks.Ddd.Domain.Values;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.Dispatcher.IntegrationEvents;
global using Masa.Contrib.Data.UoW.EFCore;
global using Masa.Contrib.Ddd.Domain;
global using Masa.Contrib.Ddd.Domain.Repository.EFCore;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.Contrib.Dispatcher.Events.Enums;
global using Masa.Contrib.Dispatcher.IntegrationEvents;
global using Masa.Contrib.Dispatcher.IntegrationEvents.Dapr;
global using Masa.Contrib.Dispatcher.IntegrationEvents.EventLogs.EFCore;
global using Masa.Contrib.ReadWriteSplitting.Cqrs.Commands;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Storage;
global using Masa.Framework.Service.Domain.Events;
global using Masa.Framework.Service.Application.Orders.Commands;
global using Masa.Framework.Service.Application.Orders.Queries;
global using Masa.Framework.Service.Domain.Aggregates.Orders;
global using Masa.Framework.Service.Domain.Repositories;
global using Masa.Framework.Service.Domain.Services;
global using Masa.Framework.Service.Infrastructure;
global using Masa.Framework.Service.Infrastructure.Middleware;
