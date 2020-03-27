using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models.ViewModels;
using Sales;
using System.Security.Claims;
using Sales.Models;
using Microsoft.AspNetCore.Http;
using Sales.Utility;

namespace on_sales.Pages.Customer.Cart
{
    public class IndexModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;
        public IndexModel(IWorkingUnit workingUnit)
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

        public IActionResult OnPostPlus(int cartId)
        {
            var cart = _workingUnit.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            _workingUnit.ShoppingCart.IncrementCount(cart, 1);
            _workingUnit.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int cartId)
        {
            var cart = _workingUnit.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            if (cart.Count == 1)
            {
                _workingUnit.ShoppingCart.Remove(cart);
                _workingUnit.Save();

                var cnt = _workingUnit.ShoppingCart.
                                GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
            }
            else
            {
                _workingUnit.ShoppingCart.DecrementCount(cart, 1);
                _workingUnit.Save();

            }


            return RedirectToPage("/Customer/Cart/Index");
        }


        public IActionResult OnPostRemove(int cartId)
        {
            var cart = _workingUnit.ShoppingCart.GetFirstOrDefault(c => c.Id == cartId);
            _workingUnit.ShoppingCart.Remove(cart);
            _workingUnit.Save();

            var cnt = _workingUnit.ShoppingCart.
                               GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}