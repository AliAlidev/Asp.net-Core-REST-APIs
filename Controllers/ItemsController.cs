using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Catalog.Repositories;
using Catalog.Entities;
using Catalog.Dtos;
using System.Linq;
using System.Security.Principal;

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
        [HttpGet]
        public IEnumerable<ItemDto> GetItems()
        {
            return repository.GetItems().Select(item => item.AsDto());
        }

        /*
            /Items/{id}
            ActionResult allow us to send response type like method NotFound for example not only types
            as we noramly do
            if item is null then method will return response with status code 204 that mean success but
            there is no data so we want to change response status code to 404 by using method NotFound()
        */
        [HttpGet("{id}")]
        public ActionResult<ItemDto> GetItem(Guid id)
        {
            var item = repository.GetItem(id);
            if (item == null)
                return NotFound();

            return Ok(item.AsDto());
        }
    }
}