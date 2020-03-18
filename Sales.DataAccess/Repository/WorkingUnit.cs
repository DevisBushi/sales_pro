using on_sales.DataAccess;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class WorkingUnit : IWorkingUnit
    {

        private readonly ApplicationDbContext _db;

        public WorkingUnit(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            PolicyType = new PolicyTypeRepository(_db);
            PolicyItems = new PolicyItemsRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public IPolicyTypeRepository PolicyType { get; private set; }
        public IPolicyItemsRepository PolicyItems { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }  

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailsRepository OrderDetails { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
