
using System;
using System.Collections.Generic;

namespace Geo.Localization.Data
{
    public partial class CompanySalesEntity
    {
        public CompanySalesEntity()
        {
            this.TCompany = new CompanyEntity();
            this.CompanySalesList = new HashSet<CompanySalesEntity>();

        }

        public int CompanySalesID { get; set; }
        public int CompanyID { get; set; }
        public int SalesYear { get; set; }
        public string SalesMonth { get; set; }
        public double TotalSales { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }

        public virtual CompanyEntity TCompany { get; set; }

        public virtual ICollection<CompanySalesEntity> CompanySalesList { get; set; }
    }
}
