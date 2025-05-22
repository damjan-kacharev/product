using product.Models;

namespace product.Responses
{
    public class ProductResponse
    {
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int page { get; set; }
        public int totalPages { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }
        public List<Product> Products { get; set; }
    }
}
