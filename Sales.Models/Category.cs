using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Sales.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display (Name="Kategoria e Produktit")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Numri Rendor")]
        public int DispOrder { get; set; }

    }
}
