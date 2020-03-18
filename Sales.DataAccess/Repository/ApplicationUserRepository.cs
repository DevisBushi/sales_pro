using on_sales.DataAccess;
using Sales.DataAccess.Repository;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;

        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
