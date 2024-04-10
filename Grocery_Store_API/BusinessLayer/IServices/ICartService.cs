using DataAccessLayer.Repository;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface ICartService
    {
        public Task<IEnumerable<ProductDTOModel>> ViewCartAsync(Guid userId);
        public Task<CartDTOModel> AddToCartAsync(CartDTOModel cart);
        public Task<CartDTOModel> RemoveFromCartAsync(CartDTOModel cart);

        public Task<string> PlaceOrder(Order order);
   
        public Task<List<OrderDetailsDTO>> MyOrders(Guid userId);
    }
}
