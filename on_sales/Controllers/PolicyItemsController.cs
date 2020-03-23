using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sales.DataAccess.Repository;
using Sales.DataAccess.Repository.IRepository;


namespace on_sales.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyItemsController : Controller
    {
        private readonly IWorkingUnit _workingUnit;
        private readonly IWebHostEnvironment _hostingEnviroment;
        public PolicyItemsController(IWorkingUnit workingUnit, IWebHostEnvironment hostingEnviroment)
        {
            _workingUnit = workingUnit;
            _hostingEnviroment = hostingEnviroment;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _workingUnit.PolicyItems.GetAll(null,null,"Category,PolicyType") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try 
           { 

            var objFromDb = _workingUnit.PolicyItems.GetFirstOrDefault(i => i.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Gabim Gjate Fshirjes!" });
            }

            var imagePath = Path.Combine(_hostingEnviroment.WebRootPath, objFromDb.Image.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _workingUnit.PolicyItems.Remove(objFromDb);
            _workingUnit.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Json(new { success = false, message = "Gabim Gjate Fshirjes!" });
            }
           
                return Json(new { success = true, message = "U Fshi!" });
           
        }
    }
}