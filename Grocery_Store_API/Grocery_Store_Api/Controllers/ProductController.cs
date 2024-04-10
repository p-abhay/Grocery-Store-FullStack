using BusinessLayer.IServices;
using DTOs.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store_Api.Controllers
{
    [ApiController]
    [Route("/api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        
        [Route("/getAllProducts")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products =  await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        
        [Route("/addProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTOModel productRequest)
        {
            productRequest.Id = Guid.NewGuid();
            await _productService.AddProductAsync(productRequest);
            return Ok(productRequest);
        }

        
        [Route("/getById")]
        [HttpPost]
        public async Task<IActionResult> GetProductById([FromBody] ProductId id)
        {
            var product = await _productService.GetProductByIdAsync(id.Id);
            return Ok(product);
        }

        
        [HttpDelete]
        [Route("/api/product/delete")]

        public async Task<IActionResult> DeleteProduct([FromBody] ProductDTOModel product)
        {
            var delete = await _productService.DeleteProduct(product.Id);
            return Ok(delete);
        }

        
        [HttpPut]
        [Route("/api/product/update")]

        public async Task<IActionResult> UpdateProduct([FromBody] ProductDTOModel product)
        {
            var updated = await _productService.UpdateProduct(product);
            return Ok(updated);
        }

        //For Review

        [HttpGet]
        [Route("/api/product/reviews")]
        public async Task<IActionResult> GetAllReviews([FromQuery] Guid productId)
        {
            var reviews = await _productService.GetAllReviewsAsync(productId);
            return Ok(reviews);
        }

        [HttpPost]
        [Route("/api/product/reviews/add")]

        public async Task<IActionResult> AddReview([FromBody]ReviewDTOModel review)
        {
            var addedReview = await _productService.AddReview(review);
            return Ok(addedReview);
        }
    }
}
