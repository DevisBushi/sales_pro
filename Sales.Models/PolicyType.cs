using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sales.Models
{
    public class PolicyType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name="Lloji i Polices")]
        public string Name { get; set; }
    }
}
