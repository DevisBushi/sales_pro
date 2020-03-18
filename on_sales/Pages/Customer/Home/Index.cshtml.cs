using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Utility;

namespace on_sales.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;

            public IndexModel(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }

        public IEnumerable<PolicyItems> PolicyItemsList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if(claim != null)
            {
                int shoppingCartCount = _workingUnit.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCart, shoppingCartCount);
            }

            PolicyItemsList = _workingUnit.PolicyItems.GetAll(null, null, "Category,PolicyType");
            CategoryList = _workingUnit.Category.GetAll(null, q => q.OrderBy(c => c.DispOrder), null);
        }
    }
}