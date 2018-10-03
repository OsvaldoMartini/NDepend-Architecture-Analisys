using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Geo.Localization.WebApp
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {

            //Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 1"); //History
            //Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 2"); //Coolies
            //Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 8"); //Temporary Internet Files
            //Process.Start("rundll32.exe", "InetCpl.cpl,ClearMyTracksByProcess 16"); //Form (Data)


            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new MultiLanguageViewEngine());
            //ViewEngines.Engines.Add(new WebFormViewEngine());

        }
    }
}
