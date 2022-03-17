using System;

namespace Catalog.Entities
{
    public record Item
    {
        //////////////// init is a new accessor developed in dotnet 5 it allow to set value only when define instance and after creation we can't change this property
        /*
            Item i1 = new Item(){
                id = Guid.NewGuid();
            }                           // valid expression
            Item i1 = new Item();
            i1.id = Guid.NewGuid();     // wrong expression
        */
        public Guid id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}