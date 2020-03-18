using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository;
using Sales.DataAccess.Repository.IRepository;
using Sales.Models;
using Sales.Utility;

namespace on_sales.Pages.Administration.Kategoria
{
    [Authorize(Roles = SD.ManagerRole)]
    public class UpsertModel : PageModel
    {
        private readonly IWorkingUnit _workingUnit;
            public UpsertModel(IWorkingUnit workingUnit)
        {
            _workingUnit = workingUnit;
        }

        [BindProperty]
        public Sales.Models.Category CategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Sales.Models.Category();
            if(id != null)
            {
                CategoryObj = _workingUnit.Category.GetFirstOrDefault(i => i.Id == id);
                if(CategoryObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (CategoryObj.Id == 0)
            {
                _workingUnit.Category.Add(CategoryObj);
            }
            else
            {
                _workingUnit.Category.Update(CategoryObj);
            }
            _workingUnit.Save();
            return RedirectToPage("./Index");
        }


    }
}