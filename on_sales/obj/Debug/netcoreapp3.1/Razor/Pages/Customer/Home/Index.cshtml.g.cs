#pragma checksum "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c2ee768fe9297a01f8344256bbd7530394aa3b48"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(on_sales.Pages.Customer.Home.Pages_Customer_Home_Index), @"mvc.1.0.razor-page", @"/Pages/Customer/Home/Index.cshtml")]
namespace on_sales.Pages.Customer.Home
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\_ViewImports.cshtml"
using on_sales;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\_ViewImports.cshtml"
using on_sales.DataAccess;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c2ee768fe9297a01f8344256bbd7530394aa3b48", @"/Pages/Customer/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc820375ccb776b4526db1dd4410ac15f1c6d4c0", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Customer_Home_Index : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("border-radius:2px;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
 foreach (var category in Model.CategoryList)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
     if (Model.PolicyItemsList.Where(c => c.CategoryId == category.Id).Count() > 0)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"row container pb-3\">\r\n            <div class=\"col-12\">\r\n                <div class=\"row\">\r\n                    <h2 class=\"text-black-50 pl-1\"><b>");
#nullable restore
#line 14 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                                                 Write(category.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></h2>\r\n\r\n                    <div class=\"col-12\">\r\n                        <div class=\"row my-3\">\r\n");
#nullable restore
#line 18 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                             foreach(var policyItem in Model.PolicyItemsList.Where(c=> c.CategoryId == category.Id))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <div class=""col-lg-3 col-xl-3 col-md-6 pb-4"">
                                    <div class=""card""style=""border:2px solid #43ac6a; border-radius:5px;"">
                                        <div class=""pl-3 pt-1 text-center"">
                                            <h3 class=""card-title text-primary""><b>");
#nullable restore
#line 23 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                                                                              Write(policyItem.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b></h3>
                                        </div>
                                        <div class=""d-flex justify-content-between form-control"" style=""border:0px;"">
                                            <div class=""col-6 badge badge-warning text-center"" style=""font-size:14px;"">
                                                ");
#nullable restore
#line 27 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                                           Write(policyItem.PolicyType.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                            </div>\r\n                                            <div class=\"col-6 text-right h4\" style=\"color:maroon;\"><b>");
#nullable restore
#line 29 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                                                                                                 Write(policyItem.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></div>\r\n                                        </div>\r\n\r\n\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c2ee768fe9297a01f8344256bbd7530394aa3b487782", async() => {
                WriteLiteral("Detajet");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 33 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                                                                                                            WriteLiteral(policyItem.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 36 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </div>\r\n                    </div>\r\n\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 43 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 43 "C:\Users\Bushi\source\repos\sales_pro\on_sales\Pages\Customer\Home\Index.cshtml"
     
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<on_sales.Pages.Customer.Home.IndexModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<on_sales.Pages.Customer.Home.IndexModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<on_sales.Pages.Customer.Home.IndexModel>)PageContext?.ViewData;
        public on_sales.Pages.Customer.Home.IndexModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
