using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace Geo.Localization.WebApp.Filters
{
    public class CustomActionFilter : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;


        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();

            if (!filterContext.HttpContext.Request.IsLocal)
            {
                throw new Exception();
                //filterContext.Result = new HttpNotFoundResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if (filterContext.Exception == null)
            {
               // filterContext.HttpContext.Response.Write(string.Format("Time taken: {0}", timer.Elapsed.TotalSeconds));
            }
        }

    }  
}