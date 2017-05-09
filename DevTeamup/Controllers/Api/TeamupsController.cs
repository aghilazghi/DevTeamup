using DevTeamup.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Http;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    public class TeamupsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public TeamupsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var currentUserId = User.Identity.GetUserId();
            var teamup = _context.Teamups.Single(t => t.Id == id && t.OrganizerId == currentUserId);

            if (teamup.IsCanceled)
                return NotFound();

            teamup.IsCanceled = true;

            var notification = new Notification
            {
                DateTime = DateTime.Now,
                Teamup = teamup,
                NotificationType = NotificationType.TeamupCanceled
            };

            var contributors = _context.Collaborations
                .Where(c => c.TeamupId == teamup.Id)
                .Select(c => c.Contributor)
                .ToList();

            foreach (var contributor in contributors)
            {
                var userNotification = new UserNotification
                {
                    User = contributor,
                    Notification = notification
                };
                _context.UserNotifications.Add(userNotification);
            }           

            _context.SaveChanges();

            return Ok();
        }
    }
}
