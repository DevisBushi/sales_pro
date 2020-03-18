using Microsoft.AspNetCore.Mvc.Rendering;
using on_sales.DataAccess;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class PolicyTypeRepository : Repository<PolicyType>, IPolicyTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public PolicyTypeRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetPolicyTypeListForDropDown()
        {
            return _db.PolicyType.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }


        public void Update(PolicyType objectToBeUpdated)
        {
            var policyTypeFromDb = _db.PolicyType.FirstOrDefault(s => s.Id == objectToBeUpdated.Id);
            policyTypeFromDb.Name = objectToBeUpdated.Name;
            _db.SaveChanges();
        }

    }
}
