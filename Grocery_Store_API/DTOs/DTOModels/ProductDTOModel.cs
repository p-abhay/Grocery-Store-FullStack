using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOModels
{
    public class ProductDTOModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Product Name must be alphanumeric.")]
        public string Name { get; set; }

        [Required]
        [MaxLength(255)]
        [RegularExpression("^[a-zA-Z0-9 -.]*$", ErrorMessage = "Description must be alphanumeric.")]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z0-9 -]*$", ErrorMessage = "Category must be alphanumeric.")]
        public string Category { get; set; }

        [Required]
        public int AvailableQuantity { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal? Discount { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [MaxLength(100)]
        [RegularExpression("^[a-zA-Z0-9 -.]*$", ErrorMessage = "Specification must be alphanumeric.")]
        public string Specification { get; set; }
    }
}
