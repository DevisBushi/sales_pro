using Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.DataAccess.Repository.IRepository
{
    public interface IPolicyItemsRepository : IRepository<PolicyItems>
    {
        void Update(PolicyItems policyItems);
    }
}
