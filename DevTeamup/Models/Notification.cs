using System;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class Notification : ITimeStamp
    {
        public int Id { get; private set; }

        public DateTime DateTime { get; private set; }

        public NotificationType NotificationType { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalAddress { get; private set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        [Required]
        public Teamup Teamup { get; private set; }

        private Notification(Teamup teamup, NotificationType notificationType)
        {
            if (teamup == null)
                throw new ArgumentNullException(nameof(teamup));

            Teamup = teamup;
            NotificationType = notificationType;
            DateTime = DateTime.Now;
        }

        public static Notification TeamupCreated(Teamup teamup)
        {
            return new Notification(teamup, NotificationType.TeamupCreated);
        }

        public static Notification TeamupCanceled(Teamup teamup)
        {
            return new Notification(teamup, NotificationType.TeamupCanceled);
        }

        public static Notification TeamupModified(Teamup newTeamup, DateTime originalDateTime, string originalAddress)
        {
            var notification = new Notification(newTeamup, NotificationType.TeamupUpdated)
            {
                    OriginalDateTime = originalDateTime,
                    OriginalAddress = originalAddress
            };

            return notification;
        }

        protected Notification()
        {
            
        }

    }
}