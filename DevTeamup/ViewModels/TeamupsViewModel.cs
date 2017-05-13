using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevTeamup.Models;

namespace DevTeamup.ViewModels
{
    public class TeamupsViewModel
    {
        public IEnumerable<Teamup> FutureTeamups { get; set; }
        public bool IsAuthenticated { get; set; }
        public string CurrentUserId { get; set; }
        public string Title { get; set; }
        public string SearchTerm { get; set; }
    }
}