using System.Web.Mvc;
using Geo.Localization.WebApp.Filters;

namespace Geo.Localization.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

       
    }

}
