using System.ComponentModel.DataAnnotations;
using StuffRescue.FeatureToggle;

namespace StuffRescue.Web.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public IFeatureToggle Facebook { get; set; }
    }
}
