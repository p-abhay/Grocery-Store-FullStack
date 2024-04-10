using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EFModels
{
    public class ReviewEFModel
    {
        [Key] public int Id { get; set; }
        public Guid ProductId { get; set; }
        public string Review { get; set; }
        public string Author { get; set; }
        public string Date { get; set; }

    }
}
