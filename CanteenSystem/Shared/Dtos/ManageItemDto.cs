﻿using Shared.Model;

namespace Shared.Dtos;

public class ManageItemDto
{
   

    public string name { get; }
    public int Id { get; }
    public int? ingredientId { get; set; }
    public ICollection<Ingredient> Ingredients { get; }
    public string? TitleContains { get;}
    
    public ManageItemDto(string name, int id, ICollection<Ingredient> ingridients)
    {
        this.name = name;
        Id = id;
        Ingredients = ingridients;

    }
    
    public ManageItemDto(string name, int id, string? titleContains)
    {
        this.name = name;
        Id = id;
        TitleContains = titleContains;
    }
   
}