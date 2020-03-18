using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Models.ViewModels
{
    public class PolicyItemsVM
    {
        public PolicyItems PolicyItems { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> PolicyTypeList { get; set; }
    }
}
