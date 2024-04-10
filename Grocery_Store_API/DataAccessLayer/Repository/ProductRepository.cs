using DataAccessLayer.DbContexts;
using DataAccessLayer.EFModels;
using DataAccessLayer.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _productDbContext;

        public ProductRepository(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<ProductEFModel> AddProductAsync(ProductEFModel product)
        {
            await _productDbContext.Products.AddAsync(product);
            await _productDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<ProductEFModel> DeleteProduct(Guid productId)
        {
            var delete = await GetProductByIdAsync(productId);
            _productDbContext.Products.Remove(delete);
            await _productDbContext.SaveChangesAsync();
            return delete;
        }

        public async Task<IEnumerable<ProductEFModel>> GetAllProductsAsync()
        {
            return await _productDbContext.Products.ToListAsync();
        }

        public async Task<ProductEFModel> GetProductByIdAsync(Guid productId)
        {
            var product = await _productDbContext.Products.Where(x => x.Id == productId).FirstOrDefaultAsync();
            return product;
        }

        public async Task<ProductEFModel> UpdateProduct(ProductEFModel product)
        {
            //var toUpdate = await GetProductByIdAsync(product.Id);
            var toUpdate = product;
            _productDbContext.Products.Update(toUpdate);

            /*_productDbContext.Products.Remove(toUpdate);
            await _productDbContext.SaveChangesAsync();
            await _productDbContext.Products.AddAsync(toUpdate);*/
            await _productDbContext.SaveChangesAsync();
            return toUpdate;
        }

        public async Task UpdateQuantity(Guid productId, int qty)
        {
            var product = await GetProductByIdAsync(productId);
            product.AvailableQuantity -= qty;
            _productDbContext.Products.Update(product);
            await _productDbContext.SaveChangesAsync();
        }

        // For Reviews.
        public async Task<IEnumerable<ReviewEFModel>> GetReviewsByProductId(Guid productId)
        {
            var reviews = await _productDbContext.Reviews.Where(x => x.ProductId == productId).ToListAsync();

            return reviews;
        } 

        //Store reviews
        public async Task<ReviewEFModel> AddReview(ReviewEFModel review)
        {
            review.Date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            await _productDbContext.Reviews.AddAsync(review);
            await _productDbContext.SaveChangesAsync();
            return review;
        }
    }
}
