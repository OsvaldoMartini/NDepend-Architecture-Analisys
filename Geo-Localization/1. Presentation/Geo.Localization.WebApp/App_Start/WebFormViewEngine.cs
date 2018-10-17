using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Geo.Localization.WebApp.App_Start
{
    public class WebFormViewEngine : VirtualPathProviderViewEngine
    {
        public WebFormViewEngine()
        {
            MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.master",
                "~/Views/Shared/{0}.master"
            };
            ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml",
                "~/Category/{1}/{0}.cshtml",
                "~/Category/Shared/{0}.cshtml"
            };
        
            PartialViewLocationFormats = ViewLocationFormats;
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new WebFormView(controllerContext, partialPath, null);
        }
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new WebFormView(controllerContext, viewPath, masterPath);
        }
    }
}