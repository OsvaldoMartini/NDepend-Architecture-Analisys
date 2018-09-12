using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using Geo.Localization.Services.DataTransferObject;
using Geo.Localization.Services.ServiceImplementation;
using Geo.Localization.Services.Utils;


namespace Geo.Localization.WebApp.Utils
{
    /// <summary>
    ///     Classe responsável por disponibilizar, para a aplicação, os controles
    ///     necessários para o tratamento de sessão de informações.
    /// </summary>
    public class SessionHelper
    {
        static int companyId = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["CompanyID"]);
            
        public static EmployeeDto SessioUserLogged
        {
            get { return RecoverValue<EmployeeDto>(SessionName._userLogged); }
        }

        public static CompanyDto SessionCompany
        {
            get
            {
                var companyVar = RecoverValue<CompanyDto>(SessionName._company);

                if (companyVar == null)
                    using (var companyService = new CompanyService())
                    {
                        DefineValue(SessionName._company, companyService.FindByID(companyId));
                     }

                return RecoverValue<CompanyDto>(SessionName._company);
            }
        }
     
     


        public static ICollection<EmployeeDto> SessionEmployee
        {
            get
            {
                var employeeVar = RecoverValue<ICollection<EmployeeDto>>(SessionName._employee);

                if (((employeeVar == null || employeeVar.Count == 0) )&& (SessioUserLogged != null))
                
                    using (var employeeService = new EmployeeService())
                    {
                        employeeVar = employeeService.GetAllEmployee(new EmployeeDto
                        {
                            TCompany = SessioUserLogged.TCompany

                        });
                        foreach (var employees in employeeVar)
                        {
                            employees.RoleName = employees.RoleID.ToEnumNameById<EnumsHelper.UserRoles>();
                            employees.NameUserRole = employees.FirstName + " (" +
                                                     employees.RoleID.ToEnumNameById<EnumsHelper.UserRoles>() + ")";
                            //employees.NameUserRole = RecoverValue<EmployeeDto>(SessionName._userLogged) != null ? RecoverValue<EmployeeDto>(SessionName._userLogged).NameUserRole : null;
                        }


                        DefineValue(SessionName._employee, employeeVar);
                    }

                return RecoverValue<ICollection<EmployeeDto>>(SessionName._employee);
            }
        }

   
      

        #region [ DefineValue ]

        /// <summary>
        ///     Method responsible for adding items in object list
        /// </summary>
        /// <param name="sessionName">Name of the session item where the object will be referenced</param>
        /// <param name="value">Object to be referenced</param>
        public static void DefineValue(SessionName sessionName, object value)
        {
            HttpContext.Current.Session[sessionName.ToString()] = value;
        }

        #endregion

        #region [ RecoverValue ]

        /// <summary>
        ///     Method responsible for retrieving items from the list of objects in the system user session.
        /// </summary>
        /// <typeparam name="T">Type of object to be fetched in session</typeparam>
        /// <param name="sessionName">Session Item Name</param>
        /// <returns>Object converted to the indicated type</returns>
        [DebuggerStepThrough]
        public static T RecoverValue<T>(SessionName sessionName)
        {
            return (T) HttpContext.Current.Session[sessionName.ToString()];
        }

        #endregion

        #region [ Remove Value ]

        /// <summary>
        ///     Method responsible for removing items from the list of objects in the system user session.
        /// </summary>
        /// <param name="sessionName">Name of the session item to be removed</param>
        public static void RemoveValue(SessionName sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName.ToString());
            HttpContext.Current.Session[sessionName.ToString()] = null;
        }

        #endregion

        #region [ Abandoning Session ]

        /// <summary>
        ///     Method responsible for forcing the abandonment of the information contained in the current session.
        /// </summary>
        public static void AbandonSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        #endregion

        public static bool IsValidAdminOrSupervisor(EmployeeDto userLogged)
        {
            return userLogged != null
                   && (EnumsHelper.UserRoles.Admin.Equals(userLogged.RoleID.ToEnumById<EnumsHelper.UserRoles>())
                       || EnumsHelper.UserRoles.Supervisor.Equals(userLogged.RoleID
                           .ToEnumById<EnumsHelper.UserRoles>()));
        }

        public static bool IsValidOnlyAdmin(EmployeeDto userLogged)
        {
            return userLogged != null
                   && EnumsHelper.UserRoles.Admin.Equals(userLogged.RoleID.ToEnumById<EnumsHelper.UserRoles>());
        }

        public static bool IsValidAdminOrCustomer(EmployeeDto userLogged)
        {
            return userLogged == null ||
                   userLogged != null &&
                   EnumsHelper.UserRoles.Admin.Equals(userLogged.RoleID.ToEnumById<EnumsHelper.UserRoles>());
        }

        public static bool IsValidOnlySupervisor(EmployeeDto userLogged)
        {
            return userLogged != null
                   && EnumsHelper.UserRoles.Supervisor.Equals(userLogged.RoleID
                       .ToEnumById<EnumsHelper.UserRoles>());
        }

        public static bool IsValidSupportOrCustomerServ(EmployeeDto userLogged)
        {
            return userLogged != null
                   && (EnumsHelper.UserRoles.CustomerServices.Equals(userLogged.RoleID
                           .ToEnumById<EnumsHelper.UserRoles>())
                       || EnumsHelper.UserRoles.Support.Equals(userLogged.RoleID
                           .ToEnumById<EnumsHelper.UserRoles>()));
        }
    }

    public enum SessionName
    {
        _company,
        _userLogged,
        _employee,
        _cultureLang
    }
}