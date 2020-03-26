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
        [BindProperty]
        public OrderDetailsCart detailCart { get; set; }

        public IActionResult OnGet()
        {
            detailCart = new OrderDetailsCart()
            {
                OrderHeader = new Sales.Models.OrderHeader()
            };

            detailCart.OrderHeader.OrderTotal = 0;

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<ShoppingCart> cart = _workingUnit.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);
            if (cart != null)
            {
                detailCart.listCart = cart.ToList();
            }
            foreach (var cartList in detailCart.listCart)
            {
                cartList.PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(m => m.Id == cartList.PolicyItemsId);
                detailCart.OrderHeader.OrderTotal += (cartList.PolicyItems.Price * cartList.Count);
            }


            ApplicationUser applicationUser = _workingUnit.ApplicationUser.GetFirstOrDefault(c => c.Id == claim.Value);
            detailCart.OrderHeader.PickupName = applicationUser.FullName;
            detailCart.OrderHeader.PickUpTime = DateTime.Now;
            detailCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            return Page();
        }

        public IActionResult OnPost(string stripeToken)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            detailCart.listCart = _workingUnit.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();
        }
    }
}