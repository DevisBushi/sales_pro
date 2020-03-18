using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.DataAccess.Repository.IRepository;

namespace on_sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IWorkingUnit _workingUnit;

        public UserController(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _workingUnit.ApplicationUser.GetAll() });
        }


        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _workingUnit.ApplicationUser.GetFirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Gabim gjate procedures Lock/Unlock" });
            }
            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _workingUnit.Save();


            return Json(new { success = true, message = "U krye me sukses." });
        }

    }
}