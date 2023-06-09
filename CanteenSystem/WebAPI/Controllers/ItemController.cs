﻿using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Dtos.IngredientDto;
using Shared.Model;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemLogic itemLogic;

    public ItemController(IItemLogic itemLogic)
    {
        this.itemLogic = itemLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Item>> CreateAsync(ItemCreationDto dto)
    {
        try
        {
            Item item = await itemLogic.CreateAsync(dto);
            return Created($"/items/{item.Id}", item);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAllItemsAsync([FromQuery] string? name)
    {
        try
        {
            SearchItemSto parameters = new(name,null);
            var items = await itemLogic.GetAllItemsAsync(parameters);
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }

    }

    
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await itemLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
   
    [HttpGet, Route("/items/{id:int}")]
    public async Task<ActionResult<ItemBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            ItemBasicDto result = (await itemLogic.GetByIdAsync(id))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet, Route("/item/{name}")]
    public async Task<ActionResult<ItemBasicDto>> GetByName([FromQuery] string name)
    {
        try
        {
            ItemBasicDto result = (await itemLogic.GetByNameAsync(name))!;
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
    
    
}