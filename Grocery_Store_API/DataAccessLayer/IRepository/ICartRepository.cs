using DataAccessLayer.EFModels;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICartRepository
    {
        public Task<IEnumerable<ProductEFModel>> ViewCartAsync(Guid userId);
        public Task<CartEFModel> AddToCartAsync(CartEFModel cart);
        public Task<CartEFModel> RemoveFromCartAsync(CartEFModel cart);

        public Task<string> PlaceOrder(Guid orderId, Guid userId);

        public Task<string> SaveOrderDetails(Guid orderId, List<ProductEFModel> products);

        public Task<List<OrderDetails>> MyOrders(Guid userId);

    }
}
