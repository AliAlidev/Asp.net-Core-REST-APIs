using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;
using System.Linq;
using System.Security.Principal;
using Asp.net_Core_REST_APIs.Dtos;

namespace Catalog.Controllers
{
    [ApiController]
    // define route name set name as controller name or we can write it explicity [Route("[Items]")]
    [Route("[Controller]")]
    public class ItemsController : ControllerBase
    {
        /*
            we define un interfce in order to use dependincy injection technique 
            define property of type new interface
            initilize this property from value passed as constructor parameter
            in this technique we don't know witch dependency implement the interface
            after that we need to register our interface and implementations
            go to startup.cs and go to section ConfigureSercices in this section we configure all services
            we add
        */
        private readonly IInMemItesmRepository repository;
        public ItemsController(IInMemItesmRepository repository)
        {
            this.repository = repository;
        }

        /*
            add HttpGet tag in order to link this method with a specific route
            default route is Get /Items
            if we write HttpGet("Name") then route will be /Items/Name and so on
        */
        // GET /Items
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return repository.GetItems().Select(item => item.AsDto());
        }

        /*
            ActionResult allow us to send response type like method NotFound for example not only types
            as we noramly do
            if item is null then method will return response with status code 204 that mean success but
            there is no data so we want to change response status code to 404 by using method NotFound()
        */
        // GET /Items/{id}
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item == null)
                // status code 404
                return NotFound();

            return Ok(item.AsDto());
        }

        // POST /items
        [HttpPost]
        public ActionResult<ItemDto> CreateItem(CreateItemDto itemDto)
        {
            Item item = new()
            {
                id = Guid.NewGuid(),
                Name = itemDto.Name,
                Price = itemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            repository.CreateItem(item);

            /*
                create action with status code 201 (record created) witch require 3 parameters:
                1- name of method/action we want to call
                2- method/action parameters
                3- returned object
            */
            return CreatedAtAction(nameof(GetItem), new { id = item.id }, item.AsDto());
        }

        /*
        */
        // PUT /Items/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateItem(Guid id, UpdateItemDto itemDto)
        {
            Item existItem = repository.GetItem(id);
            if (existItem is null)
            {
                return NotFound();
            }

            Item updateItem = existItem with
            {
                Name = itemDto.Name,
                Price = itemDto.Price
            };
            repository.UpdateItem(updateItem);
            // status code 204
            return NoContent();

        }

        // DELETE /Items/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteItem(Guid id)
        {
            var existenceItem = repository.GetItem(id);
            if (existenceItem is null)
            {
                return NotFound();
            }

            repository.DeleteItem(id);
            return NoContent();
        }
    }
}