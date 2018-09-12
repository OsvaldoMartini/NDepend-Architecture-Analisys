using System;
using System.Configuration;
using System.Web.Mvc;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceContrat;
using Geo.Localization.Services.ServiceImplementation;
using Geo.Localization.Services.Texts;
using Geo.Localization.Services.Utils;
using Geo.Localization.WebApp.Filters;
using Geo.Localization.WebApp.Models;
using Geo.Localization.WebApp.Utils;


namespace Geo.Localization.WebApp.Controllers
{
    public class AccountController : BaseController
    {
        #region Constructor

        static AccountController()
        {
            _employeeService = new EmployeeService();
        }

        #endregion

        #region Primitive Properties

        private static IEmployeeService _employeeService { get; set; }

        #endregion

        public ActionResult Index()
        {
            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            return View();
        }

        public ActionResult Login()
        {
            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            return View();
        }


        [HttpPost]
        public JsonResult LoginAjax(LoginModel model)
        {
            JsonResult msg = null;

            if (!ModelState.IsValid)
            {
                var errorList = ValidationFields.GetModelStateErrors(ModelState);
                msg = Json(new {success = false, errors = errorList, responseText = "<strong>"+ @RGlobal.UnableLogin +"</strong>"},
                    JsonRequestBehavior.AllowGet);
                return msg;
            }
            
            try
            {
                EmployeeDto _employee;

                _employee = _employeeService.FindByUserName(
                    new EmployeeDto
                    {
                        UserName = model.UserName,
                        Password = model.Password,
                         TCompany=  new CompanyDto{
                             CompanyID = Convert.ToInt32(model.CompanyID)
                    }
                        
                    });

                if (_employee != null)
                {
                    _employee.RoleName = _employee.RoleID.ToEnumById<EnumsHelper.UserRoles>().ToString();

                    try
                    {
                        _employee.NameUserRole = _employee.TCompany.Name + " " + _employee.FirstName + " (" +
                                                 _employee.RoleName + ")";

                        SessionHelper.DefineValue(SessionName._userLogged, _employee);

                        msg = Json(new {success = true, responseText = "<strong>Successful!</strong> Employee Logged."},
                            JsonRequestBehavior.AllowGet);
                    }
                    catch
                    {
                        msg = Json(new
                            {
                                success = false,
                                responseText =
                                "<strong>Unable to login</strong>.<br/>The user name or password provided is incorrect.",
                            },
                            JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {

                    msg = Json(new
                        {
                            success = false,
                            responseText =
                            "<strong>Unable to login</strong>.<br/>The user name or password provided is incorrect.",
                        },
                        JsonRequestBehavior.AllowGet);

                }
            }
            catch(Exception ex)
            {
                msg = Json(
                    new { success = false, responseText = "<strong>"+RGlobal.Forbidden+" : <br></strong>. " + ex.Message },
                    JsonRequestBehavior.AllowGet);
            }

            return msg;
        }


        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            if (SessionHelper.SessioUserLogged == null)
            {
                TempData["Unauthorized"] = true;
                var controllerNameReq = ControllerContext.HttpContext.Request.RequestContext.RouteData
                    .Values["controller"].ToString();
                return RedirectToAction("Login", controllerNameReq);
            }
            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            SessionHelper.RemoveValue(SessionName._userLogged);
            return RedirectToAction("Index", "Home");
        }
     
    }
}