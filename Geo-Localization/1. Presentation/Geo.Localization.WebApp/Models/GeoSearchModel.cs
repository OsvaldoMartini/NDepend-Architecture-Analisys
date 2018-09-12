using System.ComponentModel.DataAnnotations;
using Geo.Localization.Services.Texts;


namespace Geo.Localization.WebApp.Models
{
    [MetadataType(typeof(GeoSearchModelMetaData))]
    public partial class GeoSearchModel
    {
        public int GeoLocalizationID { get; set; }
        public int EmployeeID { get; set; }
        public string LocalName { get; set; }
        public string Lat { get; set; }
        public string Lng { get; set; }
        }

    public partial class GeoSearchModelMetaData
    {
        public int  GeoLocalizationID { get; set; }
        public int EmployeeID { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "LocalName", ResourceType = typeof(RHome))]
        [StringLength(80, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string LocalName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Lat", ResourceType = typeof(RHome))]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Lat { get; set; }
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Lng", ResourceType = typeof(RHome))]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Lng { get; set; }
}
}