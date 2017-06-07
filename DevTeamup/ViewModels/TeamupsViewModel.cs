using System.Collections.Generic;
using DevTeamup.Infrastructure;
using DevTeamup.Models;

namespace DevTeamup.ViewModels
{
    public class TeamupsViewModel
    {
        public PagedData<Teamup> FutureTeamups { get; set; }
        public bool IsAuthenticated { get; set; }
        public string CurrentUserId { get; set; }
        public string Title { get; set; }
        public string SearchTerm { get; set; }
    }
}