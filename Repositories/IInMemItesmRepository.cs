using System.Collections.Generic;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public interface IInMemItesmRepository
    {
         Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}