
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
        public int Year { get; set; }
        public string Month { get; set; }
        public double Sales { get; set; }
        public Nullable<DateTime> DateCreated { get; set; }

        public virtual CompanyEntity TCompany { get; set; }

        public virtual ICollection<CompanySalesEntity> CompanySalesList { get; set; }
    }
}
