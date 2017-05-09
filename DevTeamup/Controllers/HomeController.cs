using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace DevTeamup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {

            var futureTeamups = _context.Teamups
                .Include(t => t.Organizer)
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .Where(t => t.DateTime > DateTime.Now && !t.IsCanceled)
                .OrderBy(t => t.DateTime)
                .ToList();

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = futureTeamups,
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = User.Identity.GetUserId(),
                Title = "Future Teamups"
            };

            return View("Teamups", viewModel);
        }
    }
}