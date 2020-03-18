using on_sales.DataAccess;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderDetails orderDetails)
        {
            var orderDetailsFromDb = _db.OrderDetails.FirstOrDefault(p => p.Id == orderDetails.Id);
            _db.OrderDetails.Update(orderDetailsFromDb);
            _db.SaveChanges();
            
        }
    }
}
