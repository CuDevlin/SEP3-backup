﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.Dtos;
using Shared.Model;

namespace HttpClients.Implementations;

public class OrderHttpClient:IOrderService
{
    private readonly HttpClient client;

    public OrderHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<ICollection<Order>> getAllOrdersAsync(int? id, DateOnly? date, string? userName, string? completedStatus)
    {
        string query = ConstructQuery(id, date, userName, completedStatus);
        HttpResponseMessage response = await client.GetAsync("/Order"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Order> orders = JsonSerializer.Deserialize<ICollection<Order>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return orders;
    }

    private static string ConstructQuery(int? id, DateOnly? date, string? userName, string? completedStatus)
    {
        string query = "";
        if (id!=null)
        {
            query += $"?id={id}";
        }

        if (date.HasValue)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"date={date.Value}";
        }

        if (!string.IsNullOrEmpty(userName))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"userName={userName}";
        }

        if (!string.IsNullOrEmpty(completedStatus))
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"completedStatus={completedStatus}";
        }
        Console.WriteLine(query);
        return query;
    }

    public async Task<OrderCreationDto> GetOrderByIdAsync(int id)
    {
        HttpResponseMessage response = await client.GetAsync($"/orders/{id}");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        OrderCreationDto order = JsonSerializer.Deserialize<OrderCreationDto>(content, 
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }
        )!;
        return order;
    }

    public async Task UpdateAsync(OrderUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/orders", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        
    }

    public async Task DeleteAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"Orders/{id}");
    }

    public async Task CreateAsync(OrderCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/orders", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}