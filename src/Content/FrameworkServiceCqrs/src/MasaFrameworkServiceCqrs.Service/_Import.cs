global using Mapster;
global using Masa.Contrib.Dispatcher.Events;
global using Masa.BuildingBlocks.Ddd.Domain.Entities;
global using Masa.BuildingBlocks.Dispatcher.Events;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Commands;
global using Masa.BuildingBlocks.ReadWriteSplitting.Cqrs.Queries;
#if (!NoExample)
global using MasaFrameworkServiceCqrs.Contracts.Example;
global using MasaFrameworkServiceCqrs.Service.Application.Example.Commands;
global using MasaFrameworkServiceCqrs.Service.Application.Example.Queries;
#endif
global using MasaFrameworkServiceCqrs.Contracts;
global using MasaFrameworkServiceCqrs.Service.DataAccess;
global using Masa.Utils.Models;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.EntityFrameworkCore.Storage;
#if (UseSwagger)
global using Microsoft.OpenApi.Models;
#endif