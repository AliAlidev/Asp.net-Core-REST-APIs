using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Catalog.Repositories
{
    class InMemItesmRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { id = Guid.NewGuid(), Name = "a1", Price = 100, CreatedDate = DateTimeOffset.UtcNow },
            new Item { id = Guid.NewGuid(), Name = "a2", Price = 200, CreatedDate = DateTimeOffset.UtcNow },
            new Item { id = Guid.NewGuid(), Name = "a3", Price = 300, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> getItems(){
            return items;
        }

        public Item getItem(Guid id){
            return items.Where( item => item.id == id).SingleOrDefault();
        }
    }
}