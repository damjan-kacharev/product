using Microsoft.AspNetCore.Mvc;
using product.Models;
using product.Repository;
using product.Requests;
using product.Responses;

namespace product.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository productRepository, ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<ProductResponse> GetProducts([FromQuery] string? productData, int? page, int? pageSize, string? sortBy, string? sortOrder)
        {
            return _productRepository.GetProducts(productData, sortBy, sortOrder, pageSize, page);
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] CreateProductRequest product)
        {
            _productRepository.CreateProduct(product);
            return Ok();
        }


        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById([FromRoute] int id)
        {
            var product = _productRepository.GetProductById(id);

            if (product == null) { return NotFound(); }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct([FromRoute] int id, [FromBody] CreateProductRequest product)
        {
            var update = _productRepository.UpdateProduct(id, product);

            if (!update) { return NotFound(); }

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct([FromRoute] int id)
        {
            _productRepository.DeleteProduct(id);
            return Ok();
        }

        //[HttpGet]
        //public ActionResult GetFilteredProducts([FromQuery] all needed parameters)
    }
}