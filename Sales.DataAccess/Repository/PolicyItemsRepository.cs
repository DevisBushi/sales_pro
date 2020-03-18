using on_sales.DataAccess;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class PolicyItemsRepository : Repository<PolicyItems>, IPolicyItemsRepository
    {
        private readonly ApplicationDbContext _db;
        public PolicyItemsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(PolicyItems policyItems)
        {
            var policyItemsFromDb = _db.PolicyItems.FirstOrDefault(p => p.Id == policyItems.Id);

            policyItemsFromDb.Name = policyItems.Name;
            policyItemsFromDb.CategoryId = policyItems.CategoryId;
            policyItemsFromDb.Description = policyItems.Description;
            policyItemsFromDb.PolicyTypeId = policyItems.PolicyTypeId;
            policyItemsFromDb.Price = policyItems.Price;
            if(policyItems.Image != null)
            {
                policyItemsFromDb.Image = policyItems.Image;
            }
            _db.SaveChanges();
            
        }
    }
}
