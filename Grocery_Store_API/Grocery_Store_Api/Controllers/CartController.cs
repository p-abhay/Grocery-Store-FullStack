using BusinessLayer.IServices;
using DTOs.DTOModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Grocery_Store_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController (ICartService cartService)
        {
            _cartService = cartService;

        }

        [Authorize]
        
        [Route("/viewcart")]
        [HttpPost]
        public async Task<IActionResult> ViewCart([FromBody] CartDTOModel body)
        {
            try
            {
                var cart = await _cartService.ViewCartAsync(body.UserId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        
        [HttpPost]
        [Route("/cart/add")]
        public async Task<IActionResult> AddToCart([FromBody] CartDTOModel cart)
        {
            var res = await _cartService.AddToCartAsync(cart);
            return Ok(res);
        }

        [Authorize]
        [HttpDelete]
        [Route("/cart/delete")]
        public async Task<IActionResult> RemoveFromCart([FromBody] CartDTOModel cart)
        {
            var res = await _cartService.RemoveFromCartAsync(cart);
            return Ok(res);
        }

        [Authorize]
        [HttpPost]
        [Route("/cart/placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            await _cartService.PlaceOrder(order);
            return Ok("Order Confirmed");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> MyOrders([FromQuery] Guid userId)
        {
            var orders = await _cartService.MyOrders(userId);
            return Ok(orders);
        }
    }
}
