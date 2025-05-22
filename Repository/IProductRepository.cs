using product.Models;
using product.Requests;
using product.Responses;

namespace product.Repository
{
    public interface IProductRepository
    {
        public ProductResponse GetProducts(string productData, string sortBy, string sortOrder, int? pageSize, int? page);

        public Product GetProductById(int id);
        public void CreateProduct(CreateProductRequest product);
        public bool UpdateProduct(int id, CreateProductRequest product);
        public void DeleteProduct(int id);
    }
}