using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using System.Web.Services;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceContrat;
using Geo.Localization.Services.ServiceImplementation;
using Geo.Localization.Services.Texts;
using Geo.Localization.WebApp.Extensions;
using Geo.Localization.WebApp.Models;
using Geo.Localization.WebApp.Utils;
using NLog;
using WebGrease.Css.Extensions;


namespace Geo.Localization.WebApp.Controllers
{
    public class HomeController : BaseController
    {

        private readonly IGeoLocalizationService _geoLocalizationService;

        internal static Logger logger = LogManager.GetLogger("HomeController");
        public HomeController()
        {
            this._geoLocalizationService = new GeoLocalizationService();
        }


        public ActionResult Index()
        {
            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            ViewBag.Title = "Geo-Localization";
            return View();
        }

        [HttpPost]
        public JsonResult GetAllSearched(GeoLocalizationDto geoLocalization)
        {
            JsonResult msg = null;

            IList<GeoLocalizationDto> dataSearchedDto = _geoLocalizationService.GetAllGeoLocalization(geoLocalization);

            List<SearchedLocals> dataSearched = new List<SearchedLocals>();

            foreach (GeoLocalizationDto dt in dataSearchedDto)
                dataSearched.Add(
                    new SearchedLocals
                    {
                        Id = dt.GeoLocalizationID,
                        PlaceName = dt.LocalName,
                        OpeningHours = "9-5, M-F",
                        GeoLong = dt.Lng,
                        GeoLat = dt.Lat
                    });


            msg = Json(new
                {
                    success = true,
                    data = dataSearched,
                    responseText = "<strong>" + "Success reading the data" + "</strong>"
                },
                JsonRequestBehavior.AllowGet);

            return msg;

        }

        public ActionResult Searched()
        {
            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            ViewBag.Title = "Geo-Localization";
            return View();
        }

        [HttpPost]
        public JsonResult SaveLocalSearched(GeoLocalizationDto geoLocalization)
        {
            JsonResult msg = null;

            if (!ModelState.IsValid)
            {
                var errorList = ValidationFields.GetModelStateErrors(ModelState);
                msg = Json(
                    new
                    {
                        success = false,
                        errors = errorList,
                        responseText = "<strong>" + RValidation.RequiredFields + "</strong>"
                    },
                    JsonRequestBehavior.AllowGet);
                return msg;
            }


            try
            {
                var userLogged = SessionHelper.SessioUserLogged;

                //Supervisor Cannot Insert
                if (SessionHelper.IsValidOnlyAdmin(userLogged) ||
                    SessionHelper.IsValidSupportOrCustomerServ(userLogged))
                    geoLocalization.EmployeeID = userLogged.EmployeeID;
                else
                    geoLocalization.EmployeeID = 1; //Suppouse be the Frist "Admin"



                _geoLocalizationService.InsertGeoLocalization(geoLocalization);


                msg = Json(
                    new {success = true, responseText = "<strong>" + RValidation.SuccessRegistration + "</strong>"},
                    JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                logger.Error(string.Format("Error Insert Place:{0} \r\n error: \r\n{1}", geoLocalization.LocalName, ex.Message));

                msg = Json(
                    new
                    {
                        success = false,
                        responseText = "<strong>" + RValidation.UnableToRegister + " " + RHome.LocalName +
                                       "</strong>." + RValidation.TryAgainLater
                    }, JsonRequestBehavior.AllowGet);
            }
            finally
            {
                logger.Info(string.Format("Place Searched:{0} from Employee:{1}", geoLocalization.LocalName, geoLocalization.EmployeeID));
            }

            return msg;
        }
    }


    public struct SearchedLocals
    {
        public int Id;
        public string GeoLong;
        public string OpeningHours;
        public string PlaceName;
        public string GeoLat;
    }
}