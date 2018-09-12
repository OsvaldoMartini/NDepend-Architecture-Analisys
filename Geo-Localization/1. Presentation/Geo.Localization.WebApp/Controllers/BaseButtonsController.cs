using System.Configuration;
using System.Text;
using System.Web.Mvc;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.Texts;
using Geo.Localization.Services.Utils;
using Geo.Localization.WebApp.Helpers;
using Geo.Localization.WebApp.Utils;


namespace Geo.Localization.WebApp.Controllers
{
    public class BaseButtonsController : Controller
    {
        //
        // GET: /Buttons/
        //public ActionResult Index()
        //{
        //    return View();
        //}


        #region [Rules vs Buttons]
        public PartialViewResult AllowedButtons()
        {
            var htmlMenu = new StringBuilder();
           
            htmlMenu.Append(
                "<li><a href='" + @Url.HttpRouteUrl(GlobalHelper.CurrentCulture == GlobalHelper.DefaultCulture ? "Default" : "LocalizedDefault", new { lang = GlobalHelper.CurrentCulture, action = "Index", controller = "Home" }) + "'>" + @RGlobal.GeoLocalization + "</a></li>");
                //"<li><a href='#' id='CallLoginPage'>" + RGlobal.LogIn + "</a></li>");
            htmlMenu.Append(
                "<li><a href='" + @Url.HttpRouteUrl(GlobalHelper.CurrentCulture == GlobalHelper.DefaultCulture ? "Default" : "LocalizedDefault", new { lang = GlobalHelper.CurrentCulture, action = "Index", controller = "About" }) + "'>" + @RGlobal.About + "</a></li>");

            ViewBag.HTMLData = htmlMenu;    

            return PartialView("_OnlyAuthorized");
        }


        public PartialViewResult AccesOnlyAuthorized()
        {
            var userLogged = SessionHelper.RecoverValue<EmployeeDto>(SessionName._userLogged);
            var htmlMenu = new StringBuilder();
            if (userLogged != null)
            {
                //Roles For Admin
                if (EnumsHelper.UserRoles.Admin.Equals(userLogged.RoleID.ToEnumById<EnumsHelper.UserRoles>()))
                {
                    //Admin Access
                    htmlMenu.Append(
                        "<li><a href='#' id='CallSearched'>" + RGlobal.Searched + "</a></li>");
                }
                else
                    //Roles For Supervisor
                    if (EnumsHelper.UserRoles.Supervisor.Equals(
                        userLogged.RoleID.ToEnumById<EnumsHelper.UserRoles>()))
                    {
                        //Supervisor Access
                        htmlMenu.Append(
                            "<li><a href='#' id='CallSearched'>" + RGlobal.Searched + "</a></li>");


                    }
                    else
                        //Roles For Customer/Service
                        if (EnumsHelper.UserRoles.Support.Equals(userLogged.RoleID
                                .ToEnumById<EnumsHelper.UserRoles>()) ||
                            EnumsHelper.UserRoles.CustomerServices.Equals(userLogged.RoleID
                                .ToEnumById<EnumsHelper.UserRoles>()))
                        {
                            //To Implement
                            //Customer/Service Access
                            //htmlMenu.Append(
                            //    "<li><a href='#' id='CallOtherServices'>" + RGlobal.OtherServices + "</a></li>");

                        }
            }
            else
            {
                //htmlMenu.Append(
                //    "<li><a href='#' id='CallOtherServices'>" + RGlobal.OtherServices + "</a></li>");
            }

            ViewBag.HTMLData = htmlMenu;
            TempData["UserLogged"] = userLogged;

            return PartialView("_OnlyAuthorized");
        }





        public PartialViewResult ModalConfirmation()
        {
            var htmlAccess = new StringBuilder();

            htmlAccess.Append("<div id='modal-from-dom' class='modal hide fade'>");
            htmlAccess.Append("<div class='modal-dialog'>");
            htmlAccess.Append("<div class='modal-contect'>");

            htmlAccess.Append("<div class='modal-header'>");
            htmlAccess.Append(
                "<button type='button' class='close' data-dismiss='modal' arial-hidden='true'>&times;</button>");
            htmlAccess.Append("<h4 class='modal-title'>Log Off</h4>");
            htmlAccess.Append("</div>");
            htmlAccess.Append("<div class='modal-body'>");
            htmlAccess.Append("<p>Do you want to exit of the system ?</p>");
            htmlAccess.Append("</div>");

            htmlAccess.Append("<div class='modal-footer'>");
            htmlAccess.Append("<li><a href='#' onclick='CallActionLogOff();' class='btn danger'>Yes</a></li>");
            htmlAccess.Append("<li><a href='#' data-dismiss='modal' class='btn secondary'>No</a></li>");
            htmlAccess.Append("</div>");
            htmlAccess.Append("</div>");
            htmlAccess.Append("</div>");
            htmlAccess.Append("</div>");
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }


