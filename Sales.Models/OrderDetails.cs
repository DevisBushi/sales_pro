using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sales.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual OrderHeader OrderHeader { get; set; }

        public int PolicyItemsId { get; set; }

        [ForeignKey("PolicyItemsId")]
        public virtual PolicyItems PolicyItems { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }
    }
}
