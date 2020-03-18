using Microsoft.AspNetCore.Mvc.Rendering;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.DataAccess.Repository.IRepository
{
    public interface IPolicyTypeRepository : IRepository<PolicyType>
    {
        IEnumerable<SelectListItem> GetPolicyTypeListForDropDown();
        void Update(PolicyType policyType);
    }
}
