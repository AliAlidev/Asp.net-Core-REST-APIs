using System.ComponentModel;
using Catalog.Dtos;
using Catalog.Entities;

namespace Catalog
{
    public static class Extensions
    {
        /*
            this means that type Item has method called AsDto and return object of type ItemDto
        */
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto
            {
                id = item.id,
                Name = item.Name,
                Price = item.Price,
                CreatedDate = item.CreatedDate
            };
        }
    }
}