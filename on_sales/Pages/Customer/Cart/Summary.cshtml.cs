using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Models.ViewModels;

namespace on_sales.Pages.Customer.Cart
{
    public class SummaryModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;
        public SummaryModel(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }
        public OrderDetailsCart OrderDetailsCartVM { get; set; }
        public void OnGet()
        {
            OrderDetailsCartVM = new OrderDetailsCart()
            {
                OrderHeader = new Sales.Models.OrderHeader(),
                listCart = new List<ShoppingCart>()
            };

            OrderDetailsCartVM.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {

                IEnumerable<ShoppingCart> cart = _workingUnit.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);

                if (cart != null)
                {
                    OrderDetailsCartVM.listCart = cart.ToList();
                }
                foreach (var cartList in OrderDetailsCartVM.listCart)
                {
                    cartList.PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(p => p.Id == cartList.PolicyItemsId);
                    OrderDetailsCartVM.OrderHeader.OrderTotal += (cartList.PolicyItems.Price * cartList.Count);
                }
            }
        }

    }
}