using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Repositories;
using System.Threading.Tasks;
using Play.Catalog.Service.Entities;

namespace  Play.Catalog.Service.Controllers 
{
    //https://localhost:5001/items this controller will handle the routes that go to items
    // Allows us to implement API methods
    [ApiController]
    [Route("items")]
    public class IItemsController: ControllerBase
    {
           private readonly IItemsRepository itemsRepository;

           public IItemsController(IItemsRepository itemsRepository)
           {
               this.itemsRepository = itemsRepository;
           }
           
            [HttpGet]
            public  async Task<IEnumerable<ItemDto>> getAsync(){
                        var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
                return items;
            }
          
          
            //GET /items/12345/ gets captured in the id and transfered into the parameter
                [HttpGet("{id}")]
            public async Task<ActionResult<ItemDto>> getByIdAsync(Guid id) 
            {
                    
                         var item = await itemsRepository.getAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
            }
                //POST / items
            [HttpPost]
            public async Task<ActionResult<ItemDto>> postAsync(CreateItemDto createItemDto) 
            {

                var item = new Item
                {
                    Name = createItemDto.name,
                    Description = createItemDto.description,
                    Price = createItemDto.Price,
                    CreatedDate = DateTimeOffset.UtcNow
                };

                await itemsRepository.createAsync(item);

        
                return CreatedAtAction(nameof(getByIdAsync), new { id = item.Id }, item);
            }


            [HttpPut("{id}")]
            //We use IActionResult because we dont want to return anything
            public async Task<IActionResult>  PutAsync(Guid id, UpdateItemDto updateItemDto){


                   var existingItem = await itemsRepository.getAsync(id);

                   if(existingItem == null) {
                       return NotFound();
                   }
                  existingItem.Name = updateItemDto.name;
                  existingItem.Description = updateItemDto.description;
                  existingItem.Price = updateItemDto.Price;

             await itemsRepository.updateAsync(existingItem);

                    return NoContent();
            }
                //DELETE /items/{id}
                [HttpDelete("{id}")]
            public  async Task<IActionResult>  Delete(Guid id){
                       var item = await itemsRepository.getAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.removeAsync(item.Id);
                     return NoContent();
            }
    }
}