
using System;
using System.Collections.Generic;

namespace Geo.Localization.Data
{
    public partial class CompanySaleEntity
    {
        public CompanySaleEntity()
        {
            this.TCompany = new CompanyEntity();
            this.CompanySaleList = new HashSet<CompanySaleEntity>();

        }

        public int CompanySaleID { get; set; }
        public int CompanyID { get; set; }
        public int SaleYear { get; set; }
        public string SaleMonth { get; set; }
        public double TotalSale { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }

        public virtual CompanyEntity TCompany { get; set; }

        public virtual ICollection<CompanySaleEntity> CompanySaleList { get; set; }
    }
}
