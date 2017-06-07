using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DevTeamup.ViewModels
{
    public class ExtendRegisterViewModel : RegisterViewModel
    {
        [Display(Name = "User Profile Photo")]
        public HttpPostedFileBase UserProfileImage { get; set; }
    }
}