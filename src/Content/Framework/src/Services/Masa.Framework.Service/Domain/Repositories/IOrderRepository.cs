﻿namespace Masa.Framework.Service.Domain.Repositories;


public interface IOrderRepository : IRepository<Order>
{
    Task<List<Order>> GetListAsync();
}