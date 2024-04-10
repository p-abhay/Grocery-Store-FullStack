using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mapper
{
    public static class CartMapper
    {
        public static CartDTOModel ToDTO(this CartEFModel cart)
        {
            //if (user == null) return null;
            return new CartDTOModel
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
            };
        }

        public static CartEFModel ToEFModel(this CartDTOModel cart)
        {
            //if (user == null) return null;
            return new CartEFModel
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
            };
        }
    }
}
