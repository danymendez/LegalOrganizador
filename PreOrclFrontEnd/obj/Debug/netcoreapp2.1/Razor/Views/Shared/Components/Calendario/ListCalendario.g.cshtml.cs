#pragma checksum "C:\Users\dmendez\Source\Repos\PreOracle2\PreOrclFrontEnd\Views\Shared\Components\Calendario\ListCalendario.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0abf2dbff7e83a92558f6d695247ed121c2a83ae"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Components_Calendario_ListCalendario), @"mvc.1.0.view", @"/Views/Shared/Components/Calendario/ListCalendario.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/Components/Calendario/ListCalendario.cshtml", typeof(AspNetCore.Views_Shared_Components_Calendario_ListCalendario))]
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
#line 1 "C:\Users\dmendez\Source\Repos\PreOracle2\PreOrclFrontEnd\Views\_ViewImports.cshtml"
using PreOrclFrontEnd;

#line default
#line hidden
#line 2 "C:\Users\dmendez\Source\Repos\PreOracle2\PreOrclFrontEnd\Views\_ViewImports.cshtml"
using PreOrclFrontEnd.Models;

#line default
#line hidden
#line 3 "C:\Users\dmendez\Source\Repos\PreOracle2\PreOrclFrontEnd\Views\_ViewImports.cshtml"
using PreOrclFrontEnd.Extensions;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0abf2dbff7e83a92558f6d695247ed121c2a83ae", @"/Views/Shared/Components/Calendario/ListCalendario.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c1eae7e7d2a360c67a397481350099960760207", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Components_Calendario_ListCalendario : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Microsoft.Graph.Event>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(43, 132, true);
            WriteLiteral("<div class=\"calender-cont widget-calender\">\r\n    <div id=\"calendar\"></div>\r\n</div>\r\n<button id=\"botonnuevo\">Botón Nuevo</button>\r\n\r\n");
            EndContext();
            DefineSection("Scripts", async() => {
                BeginContext(437, 133, true);
                WriteLiteral("\r\n    <script>\r\n\r\n\r\n        jQuery(\"#botonnuevo\").click(function () {\r\n            jQuery(this).hide();\r\n        });\r\n    </script>\r\n");
                EndContext();
            }
            );
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Microsoft.Graph.Event>> Html { get; private set; }
    }
}
#pragma warning restore 1591