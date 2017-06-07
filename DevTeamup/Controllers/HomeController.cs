using DevTeamup.Infrastructure;
using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace DevTeamup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private const int TeamupsPerPage = 6;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index(string query = null, int page = 1)
        {

            var upcomingTeamups = _context.Teamups
                .Include(t => t.Organizer)
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .Where(t => t.DateTime > DateTime.Now && !t.IsCanceled)
                .ToList();

            var totalTeamupsCount = upcomingTeamups.Count;

            if (!String.IsNullOrWhiteSpace(query))
            {
                var queriedTeamups = upcomingTeamups.Where(t =>
                        t.Organizer.FirstName.Contains(query) ||
                        t.Organizer.LastName.Contains(query) ||
                        t.DevelopmentLanguage.Name.Contains(query) ||
                        t.DevelopmentType.Name.Contains(query))
                    .ToList();

                totalTeamupsCount = queriedTeamups.Count;
                upcomingTeamups = CurrentTeamupPage(page, queriedTeamups);
            }
            else
            {
                upcomingTeamups = CurrentTeamupPage(page, upcomingTeamups);
            }

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = new PagedData<Teamup>(upcomingTeamups, totalTeamupsCount, page, TeamupsPerPage),
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = User.Identity.GetUserId(),
                Title = "Upcoming Teamups",
                SearchTerm = query
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

        public FileContentResult UserImages(string id = null)
        {
            if (!User.Identity.IsAuthenticated) return DefaultImageFile();

            var currentUserId = id ?? User.Identity.GetUserId();
            var image = _context.Users.FirstOrDefault(u => u.Id == currentUserId && u.UserImage != null);

            return image != null ? new FileContentResult(image.UserImage, "image/jpeg") : DefaultImageFile();
        }

        private FileContentResult DefaultImageFile()
        {
            var fileName = HttpContext.Server.MapPath(@"~/Images/default.png");
            var fileInfo = new FileInfo(fileName);
            var imageLength = fileInfo.Length;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var binaryReader = new BinaryReader(fileStream);
            var imageData = binaryReader.ReadBytes((int) imageLength);
            return File(imageData, "Image/png");
        }
    }
}