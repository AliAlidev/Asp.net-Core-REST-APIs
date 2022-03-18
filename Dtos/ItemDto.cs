namespace Catalog.Dtos
{
    /*
        Dto it contains the shape of reponse from the service to the end client
    */
    public record ItemDto
    {
        public Guid id { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreatedDate { get; init; }

    }
}