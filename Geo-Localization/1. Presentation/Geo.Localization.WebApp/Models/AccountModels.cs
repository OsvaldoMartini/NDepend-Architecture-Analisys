using System.ComponentModel.DataAnnotations;
using Geo.Localization.Services.Texts;

namespace Geo.Localization.WebApp.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    [MetadataType(typeof(LoginModelMetaData))]
    public partial class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyID { get; set; }
    }



    public partial class LoginModelMetaData
    {
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "UserName", ResourceType = typeof(RGlobal))]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "Password", ResourceType = typeof(RGlobal))]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(RGlobal))]
        [Display(Name = "CompanyID", ResourceType = typeof(RGlobal))]
        public string CompanyID { get; set; }

    }
}