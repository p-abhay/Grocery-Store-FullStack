using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mapper
{
    public static class ReviewMapper
    {
        public static ReviewDTOModel ToDTO(this ReviewEFModel cart)
        {
            //if (user == null) return null;
            return new ReviewDTOModel
            {
                Id = cart.Id,
                ProductId = cart.ProductId,
                Author = cart.Author,
                Date = cart.Date,
                Review = cart.Review
            };
        }

        public static ReviewEFModel ToEFModel(this ReviewDTOModel cart)
        {
            //if (user == null) return null;
            return new ReviewEFModel
            {
                Id= cart.Id,
                ProductId= cart.ProductId,
                Author = cart.Author,
                Date = cart.Date,
                Review = cart.Review
            };
        }
    }
}
