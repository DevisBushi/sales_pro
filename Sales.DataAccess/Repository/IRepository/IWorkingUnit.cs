using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.DataAccess.Repository.IRepository
{
    public interface IWorkingUnit : IDisposable
    {
        ICategoryRepository Category { get; }
        IPolicyTypeRepository PolicyType { get; }
        IPolicyItemsRepository PolicyItems { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IOrderDetailsRepository OrderDetails { get; }
        void Save();
    }
}
