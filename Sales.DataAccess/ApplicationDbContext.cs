using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sales.Models;

namespace on_sales.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }
        public DbSet<PolicyType> PolicyType { get; set; }
        public DbSet<PolicyItems> PolicyItems { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<OrderHeader> OrderHeader { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
    }
}
