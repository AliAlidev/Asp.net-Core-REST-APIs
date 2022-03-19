using System.ComponentModel.DataAnnotations;

namespace Asp.net_Core_REST_APIs.Dtos
{
    public record UpdateItemDto
    {

        /*
            we can add data annotations:
            - Required in order to make this field required
            - Range in order to make this field value between this two values
        */
        [Required]
        public string Name { get; init; }
        [Required]
        [Range(1, 10000)]
        public decimal Price { get; init; }

    }
}