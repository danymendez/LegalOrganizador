#pragma checksum "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4b559870b88afcf69cb30f462b41bce49bbead05"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_SisPerPersonas_Details), @"mvc.1.0.view", @"/Views/SisPerPersonas/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/SisPerPersonas/Details.cshtml", typeof(AspNetCore.Views_SisPerPersonas_Details))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\_ViewImports.cshtml"
using PreOrclFrontEnd;

#line default
#line hidden
#line 2 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\_ViewImports.cshtml"
using PreOrclFrontEnd.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4b559870b88afcf69cb30f462b41bce49bbead05", @"/Views/SisPerPersonas/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b30f34cfaf3d74d79d71ae14a5974b07fb15c499", @"/Views/_ViewImports.cshtml")]
    public class Views_SisPerPersonas_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<PreOrclFrontEnd.Models.SisPerPersona>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
            BeginContext(45, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(90, 127, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>SisPerPersona</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(218, 52, false);
#line 14 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_nombre_razon));

#line default
#line hidden
            EndContext();
            BeginContext(270, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(314, 48, false);
#line 17 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_nombre_razon));

#line default
#line hidden
            EndContext();
            BeginContext(362, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(406, 58, false);
#line 20 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_apellido_comercial));

#line default
#line hidden
            EndContext();
            BeginContext(464, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(508, 54, false);
#line 23 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_apellido_comercial));

#line default
#line hidden
            EndContext();
            BeginContext(562, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(606, 43, false);
#line 26 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_nit));

#line default
#line hidden
            EndContext();
            BeginContext(649, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(693, 39, false);
#line 29 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_nit));

#line default
#line hidden
            EndContext();
            BeginContext(732, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(776, 47, false);
#line 32 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_dui_nrc));

#line default
#line hidden
            EndContext();
            BeginContext(823, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(867, 43, false);
#line 35 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_dui_nrc));

#line default
#line hidden
            EndContext();
            BeginContext(910, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(954, 62, false);
#line 38 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_direccion_departamento));

#line default
#line hidden
            EndContext();
            BeginContext(1016, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1060, 58, false);
#line 41 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_direccion_departamento));

#line default
#line hidden
            EndContext();
            BeginContext(1118, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1162, 59, false);
#line 44 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_direccion_municipio));

#line default
#line hidden
            EndContext();
            BeginContext(1221, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1265, 55, false);
#line 47 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_direccion_municipio));

#line default
#line hidden
            EndContext();
            BeginContext(1320, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1364, 49, false);
#line 50 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_direccion));

#line default
#line hidden
            EndContext();
            BeginContext(1413, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1457, 45, false);
#line 53 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_direccion));

#line default
#line hidden
            EndContext();
            BeginContext(1502, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1546, 48, false);
#line 56 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_telefono));

#line default
#line hidden
            EndContext();
            BeginContext(1594, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1638, 44, false);
#line 59 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_telefono));

#line default
#line hidden
            EndContext();
            BeginContext(1682, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1726, 45, false);
#line 62 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_movil));

#line default
#line hidden
            EndContext();
            BeginContext(1771, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1815, 41, false);
#line 65 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_movil));

#line default
#line hidden
            EndContext();
            BeginContext(1856, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1900, 45, false);
#line 68 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_email));

#line default
#line hidden
            EndContext();
            BeginContext(1945, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1989, 41, false);
#line 71 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_email));

#line default
#line hidden
            EndContext();
            BeginContext(2030, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2074, 46, false);
#line 74 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_codigo));

#line default
#line hidden
            EndContext();
            BeginContext(2120, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2164, 42, false);
#line 77 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_codigo));

#line default
#line hidden
            EndContext();
            BeginContext(2206, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2250, 52, false);
#line 80 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_nacionalidad));

#line default
#line hidden
            EndContext();
            BeginContext(2302, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2346, 48, false);
#line 83 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_nacionalidad));

#line default
#line hidden
            EndContext();
            BeginContext(2394, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2438, 59, false);
#line 86 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_tipo_contribullente));

#line default
#line hidden
            EndContext();
            BeginContext(2497, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2541, 55, false);
#line 89 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_tipo_contribullente));

#line default
#line hidden
            EndContext();
            BeginContext(2596, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2640, 47, false);
#line 92 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_dir_cli));

#line default
#line hidden
            EndContext();
            BeginContext(2687, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2731, 43, false);
#line 95 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_dir_cli));

#line default
#line hidden
            EndContext();
            BeginContext(2774, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(2818, 46, false);
#line 98 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.per_cobros));

#line default
#line hidden
            EndContext();
            BeginContext(2864, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2908, 42, false);
#line 101 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
       Write(Html.DisplayFor(model => model.per_cobros));

#line default
#line hidden
            EndContext();
            BeginContext(2950, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(2997, 61, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ab1fcea5659941d9a978e03bd9e39125", async() => {
                BeginContext(3050, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 106 "C:\Users\Palacios\Source\Repos\PreOracle\PreOrclFrontEnd\Views\SisPerPersonas\Details.cshtml"
                           WriteLiteral(Model.per_IDPER);

#line default
#line hidden
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
            EndContext();
            BeginContext(3058, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(3066, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "27ac71924ed149b1bc08053b168f9e8c", async() => {
                BeginContext(3088, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3104, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<PreOrclFrontEnd.Models.SisPerPersona> Html { get; private set; }
    }
}
#pragma warning restore 1591
