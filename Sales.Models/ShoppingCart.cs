using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sales.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Count = 1;
        }
        public int Id { get; set; }
        public int PolicyItemsId { get; set; }
        [NotMapped]
        [ForeignKey("PolicyItemsId")]
        public virtual PolicyItems PolicyItems { get; set; }
        public string ApplicationUserId { get; set; }
        [NotMapped]
        [ForeignKey("ApplicationUserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Range(1,100, ErrorMessage = "Ju lutem Zgjidhni ndermjet 1 ose 100")]
        public int Count { get; set; }

    }
}
