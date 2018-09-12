using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Geo.Localization.Services.DataTransferObject
{
    [DataContract(Name = "Company")]
    public class CompanyDto : BaseAccessDto
    {

        public CompanyDto()
        {
            this.EmployeeList = new HashSet<EmployeeDto>();
            
        }

        [DataMember]
        public int CompanyID { get; set; }
        [DataMember]
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Company Name")]
        
        public string Name { get; set; }
        [DataMember]
        [Required]
        [StringLength(250, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [DataMember]
        [Required]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [DataMember]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "State")]
        public string State { get; set; }
        [DataMember]
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [DataMember]
        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }
        [DataMember]
        [Required]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "web Site")]
        public string WebSite { get; set; }
        [DataMember]
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Phone")]
        
        public string Phone { get; set; }
        [DataMember]
        public Nullable<System.DateTime> DateCreated { get; set; }

        [DataMember]
        public virtual ICollection<EmployeeDto> EmployeeList { get; set; }
    
    }
}
