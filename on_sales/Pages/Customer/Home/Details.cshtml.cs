using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Utility;

namespace on_sales.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;

        public DetailsModel(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart()
            {
                PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(includeProperties: "Category,PolicyType", filter: c => c.Id == id),
                PolicyItemsId = id
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                ShoppingCartObj.ApplicationUserId = claim.Value;

                ShoppingCart cartFromDb = _workingUnit.ShoppingCart.GetFirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId &&
                                            c.PolicyItemsId == ShoppingCartObj.PolicyItemsId);

                if(cartFromDb == null)
                {
                    _workingUnit.ShoppingCart.Add(ShoppingCartObj);
                }
                else
                {
                    _workingUnit.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                }
                _workingUnit.Save();

                var count = _workingUnit.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ShoppingCart, count);
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(includeProperties: "Category,PolicyType", filter: c => c.Id == ShoppingCartObj.PolicyItemsId);
               return Page();
            }
        }
    }
}