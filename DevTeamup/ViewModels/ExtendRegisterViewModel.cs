using System.Web;

namespace DevTeamup.ViewModels
{
    public class ExtendRegisterViewModel : RegisterViewModel
    {
        public HttpPostedFileBase UserProfileImage { get; set; }
    }
}