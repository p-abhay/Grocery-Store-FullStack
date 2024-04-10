using BusinessLayer.IServices;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using DTOs.DTOModels;
using DTOs.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTOModel> AddProductAsync(ProductDTOModel productRequest)
        {
            var productEF = ProductMapper.ToEFModel(productRequest);
            var returnProduct = await _productRepository.AddProductAsync(productEF);
            return ProductMapper.ToDTO(returnProduct);
        }

        public async Task<ProductDTOModel> DeleteProduct(Guid id)
        {
            var product = await _productRepository.DeleteProduct(id);
            return ProductMapper.ToDTO(product);
        }

        public async Task<IEnumerable<ProductDTOModel>> GetAllProductsAsync()
        {
            var products =  await _productRepository.GetAllProductsAsync();
            var res = products.Select(p => ProductMapper.ToDTO(p));
            return res;
        }

        public async Task<ProductDTOModel> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return ProductMapper.ToDTO(product);
        }

        public async Task<ProductDTOModel> UpdateProduct(ProductDTOModel productRequest)
        {
            var productEF = ProductMapper.ToEFModel(productRequest);
            var updated = await _productRepository.UpdateProduct(productEF);
            return ProductMapper.ToDTO(updated);
        }

        public async Task<ReviewDTOModel> AddReview(ReviewDTOModel review)
        {
            var reviewEF = ReviewMapper.ToEFModel(review);
            var addedReview = await _productRepository.AddReview(reviewEF);
            return ReviewMapper.ToDTO(addedReview);

        }

        public async Task<IEnumerable<ReviewDTOModel>> GetAllReviewsAsync(Guid productId)
        {
            var allReviews = await _productRepository.GetReviewsByProductId(productId);
            var allReviewsDTO = allReviews.Select(r => ReviewMapper.ToDTO(r));

            return allReviewsDTO;
        }
    }
}
