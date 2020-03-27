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
    public class ManageOrderModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;

        public ManageOrderModel(IWorkingUnit unitOfWork)
        {
            _workingUnit = unitOfWork;
        }

        [BindProperty]
        public List<OrderDetailsVM> orderDetailsVM { get; set; }


        public void OnGet()
        {
            orderDetailsVM = new List<OrderDetailsVM>();

            List<OrderHeader> OrderHeaderList = _workingUnit.OrderHeader
                .GetAll(o => o.Status == SD.StatusSubmitted || o.Status == SD.StatusInProcess)
                .OrderByDescending(u => u.PickUpTime).ToList();


            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVM individual = new OrderDetailsVM
                {
                    OrderHeader = item,
                    OrderDetails = _workingUnit.OrderDetails.GetAll(o => o.OrderId == item.Id).ToList()
                };
                orderDetailsVM.Add(individual);
            }
        }

        public IActionResult OnPostOrderPrepare(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusInProcess;
            _workingUnit.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderReady(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusReady;
            _workingUnit.Save();
            return RedirectToPage("ManageOrder");
        }

        public IActionResult OnPostOrderCancel(int orderId)
        {
            OrderHeader orderHeader = _workingUnit.OrderHeader.GetFirstOrDefault(o => o.Id == orderId);
            orderHeader.Status = SD.StatusCancelled;
            _workingUnit.Save();
            return RedirectToPage("ManageOrder");
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
            return RedirectToPage("ManageOrder");
        }

    }
}