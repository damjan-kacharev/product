using product.Models;
using System.ComponentModel.DataAnnotations;

namespace product.Requests
{
    public class CreateProductRequest
    {
        public int id {  get; set; }
        public required string name { get; set; }
        public string? description { get; set; }
        public decimal price { get; set; }
        public int quantityInStock { get; set; }
        public string? category { get; set; }
    }
}
