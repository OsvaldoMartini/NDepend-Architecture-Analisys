using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Geo.Localization.WebApp.Extensions;
using Geo.Localization.WebApp.Helpers;

namespace Geo.Localization.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private static string _cookieLangName = "LangForMultiLanguage";

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string cultureOnCookie = GetCultureOnCookie(filterContext.HttpContext.Request);
            string cultureOnURL = filterContext.RouteData.Values.ContainsKey("lang")
                ? filterContext.RouteData.Values["lang"].ToString()
                : GlobalHelper.DefaultCulture;
            string culture = (cultureOnCookie == string.Empty)
                ? (filterContext.RouteData.Values["lang"].ToString())
                : cultureOnCookie;
            if (cultureOnURL.ToLower() != culture.ToLower())
            {
                filterContext.HttpContext.Response.RedirectToRoute("LocalizedDefault",
                    new
                    {
                        lang = culture,
                        controller = filterContext.RouteData.Values["controller"],
                        action = filterContext.RouteData.Values["action"]
                    });
                return;
            }
            SetCurrentCultureOnThread(culture);
            if (culture != MultiLanguageViewEngine.CurrentCulture)
            {
                (ViewEngines.Engines[1] as MultiLanguageViewEngine).SetCurrentCulture(culture);
            }
            base.OnActionExecuting(filterContext);

        }


        private static void SetCurrentCultureOnThread(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                lang = GlobalHelper.DefaultCulture;
            //CultureInfo cultureInfo = new System.Globalization.CultureInfo(lang);
            //Thread.CurrentThread.CurrentUICulture = cultureInfo;
            //Thread.CurrentThread.CurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            using (CultureInfoExtension cultureInfo = new CultureInfoExtension(lang))
            {
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            }


        }

        public static String GetCultureOnCookie(HttpRequestBase request)
        {
            var cookie = request.Cookies[_cookieLangName];
            string culture = string.Empty;
            if (cookie != null)
            {
                culture= cookie.Value;
            }
            return culture;
        }
    }
}