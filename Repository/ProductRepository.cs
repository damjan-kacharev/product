using Microsoft.EntityFrameworkCore;
using product.Database;
using product.Models;
using product.Requests;
using product.Responses;

namespace product.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }
        public void CreateProduct(CreateProductRequest createProduct)
        {
            var product = new Product();

            product.Name = createProduct.name;
            product.Description = createProduct.description;
            product.Price = createProduct.price;
            product.QuantityInStock = createProduct.quantityInStock;

            if (Enum.TryParse(createProduct.category, out CategoryEnum parsedCategory))
            {
                product.Category = parsedCategory;
            }

            _productDbContext.Products.Add(product);
            _productDbContext.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var productFromDb = _productDbContext.Products.Where(x => x.Id.Equals(id)).FirstOrDefault();
            _productDbContext.Products.Remove(productFromDb);
            _productDbContext.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            var product = _productDbContext.Products.Where(x => x.Id.Equals(id)).FirstOrDefault();
            return product;
        }

        public ProductResponse GetProducts(string productData, string sortBy, string sortOrder, int? pageSize, int? page)
        {
            int actualPageSize = pageSize.HasValue && pageSize > 0 ? pageSize.Value : 10;
            int actualPage = page.HasValue && page > 0 ? page.Value : 1;
            string actualSortBy = string.IsNullOrWhiteSpace(sortBy) ? "Id" : sortBy;
            string actualSortOrder = string.IsNullOrWhiteSpace(sortOrder) ? "asc" : sortOrder.ToLower();

            IQueryable<Product> query = _productDbContext.Products;

            if (!string.IsNullOrWhiteSpace(productData))
            {
                query = query.Where(p => p.Name.Contains(productData));
            }

            int totalCount = query.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)actualPageSize);

            if (actualSortOrder == "asc")
            {
                query = query.OrderBy(p => EF.Property<object>(p, actualSortBy));
            }
            else
            {
                query = query.OrderByDescending(p => EF.Property<object>(p, actualSortBy));
            }

            var productsPage = query
                .Skip((actualPage - 1) * actualPageSize)
                .Take(actualPageSize)
                .ToList();


            ProductResponse productResponse = new ProductResponse
            {
                totalCount = totalCount,
                pageSize = actualPageSize,
                Products = productsPage,
                totalPages = totalPages,
                page = actualPage,
                sortBy = actualSortBy,
                sortOrder = actualSortOrder,
            };

            return productResponse;
        }

        public bool UpdateProduct(int id, CreateProductRequest updateProduct)
        {
            var productFromDb = _productDbContext.Products.Where(x => x.Id.Equals(id)).FirstOrDefault();

            if (productFromDb == null) { return false; }

            productFromDb.Name = updateProduct.name;
            productFromDb.Description = updateProduct.description;
            productFromDb.Price = updateProduct.price;
            productFromDb.QuantityInStock = updateProduct.quantityInStock;

            if (Enum.TryParse(updateProduct.category, out CategoryEnum parsedCategory))
            {
                productFromDb.Category = parsedCategory;
            }

            _productDbContext.SaveChanges();

            return true;
        }
    }
}
