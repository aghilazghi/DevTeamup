using DevTeamup.Models;
using System;

namespace DevTeamup.Dtos
{
    public class NotificationDto
    {
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalAddress { get; set; }
        public TeamupDto Teamup { get; set; }

    }
}