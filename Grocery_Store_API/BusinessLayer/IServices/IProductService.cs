using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTOModel>> GetAllProductsAsync();
        public Task<ProductDTOModel> AddProductAsync(ProductDTOModel productRequest);

        public Task<ProductDTOModel> GetProductByIdAsync(Guid id);

        public Task<ProductDTOModel> DeleteProduct(Guid id);

        public Task<ProductDTOModel> UpdateProduct(ProductDTOModel productRequest);

        public Task<ReviewDTOModel> AddReview(ReviewDTOModel review);

        public Task<IEnumerable<ReviewDTOModel>> GetAllReviewsAsync(Guid productId);
    }
}
