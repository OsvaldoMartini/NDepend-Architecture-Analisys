
using System;

namespace Geo.Localization.Data
{
    public partial class EmployeeEntity
    {
        public EmployeeEntity()
        {
        }

        public int EmployeeID { get; set; }
        public int CompanyID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public virtual CompanyEntity TCompany { get; set; }
    }
}
