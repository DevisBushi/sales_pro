using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Sales.DataAccess.Repository.IRepository;
using Sales.Utility;

namespace on_sales.Pages.Administration.Lloji
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
        public Sales.Models.PolicyType PolicyTypeObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            PolicyTypeObj = new Sales.Models.PolicyType();
            if (id != null)
            {
                PolicyTypeObj = _workingUnit.PolicyType.GetFirstOrDefault(i => i.Id == id);
                if (PolicyTypeObj == null)
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
            if (PolicyTypeObj.Id == 0)
            {
                _workingUnit.PolicyType.Add(PolicyTypeObj);
            }
            else
            {
                _workingUnit.PolicyType.Update(PolicyTypeObj);
            }
            _workingUnit.Save();
            return RedirectToPage("./Index");
        }


    }
}