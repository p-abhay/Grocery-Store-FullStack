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
    public class CartRepository : ICartRepository
    {
        private readonly ProductDbContext _productDbContext;
        private readonly IProductRepository _productRepository;

        public CartRepository(ProductDbContext productDbContext,IProductRepository productRepository)
        {
            _productDbContext = productDbContext;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductEFModel>> ViewCartAsync(Guid userId)
        {
            var products = from cart in _productDbContext.Carts
                           join product in _productDbContext.Products on cart.ProductId equals product.Id
                           where cart.UserId == userId
                           select new ProductEFModel { Id = product.Id, Name = product.Name, Description = product.Description, AvailableQuantity = cart.Quantity,Category = product.Category,Price = product.Price,
                           ImageUrl = product.ImageUrl,Discount = product.Discount,Specification=product.Specification};

            return await products.ToListAsync();
        }

        public async Task<CartEFModel> AddToCartAsync(CartEFModel cart)
        {
            await _productDbContext.Carts.AddAsync(cart);
            await _productDbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<CartEFModel> GetCartById(Guid userId,Guid productId)
        {
            var cart = await _productDbContext.Carts.Where(c => (c.UserId == userId && c.ProductId == productId)).FirstOrDefaultAsync();
            if (cart == null) 
            {
                return null;
            }
            return cart;
        }

        public async Task<CartEFModel> UpdateCart(CartEFModel cart)
        {
            var updatedCart = await GetCartById(cart.UserId,cart.ProductId);
            //Console.WriteLine(updatedCart);
            updatedCart.Quantity += cart.Quantity;
            //_productDbContext.Carts.Update(updatedCart);
            //await _productDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<CartEFModel> RemoveFromCartAsync(CartEFModel cart)
        {
            var removeCart = await GetCartById(cart.UserId,cart.ProductId);
            _productDbContext.Carts.Remove(removeCart);
            _productDbContext.SaveChanges();
            return removeCart;
        }

        public async Task<string> PlaceOrder(Guid orderId, Guid userId)
        {
            var order = new OrderEFModel
            {
                OrderId = orderId,
                UserId = userId,
                Status = "Confirmed",
                Date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")

            };
            await _productDbContext.Orders.AddAsync(order);
            return "Order Placed";
        }

        public async Task<string> SaveOrderDetails(Guid orderId, List<ProductEFModel> products)
        {
            foreach(var product in products)
            {
                var orderDetails = new OrderDetailsEFModel
                {
                    OrderId = orderId,
                    ProductId = product.Id,
                    Quantity = product.AvailableQuantity // quantity of item that is ordered 
                    
                };
                //await _productRepository.UpdateProduct(product);
                await _productRepository.UpdateQuantity(product.Id,product.AvailableQuantity);
                await _productDbContext.OrderDetails.AddAsync(orderDetails);
                await _productDbContext.SaveChangesAsync();
            }
            return "Save Successful";
        }


        public async Task<List<OrderDetails>> MyOrders(Guid userId)
        {
            var products = await (from orders in _productDbContext.Orders
                                  join orderDetails in _productDbContext.OrderDetails on orders.OrderId equals orderDetails.OrderId
                                  join product in _productDbContext.Products on orderDetails.ProductId equals product.Id
                                  where orders.UserId == userId
                                  select new OrderDetails
                                  {
                                      OrderId = orders.OrderId,
                                      ProductName = product.Name,
                                      ProductImage = product.ImageUrl,
                                      Quantity = orderDetails.Quantity,
                                      Date = orders.Date
                                  }).ToListAsync();
            return products;
        }


        /*public async Task MyOrders(Guid userId)
        {
            var products = from orders in _productDbContext.Orders
                           join orderDetails in _productDbContext.OrderDetails on orders.OrderId equals orderDetails.OrderId
                           join products in _productDbContext.Products on orderDetails.ProductId equals products.Id
                           //where cart.UserId == userId
                           select new 
                           {
                               OrderId = orders.OrderId,
                               ProductName = products.Name,
                               ProductImage = products.ImageUrl,
                               Quantity = orderDetails.Quantity,
                               Date = orders.Date
                           };
            return products;
        }
*/
        /*public Task<IEnumerable<ProductEFModel>> ViewCart(Guid userId)
        {
            var products = from cart in _productDbContext.Carts
                           join product in _productDbContext.Products on cart.ProductId equals product.Id
                           where cart.UserId == userId
                           select new { product.Id, product.Name, product.Specification, cart.Quantity };

            return products.ToList();
        }*/
    }
}
