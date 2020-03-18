using on_sales.DataAccess;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(OrderHeader orderHeader)
        {
            var orderHeaderFromDb = _db.OrderHeader.FirstOrDefault(p => p.Id == orderHeader.Id);
            _db.OrderHeader.Update(orderHeaderFromDb);
            _db.SaveChanges();
            
        }
    }
}
