﻿using Shared.Dtos;
using Shared.Model;

namespace HttpClients.ClientInterfaces;

public interface IOrderService
{
    Task<ICollection<Order>> getAllOrdersAsync(int? userId, DateOnly? date, string? userName, string? completedStatus);
    Task<OrderCreationDto> GetOrderByIdAsync(int id);
    Task UpdateAsync(OrderUpdateDto dto);
    Task CreateAsync(OrderCreationDto dto);
    Task DeleteAsync(int id);

}