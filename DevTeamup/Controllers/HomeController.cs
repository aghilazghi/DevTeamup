using DevTeamup.Models;
using DevTeamup.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
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

        public ActionResult Index(string query = null)
        {

            var futureTeamups = _context.Teamups
                .Include(t => t.Organizer)
                .Include(t => t.DevelopmentLanguage)
                .Include(t => t.DevelopmentType)
                .Where(t => t.DateTime > DateTime.Now && !t.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                futureTeamups = futureTeamups
                    .Where(t =>
                        t.Organizer.FirstName.Contains(query) ||
                        t.Organizer.LastName.Contains(query) ||
                        t.DevelopmentLanguage.Name.Contains(query) ||
                        t.DevelopmentType.Name.Contains(query));
            }

            var viewModel = new TeamupsViewModel
            {
                FutureTeamups = futureTeamups,
                IsAuthenticated = User.Identity.IsAuthenticated,
                CurrentUserId = User.Identity.GetUserId(),
                Title = "Future Teamups",
                SearchTerm = query
            };

            return View("Teamups", viewModel);
        }

      
        public FileContentResult UserImages(string id = null)
        {
            if (!User.Identity.IsAuthenticated) return DefaultImageFile();

            var currentUserId = id ?? User.Identity.GetUserId();
            //var context = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
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