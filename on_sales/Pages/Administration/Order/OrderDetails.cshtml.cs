using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Models.ViewModels;
using Sales.Utility;
using Stripe;

namespace on_sales.Pages.Administration.Order
{
    public class OrderDetailsModel : PageModel
    {

        private readonly IWorkingUnit _workingUnit;
        public OrderDetailsModel(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }
        [BindProperty]
        public OrderDetailsVM OrderDetailsVM { get; set; }
        public void OnGet(int id)
        {
            OrderDetailsVM = new OrderDetailsVM()
            {
                OrderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(m => m.Id == id),
                OrderDetails = _workingUnit.OrderDetails.GetAll(i => i.OrderId == id).ToList()
            };
            OrderDetailsVM.OrderHeader.ApplicationUser = _workingUnit.ApplicationUser.GetFirstOrDefault(e => e.Id == OrderDetailsVM.OrderHeader.UserId);
        }
        public IActionResult OnPostOrderConfirm(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCompleted;
            _workingUnit.Save();
            return RedirectToPage("OrderList");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCancelled;
            _workingUnit.Save();
            return RedirectToPage("OrderList");
        }

        public IActionResult OnPostOrderRefund(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            //refund amount
            var options = new RefundCreateOptions
            {
                Amount = Convert.ToInt32(orderHeader.OrderTotal * 100),
                Reason = RefundReasons.RequestedByCustomer,
                Charge = orderHeader.TransactionId
            };
            var service = new RefundService();
            Refund refund = service.Create(options);

            orderHeader.Status = SD.StatusRefunded;
            _workingUnit.Save();
            return RedirectToPage("OrderList");
        }
    }
}