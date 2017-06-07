using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DevTeamup.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(250)]
        public string Interest { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Profile Photo")]
        public HttpPostedFileBase UserProfileImage { get; set; }
    }
}