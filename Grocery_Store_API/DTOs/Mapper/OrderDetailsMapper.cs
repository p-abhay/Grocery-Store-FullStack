using DataAccessLayer.EFModels;
using DataAccessLayer.Repository;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mapper
{
    public static class OrderDetailsMapper
    {
        public static OrderDetailsDTO ToDTO(this OrderDetails cart)
        {
            //if (user == null) return null;
            return new OrderDetailsDTO
            {
                OrderId = cart.OrderId,
                ProductImage = cart.ProductImage,
                ProductName = cart.ProductName,
                Date = cart.Date,
                Quantity = cart.Quantity
            };
        }
    }
}
