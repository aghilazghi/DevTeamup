using System.Data.Entity;
using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace DevTeamup.Controllers
{
    public class TeamupsController : Controller
    {
        private ApplicationDbContext _context;

        public TeamupsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Mine()
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Teamups
                .Where(t => t.OrganizerId == currentUserId)
                .Include(t => t.Organizer)
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .ToList();

            return View(teamups);
        }

        [Authorize]
        public ActionResult Favoring()
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Favorites
                .Where(c => c.FavoringUserId == currentUserId)
                .Select(c => c.Teamup)
                .Include(c => c.Organizer)
                .Include(c => c.DevelopmentLanguage)
                .Include(c => c.DevelopmentType)
                .ToList();

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = teamups,
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = currentUserId,
                Title = "My Favorite Teamups"
            };
            return View("Teamups", viewModel);
        }

        [Authorize]
        public ActionResult Contributing()
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Collaborations
                .Where(c => c.ContributorId == currentUserId)
                .Select(c => c.Teamup)
                .Include(c => c.Organizer)
                .Include(c => c.DevelopmentLanguage)
                .Include(c => c.DevelopmentType)
                .ToList();

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = teamups,
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = currentUserId,
                Title = "Teamups I'm Contributing"
            };

            return View("Teamups", viewModel);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new TeamupFormViewModel
            {
                DevelopmentLanguages = _context.DevelopmentLanguages.ToList(),
                DevelopmentTypes = _context.DevelopmentTypes.ToList(),
                Title = "Create a Teamup"
            };

            return View("TeamupForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var teamup = _context.Teamups.Single(t => t.Id == id && t.OrganizerId == currentUserId);

            var viewModel = new TeamupFormViewModel
            {
                DevelopmentLanguages = _context.DevelopmentLanguages.ToList(),
                DevelopmentTypes = _context.DevelopmentTypes.ToList(),
                Date = teamup.DateTime.ToString("MMM d yyyy"),
                Time = teamup.DateTime.ToString("HH:mm"),
                Address = teamup.Address,
                DevelopmentLanguage = teamup.DevelopmentLanguageId,
                DevelopmentType = teamup.DevelopmentTypeId,
                Description = teamup.Description,
                Id = teamup.Id,
                Title = "Update a Teamup"
            };

            return View("TeamupForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TeamupFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DevelopmentLanguages = _context.DevelopmentLanguages.ToList();
                viewModel.DevelopmentTypes = _context.DevelopmentTypes.ToList();

                return View("TeamupForm", viewModel);
            }

            var teamup = new Teamup
            {
                OrganizerId = User.Identity.GetUserId(),
                Address = viewModel.Address,
                DateTime = viewModel.GetDateTime(),
                DevelopmentLanguageId = viewModel.DevelopmentLanguage,
                DevelopmentTypeId = viewModel.DevelopmentType,
                Description = viewModel.Description,
            };

            _context.Teamups.Add(teamup);
            _context.SaveChanges();

            return RedirectToAction("Mine", "Teamups");
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(TeamupFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.DevelopmentLanguages = _context.DevelopmentLanguages.ToList();
                viewModel.DevelopmentTypes = _context.DevelopmentTypes.ToList();

                return View("TeamupForm", viewModel);
            }

            var currentUserId = User.Identity.GetUserId();
            var teamup = _context.Teamups.Single(t => t.Id == viewModel.Id && t.OrganizerId == currentUserId);

            teamup.Address = viewModel.Address;
            teamup.DateTime = viewModel.GetDateTime();
            teamup.Description = viewModel.Description;
            teamup.DevelopmentLanguageId = viewModel.DevelopmentLanguage;
            teamup.DevelopmentTypeId = viewModel.DevelopmentType;

            _context.SaveChanges();

            return RedirectToAction("Mine", "Teamups");
        }
    }
}