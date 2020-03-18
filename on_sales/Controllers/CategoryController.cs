using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.DataAccess.Repository;
using Sales.DataAccess.Repository.IRepository;
using Sales.Utility;

namespace on_sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class CategoryController : Controller
    {
        private readonly IWorkingUnit _workingUnit;
        public CategoryController(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _workingUnit.Category.GetAll() });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _workingUnit.Category.GetFirstOrDefault(i => i.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Gabim Gjate Fshirjes!" });
            }
            _workingUnit.Category.Remove(objFromDb);
            _workingUnit.Save();
            return Json(new { success = true, message = "U Fshi!" });
        }
    }
}