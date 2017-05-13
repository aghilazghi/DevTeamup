using System;
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

        public NotificationResultDto GetNewNotifications()
        {
            var currentUserId = User.Identity.GetUserId();
            var today = DateTime.Now;

            var userNotifications = _context.UserNotifications
                .Where(u => u.UserId == currentUserId);

            var unreadNotifications = userNotifications
                .Where(u => !u.IsRead)
                .ToList()
                .Count;     

            var notifications = userNotifications
                .Select(u => u.Notification)
                .Include(n => n.Teamup.Organizer)
                .Where(n => DbFunctions.DiffDays(n.CreatedOn, today) <= 30)
                .ToList();

            var notificationResultDto = new NotificationResultDto
            {
                Unread = unreadNotifications,
                All = notifications.Select(Mapper.Map<Notification, NotificationDto>)
            };

            return notificationResultDto;
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
