using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Geo.Localization.Services.DataTransferObject
{
    [DataContract(Name = "MegaCorpSales")]
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
        [Display(Name = "Year")]
        public int Year { get; set; }

        [DataMember]
        [Display(Name = "Month")]
        public string Month { get; set; }
        
        [DataMember]
        [Display(Name = "Sales")]
        public double Sales { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public virtual CompanyDto TCompany { get; set; }

        [DataMember]
        public virtual ICollection<CompanySalesDto> CompanySalesList { get; set; }

    }
}