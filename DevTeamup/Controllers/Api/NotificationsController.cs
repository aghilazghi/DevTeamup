using AutoMapper;
using DevTeamup.Dtos;
using DevTeamup.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace DevTeamup.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();  
        }

        public IEnumerable<NotificationDto> GetNewNotifications()
        {
            var currentUserId = User.Identity.GetUserId();

            var notifications = _context.UserNotifications
                .Where(u => u.UserId == currentUserId && !u.IsRead)
                .Select(u => u.Notification)
                .Include(n => n.Teamup.Organizer)
                .ToList();

            return notifications.Select(Mapper.Map<Notification, NotificationDto>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var currentUserId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(u => u.UserId == currentUserId && !u.IsRead)
                .ToList();

            notifications.ForEach(n => n.Read());

            _context.SaveChanges();

            return Ok();
        }
    }
}
