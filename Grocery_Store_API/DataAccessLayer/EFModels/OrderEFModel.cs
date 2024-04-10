using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class OrderEFModel
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
