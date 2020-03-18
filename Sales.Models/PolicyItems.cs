using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sales.Models
{
    public class PolicyItems
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = "Cmimi nuk mund te jete me pak se 1 ALL")]
        public double Price { get; set; }
        [Display(Name="Lloji i Kategorise")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [Display(Name = "Lloji i Polices")]
        public int PolicyTypeId { get; set; }
        [ForeignKey("PolicyTypeId")]
        public virtual PolicyType PolicyType { get; set; }
    }
}
   