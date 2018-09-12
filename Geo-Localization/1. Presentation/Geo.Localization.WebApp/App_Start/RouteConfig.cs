using System;
using System.Web.Http.Routing;
using System.Web.Mvc;
using System.Web.Routing;

namespace Geo.Localization.WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LocalizedDefault",
                url: "{lang}/{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", HttpRoute=true },
                constraints: new { lang = "pt-pt|en-gb" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index", lang = "en-gb" , HttpRoute=true}
            );

        }

    }
}
