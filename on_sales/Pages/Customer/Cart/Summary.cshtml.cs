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
using Sales.Models.ViewModels;
using Sales.Utility;
using Stripe;

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
            detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            detailCart.OrderHeader.OrderDate = DateTime.Now;
            detailCart.OrderHeader.UserId = claim.Value;
            detailCart.OrderHeader.Status = SD.PaymentStatusPending;
            detailCart.OrderHeader.PickUpTime = Convert.ToDateTime(
                detailCart.OrderHeader.PickUpDate.ToShortDateString() + " " + detailCart.OrderHeader.PickUpTime.ToShortTimeString());

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            _workingUnit.OrderHeader.Add(detailCart.OrderHeader);
            _workingUnit.Save();

            foreach (var item in detailCart.listCart)
            {
                item.PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(m => m.Id == item.PolicyItemsId);
                OrderDetails orderDetails = new OrderDetails
                {
                    PolicyItemsId = item.PolicyItemsId,
                    OrderId = detailCart.OrderHeader.Id,
                    Description = item.PolicyItems.Description,
                    Name = item.PolicyItems.Name,
                    Price = item.PolicyItems.Price,
                    Count = item.Count

                };
                detailCart.OrderHeader.OrderTotal += (orderDetails.Count * orderDetails.Price);
                _workingUnit.OrderDetails.Add(orderDetails);
            }
            detailCart.OrderHeader.OrderTotal = Convert.ToDouble(String.Format("{0:.##}", detailCart.OrderHeader.OrderTotal));
            _workingUnit.ShoppingCart.RemoveRange(detailCart.listCart);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
            _workingUnit.Save();
            if (stripeToken != null)
            {
                StripeConfiguration.ApiKey = "sk_test_vtR3ue4zalL9zTMBswgEiLSS00zqKhmtu9";



                var options = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(detailCart.OrderHeader.OrderTotal * 100),
                    Currency = "USD",
                    Source = stripeToken,
                    Description = "Order Id: " + detailCart.OrderHeader.Id
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);


                detailCart.OrderHeader.TransactionId = charge.Id;
                if (charge.Status.ToLower() == "succeeded")
                {
                    //email
                    detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;
                    detailCart.OrderHeader.Status = SD.StatusSubmitted;
                }
                else
                {
                    detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }

            }
            else
            {
                detailCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
            }
            _workingUnit.Save();

            return RedirectToPage("/Customer/Cart/OrderConfirmation", new { id = detailCart.OrderHeader.Id });
        }
    }
}