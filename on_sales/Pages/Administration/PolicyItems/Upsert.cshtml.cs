using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models.ViewModels;
using Sales.DataAccess;
using System;
using System.IO;
using Sales.Utility;
using Microsoft.AspNetCore.Authorization;

namespace on_sales.Pages.Administration.PolicyItems
{
    [Authorize(Roles = SD.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UpsertModel(IWorkingUnit workingUnit, IWebHostEnvironment hostingEnviroment)
        {
            _workingUnit = workingUnit;
            _hostingEnvironment = hostingEnviroment;
        }

        [BindProperty]
        public PolicyItemsVM PolicyItemsObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            PolicyItemsObj = new PolicyItemsVM
            {
                CategoryList = _workingUnit.Category.GetCategoryListForDropDown(),
                PolicyTypeList = _workingUnit.PolicyType.GetPolicyTypeListForDropDown(),
                PolicyItems = new Sales.Models.PolicyItems()

            };
            if (id != null)
            {
                PolicyItemsObj.PolicyItems = _workingUnit.PolicyItems.GetFirstOrDefault(i => i.Id == id);
                if(PolicyItemsObj.PolicyItems == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (PolicyItemsObj.PolicyItems.Id == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\policyitems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                PolicyItemsObj.PolicyItems.Image = @"\images\policyitems\" + fileName + extension;

                _workingUnit.PolicyItems.Add(PolicyItemsObj.PolicyItems);
            }
            else
            {
                //Edit Menu Item
                var objFromDb = _workingUnit.PolicyItems.Get(PolicyItemsObj.PolicyItems.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\policyitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    PolicyItemsObj.PolicyItems.Image = @"\images\policyitems\" + fileName + extension;
                }
                else
                {
                    PolicyItemsObj.PolicyItems.Image = objFromDb.Image;
                }


                _workingUnit.PolicyItems.Update(PolicyItemsObj.PolicyItems);
            }
            _workingUnit.Save();
            return RedirectToPage("./Index");
        }


    }
}