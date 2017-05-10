using System;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class Notification
    {
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType NotificationType { get; private set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalAdsress { get; set; }

        [Required]
        public Teamup Teamup { get; private set; }

        public Notification(Teamup teamup, NotificationType notificationType)
        {
            if (teamup == null)
                throw new ArgumentNullException(nameof(teamup));

            Teamup = teamup;
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }

        protected Notification()
        {
            
        }
    }
}