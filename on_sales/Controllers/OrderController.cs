using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Models.ViewModels;
using Sales.Utility;

namespace on_sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IWorkingUnit _workingUnit;

        public OrderController(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(string status = null)
        {
            List<OrderDetailsVM> orderListVM = new List<OrderDetailsVM>();

            IEnumerable<OrderHeader> OrderHeaderList;

            if (User.IsInRole(SD.CustomerRole))
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                OrderHeaderList = _workingUnit.OrderHeader.GetAll(u => u.UserId == claim.Value, null, "ApplicationUser");
            }
            else
            {
                OrderHeaderList = _workingUnit.OrderHeader.GetAll(null, null, "ApplicationUser");
            }

            if (status == "cancelled")
            {
                OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusCancelled || o.Status == SD.StatusRefunded || o.Status == SD.PaymentStatusRejected);
            }
            else
            {
                if (status == "completed")
                {
                    OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusCompleted);
                }
                else
                {
                    OrderHeaderList = OrderHeaderList.Where(o => o.Status == SD.StatusReady || o.Status == SD.StatusInProcess || o.Status == SD.StatusSubmitted || o.Status == SD.PaymentStatusPending);
                }

            }
            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVM individual = new OrderDetailsVM
                {
                    OrderHeader = item,
                    OrderDetails = _workingUnit.OrderDetails.GetAll(o => o.OrderId == item.Id).ToList()
                };
                orderListVM.Add(individual);
            }
            return Json(new { data = orderListVM });
        }
    }
}