using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Geo.Localization.Services.DataTransferObject
{
    [DataContract(Name = "GeoLocalization")]
    public class GeoLocalizationDto : BaseAccessDto
    {
        public GeoLocalizationDto()
        {
        }

        [DataMember]
        public int GeoLocalizationID { get; set; }
        [DataMember]
        public int EmployeeID { get; set; }
        
        [DataMember]
        [Required]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "LocalName")]
        public string LocalName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Lat")]
        public string Lat { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "Lng")]
        public string Lng { get; set; }

        public int CompanyID { get; set; }
    
    }
}