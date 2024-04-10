using DataAccessLayer.EFModels;
using DTOs.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.Mapper
{
    public static class ProductMapper
    {
        public static ProductDTOModel ToDTO(this ProductEFModel product)
        {
            return new ProductDTOModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                AvailableQuantity = product.AvailableQuantity,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Discount = product.Discount,
                Specification = product.Specification
            };
        }

        public static ProductEFModel ToEFModel(this ProductDTOModel product)
        {
            return new ProductEFModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                AvailableQuantity = product.AvailableQuantity,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Discount = product.Discount,
                Specification = product.Specification
            };
        }

    }
}
