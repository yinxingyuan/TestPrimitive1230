using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;
namespace TestPrimitive.Web
{
    public class AreaViewEngine : RazorViewEngine
    {
        //public AreaViewEngine()
        //{
        //    this.AreaViewLocationFormats = new[] {"~/Views/{2}/{1}/{0}.cshtml","~/Views/Shared/{0}.cshtml"};

        //    this.AreaMasterLocationFormats = new[] { "~/Views/Shared/{0}.cshtml" };

        //    this.AreaPartialViewLocationFormats = new[] { "~/Views/{2}/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" }; 

        //    this.ViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" }; 

        //    this.MasterLocationFormats = new[] { "~/Views/Shared/{0}.cshtml" };  

        //    this.PartialViewLocationFormats = new[] { "~/Views/{1}/{0}.cshtml", "~/Views/Shared/{0}.cshtml" }; 
        //}
        public AreaViewEngine(IRazorPageFactoryProvider pageFactory, IRazorPageActivator pageActivator, HtmlEncoder htmlEncoder, IOptions<RazorViewEngineOptions> optionsAccessor, RazorProject razorProject, ILoggerFactory loggerFactory, DiagnosticSource diagnosticSource) : base(pageFactory, pageActivator, htmlEncoder, optionsAccessor, razorProject, loggerFactory, diagnosticSource)
        {
        }
    }
}