using DataAccessLayer.EFModels;

namespace DataAccessLayer.IRepository
{
    public interface IProductRepository
    {
        public Task<IEnumerable<ProductEFModel>> GetAllProductsAsync();
        public Task<ProductEFModel> AddProductAsync(ProductEFModel product);

        public Task<ProductEFModel> GetProductByIdAsync(Guid productId);

        public Task<ProductEFModel> DeleteProduct(Guid productId);

        public Task<ProductEFModel> UpdateProduct(ProductEFModel product);

        public Task UpdateQuantity(Guid productId, int qty);

        public Task<IEnumerable<ReviewEFModel>> GetReviewsByProductId(Guid productId);
        public Task<ReviewEFModel> AddReview(ReviewEFModel review);
    }
}
