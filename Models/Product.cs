using System.ComponentModel.DataAnnotations;

namespace product.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be zero or positive.")]
        public decimal Price { get; set; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be zero or positive.")]
        public int QuantityInStock { get; set; }
        public CategoryEnum? Category { get; set; }


        //public string? Photo { get; set; }
    }

    public enum CategoryEnum
    {
        electronics,
        books,
        clothing,
        beauty,
        jewelry,
        tools
    }
}
