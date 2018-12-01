using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Geo.Localization.Services.DataTransferObject
{
    [DataContract(Name = "CompanySales")]
    public class CompanySalesDto : BaseAccessDto
    {
        public CompanySalesDto()
        {
            this.TCompany = new CompanyDto();
            this.CompanySalesList = new HashSet<CompanySalesDto>();
        }
        
        [DataMember]
        public int CompanySalesID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        [Display(Name = "SalesYear")]
        public int SalesYear { get; set; }

        [DataMember]
        [Display(Name = "SalesMonth")]
        public string SalesMonth { get; set; }
        
        [DataMember]
        [Display(Name = "TotalSales")]
        public double TotalSales { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public virtual CompanyDto TCompany { get; set; }

        [DataMember]
        public virtual ICollection<CompanySalesDto> CompanySalesList { get; set; }

    }
}