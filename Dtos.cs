using System;
using System.ComponentModel.DataAnnotations;

namespace Play.Catalog.Service.Dtos 
{
public record ItemDto(Guid iD, string name, string description, decimal price, DateTimeOffset  createDate);
public record CreateItemDto([Required] string name, string description, [Range(0, 1000)] decimal Price);

public record UpdateItemDto([Required] string name, string description, [Range(0, 1000)] decimal Price);

}