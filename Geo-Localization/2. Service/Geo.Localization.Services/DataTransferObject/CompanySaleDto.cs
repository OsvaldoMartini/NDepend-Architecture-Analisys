using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Geo.Localization.Services.DataTransferObject
{
    [DataContract(Name = "CompanySales")]
    public class CompanySaleDto : BaseAccessDto
    {
        public CompanySaleDto()
        {
            this.TCompany = new CompanyDto();
            this.CompanySaleList = new HashSet<CompanySaleDto>();
        }
        
        [DataMember]
        public int CompanySaleID { get; set; }
        [DataMember]
        public int CompanyID { get; set; }

        [DataMember]
        [Display(Name = "SaleYear")]
        public int SaleYear { get; set; }

        [DataMember]
        [Display(Name = "SaleMonth")]
        public string SaleMonth { get; set; }
        
        [DataMember]
        [Display(Name = "TotalSale")]
        public double TotalSale { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public virtual CompanyDto TCompany { get; set; }

        [DataMember]
        public virtual ICollection<CompanySaleDto> CompanySaleList { get; set; }

    }
}