        public PartialViewResult ButtonLoginPage()
        {
            var userLogged = SessionHelper.SessioUserLogged;
            var htmlAccess = new StringBuilder();
            if (userLogged != null)
            {

                htmlAccess.Append(
                    "<li><a href='#' onclick='CallModalOptPassword();'>" + SessionHelper.SessioUserLogged.NameUserRole + "</a></li>");
            }
            else
            {
                htmlAccess.Append(
                    "<li><a href='#' id='CallLoginPage'>" + @RGlobal.LogIn + "</a></li>");

            }

            ViewData["MvcExtensionless"] = ConfigurationManager.AppSettings["MvcExtensionless"];
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");

        }

        public PartialViewResult ButtonLogin()
        {
            var htmlAccess = new StringBuilder();
            htmlAccess.Append( //onclick='return false;'
                "<button id='CallLogin' class='btn btn-primary' type='submit'>" + @RGlobal.LogIn + "</button>");
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        //Get Name Of Request Controller and create Role
        public PartialViewResult ButtonCreateNew()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidOnlyAdmin(userLogged)) //(SessionHelper.IsValidOnlyAdmin(userLogged))
            {
                var controllerNameReq = ControllerContext.HttpContext.Request.RequestContext.RouteData.Values["controller"]
                    .ToString();

                var action = "Create";
                htmlAccess.Append("<p><a href='#' class='btn btn-primary btn-md btn-block' onclick=CallRedirect('" + controllerNameReq + "','" + action + "');>Create New</a></li></p>");
         
            }
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        public PartialViewResult ButtonBackIndex()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidAdminOrSupervisor(userLogged) ||
                SessionHelper.IsValidSupportOrCustomerServ(userLogged))
            {
                var controllerNameReq = ControllerContext.HttpContext.Request.RequestContext.RouteData.Values["controller"]
                    .ToString();

                var action = "Index";

                htmlAccess.Append("<p><a href='#' class='btn btn-primary btn-md btn-block' onclick=CallRedirect('" + controllerNameReq + "','" + action + "');>Back to List</a></li></p>");
         
            }
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        //Role Only Admin Can Create the Comment and the Customer
        public PartialViewResult ButtonRegisterComment()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidAdminOrCustomer(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnRegister' class='btn btn-primary btn-md btn-block' onclick='return false;'>Register</button></p>");
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        public PartialViewResult ButtonUpdateCustomerComment()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidAdminOrSupervisor(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnUpdate' class='btn btn-primary btn-md btn-block' onclick='return false;'>Update Info</button></p>");
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        public PartialViewResult ButtonDeleteComment()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidOnlyAdmin(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnDelete' class='btn btn-primary btn-md btn-block' onclick='return false;'>Delete Info</button></p>");
            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }


        //Role Only Admin Can Create the Employee
        public PartialViewResult ButtonRegisterEmployee()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidOnlyAdmin(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnRegister' class='btn btn-primary btn-md btn-block' onclick='return false;'>Register</button></p>");

            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        public PartialViewResult ButtonUpdateEmployee()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidOnlyAdmin(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnUpdate' class='btn btn-primary btn-md btn-block' onclick='return false;'>Update Info</button></p>");

            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        public PartialViewResult FieldsAccesReAssignComment()
        {
            var userLogged = SessionHelper.SessioUserLogged;

            var htmlAccess = new StringBuilder();
            if (SessionHelper.IsValidAdminOrSupervisor(userLogged))
                ViewBag.AdminOrSuperv = true;
            else if (SessionHelper.IsValidOnlySupervisor(userLogged))
                ViewBag.OnlySuperv = true;
            else if (SessionHelper.IsValidSupportOrCustomerServ(userLogged))
                ViewBag.SupportOrCustomerServ = true;


            if (SessionHelper.IsValidSupportOrCustomerServ(userLogged))
                htmlAccess.Append(
                    "<p><button id='btnReAssignComment' class='btn btn-primary btn-md btn-block' disabled >Re-Assign</button></p>");


            ViewBag.HTMLData = htmlAccess;
            return PartialView("_OnlyAuthorized");
        }

        #endregion
	}
}