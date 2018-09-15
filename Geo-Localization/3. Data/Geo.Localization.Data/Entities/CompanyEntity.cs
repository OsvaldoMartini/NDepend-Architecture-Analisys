
using System;
using System.Collections.Generic;

namespace Geo.Localization.Data
{
    public partial class CompanyEntity
    {
        public CompanyEntity()
        {
            this.EmployeeList = new HashSet<EmployeeEntity>();
        }

        public int CompanyID { get; set; }
        public string CompanyType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }

        public virtual ICollection<EmployeeEntity> EmployeeList { get; set; }
    }
}
