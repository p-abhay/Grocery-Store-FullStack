using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.DTOModels
{
    public class Order
    {
        public Guid UserId { get; set; }
        public IEnumerable<ProductDTOModel> Product { get; set; }
    }
}
