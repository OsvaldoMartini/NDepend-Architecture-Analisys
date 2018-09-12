using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Services;
using Geo.Localization.WebApp.Helpers;

namespace Geo.Localization.WebApp.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string GetImage(this UrlHelper helper, string imageFileName, bool localizable = true)
        {
            string strUrlPath, strFilePath = string.Empty;
            string appPathName = System.Configuration.ConfigurationManager.AppSettings["appPathAppName"];

            if ((HttpContext.Current.Request.IsLocal) && (HttpContext.Current.IsDebuggingEnabled))
                appPathName = string.Empty;

            if (localizable)
            {
                /* Search result in current culture folder */
                strUrlPath = string.Format("{0}/Content/images/{1}/{2}", appPathName, GlobalHelper.CurrentCulture,
                    imageFileName);
                strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
                if (!File.Exists(strFilePath))
                {
                    /* Search result in default culture folder */
                    strUrlPath = string.Format("{0}/Content/{1}/images/{2}", appPathName, GlobalHelper.DefaultCulture,
                        imageFileName);
                }

                return strUrlPath;
            }

            strUrlPath = string.Format("{0}/Content/images/{1}", appPathName, imageFileName);
            strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
            if (File.Exists(strFilePath))
            {
                /* search result in root directory folder */
                return strUrlPath;
            }

            return strUrlPath;
        }

        public static string GetScript(this UrlHelper helper, string scriptFileName, bool localizable = true)
        {
            string strUrlPath, strFilePath = string.Empty;
            string appPathName = System.Configuration.ConfigurationManager.AppSettings["appPathAppName"];

            if ((HttpContext.Current.Request.IsLocal) && (HttpContext.Current.IsDebuggingEnabled))
                appPathName = string.Empty;

            if (localizable)
            {
                /* Search result in current culture folder */
                strUrlPath = string.Format("{0}/js/lang/{1}/{2}", appPathName, GlobalHelper.CurrentCulture,
                    scriptFileName);
                strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
                if (!File.Exists(strFilePath))
                {
                    /* Search result in default culture folder */
                    strUrlPath = string.Format("{0}/js/{1}/lang/{2}", appPathName, GlobalHelper.DefaultCulture,
                        scriptFileName);
                }

                return strUrlPath;
            }

            strUrlPath = string.Format("/js/lang/{0}", scriptFileName);
            strFilePath = HttpContext.Current.Server.MapPath(strUrlPath);
            if (File.Exists(strFilePath))
            {
                /* search result in root directory folder */
                return strUrlPath;
            }

            return strUrlPath;
        }

        private const string geo_PT_Office = "38.725067,-9.146199";
        private const string geo_UK_Office = "51.320620,-0.138142";

        public static string GetGeoUrlLocation(this UrlHelper helper)
        {

            if (GlobalHelper.CurrentCulture.ToLower().Equals("pt-pt"))
            {
                return @"https://maps.google.com/maps?q=" + geo_PT_Office + "&hl=es;z=14&amp;output=embed";
            }
            else
            {
                return @"https://maps.google.com/maps?q=" + geo_UK_Office + "&hl=es;z=14&amp;output=embed";
            }
        }

        public static string GetGeoLocationPosition()
        {

            if (GlobalHelper.CurrentCulture.ToLower().Equals("pt-pt"))
            {
                return geo_PT_Office;
            }
            else
            {
                return geo_UK_Office;
            }
        }
    }
}