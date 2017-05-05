using System.ComponentModel.DataAnnotations;

namespace DevTeamup.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}