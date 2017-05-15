using DevTeamup.Models;

namespace DevTeamup.ViewModels
{
    public class TeamupDetailViewModel
    {
        public Teamup Teamup { get; set; }
        public string CurrentUserId { get; set; }
        public bool IsContributing { get; set; }
        public bool IsFavoring { get; set; }
    }
}