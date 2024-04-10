using BusinessLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using DTOs.DTOModels;
using DTOs.Mapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IEnumerable<ProductDTOModel>> ViewCartAsync(Guid userId)
        {
            var cart = await _cartRepository.ViewCartAsync(userId);
            var cartDTO = cart.Select(p => ProductMapper.ToDTO(p));
            return cartDTO;
        }
        
        public async Task<CartDTOModel> AddToCartAsync(CartDTOModel cart)
        {
            var cartEF = CartMapper.ToEFModel(cart);
            var res = await _cartRepository.AddToCartAsync(cartEF);
            return CartMapper.ToDTO(res);
        }

        public async Task<CartDTOModel> RemoveFromCartAsync(CartDTOModel cart)
        {
            var cartEF = CartMapper.ToEFModel(cart);
            var res = await _cartRepository.RemoveFromCartAsync(cartEF);
            return CartMapper.ToDTO(res);
        }

        public async Task<string> PlaceOrder(Order order)
        {
            var orderId = Guid.NewGuid();
            var productsDTO = order.Product.Select(p => ProductMapper.ToEFModel(p)).ToList();
            await _cartRepository.PlaceOrder(orderId,order.UserId);
            await _cartRepository.SaveOrderDetails(orderId, productsDTO);
            return "ok";
        }

        public async Task<List<OrderDetailsDTO>> MyOrders(Guid userId)
        {
            var orderDetails =  await _cartRepository.MyOrders(userId);
            var res = orderDetails.Select(o => OrderDetailsMapper.ToDTO(o)).ToList();

            return res;
        }
    }
}
