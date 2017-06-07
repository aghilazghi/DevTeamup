using System.Collections.Generic;
using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using DevTeamup.Infrastructure;

namespace DevTeamup.Controllers
{
    [Authorize]
    public class TeamupsController : Controller
    {
        private ApplicationDbContext _context;
        private const int TeamupsPerPage = 6;

        public TeamupsController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ActionResult Mine()
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Teamups
                .Where(t => t.OrganizerId == currentUserId && !t.IsCanceled)
                .Include(t => t.Organizer)
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .ToList();

            return View(teamups);
        }

        
        public ActionResult Favoring(int page = 1)
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Favorites
                .Where(c => c.FavoringUserId == currentUserId)
                .Select(c => c.Teamup)
                .Include(c => c.Organizer)
                .Include(c => c.DevelopmentLanguage)
                .Include(c => c.DevelopmentType)
                .OrderByDescending(t => t.CreatedOn)
                .Skip((page - 1) * TeamupsPerPage)
                .Take(TeamupsPerPage)
                .ToList();

            var totalTeamupsCount = teamups.Count;

            teamups = CurrentTeamupPage(page, teamups);

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = new PagedData<Teamup>(teamups, totalTeamupsCount, page, TeamupsPerPage),
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = currentUserId,
                Title = "My Favorite Teamups"
            };
            return View("Teamups", viewModel);

        }

        
        public ActionResult Contributing(int page = 1)
        {
            var currentUserId = User.Identity.GetUserId();
            var teamups = _context.Collaborations
                .Where(c => c.ContributorId == currentUserId)
                .Select(c => c.Teamup)
                .Include(c => c.Organizer)
                .Include(c => c.DevelopmentLanguage)
                .Include(c => c.DevelopmentType)
                .ToList();

            var totalTeamupsCount = teamups.Count;

            teamups = CurrentTeamupPage(page, teamups);

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = new PagedData<Teamup>(teamups, totalTeamupsCount, page, TeamupsPerPage),
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = currentUserId,
                Title = "Teamups I'm Contributing",
            };

            return View("Teamups", viewModel);

        }

        private static List<Teamup> CurrentTeamupPage(int page, IEnumerable<Teamup> teamups)
        {
            // get teamups for current page
            return teamups
                .OrderByDescending(t => t.CreatedOn)
                .Skip((page - 1) * TeamupsPerPage)
                .Take(TeamupsPerPage)
                .ToList();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(TeamupsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        
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
            var teamup = _context.Teamups
                .Include(t => t.Collaborations.Select(c => c.Contributor))
                .Single(t => t.Id == viewModel.Id && t.OrganizerId == currentUserId);

            teamup.Modify(viewModel);

            _context.SaveChanges();

            return RedirectToAction("Mine", "Teamups");
        }

        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var teamup = _context.Teamups
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .Include(t => t.Organizer)
                .SingleOrDefault(t => t.Id == id);

            if (teamup == null)
                return HttpNotFound();

            var viewModel = new TeamupDetailViewModel {Teamup = teamup};

            if (!User.Identity.IsAuthenticated) return View("Details", viewModel);

            var currentUserId = User.Identity.GetUserId();

            viewModel.CurrentUserId = currentUserId;

            viewModel.IsContributing = _context.Collaborations
                .Any(c => c.TeamupId == teamup.Id && c.ContributorId == currentUserId);

            viewModel.IsFavoring = _context.Favorites
                .Any(f => f.TeamupId == teamup.Id && f.FavoringUserId == currentUserId);

            return View("Details", viewModel);
        }
    }
}