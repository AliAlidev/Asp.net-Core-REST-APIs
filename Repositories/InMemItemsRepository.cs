using Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Catalog.Repositories
{

    /*
        implement interface
    */
    class InMemItesmRepository : IInMemItesmRepository
    {
        private readonly List<Item> items = new()
        {
            new Item { id = Guid.NewGuid(), Name = "a1", Price = 100, CreatedDate = DateTimeOffset.UtcNow },
            new Item { id = Guid.NewGuid(), Name = "a2", Price = 200, CreatedDate = DateTimeOffset.UtcNow },
            new Item { id = Guid.NewGuid(), Name = "a3", Price = 300, CreatedDate = DateTimeOffset.UtcNow },
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(item => item.id == id).SingleOrDefault();
        }
    }
}