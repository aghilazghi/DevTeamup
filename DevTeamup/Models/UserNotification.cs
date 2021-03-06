using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTeamup.Models
{
    public class UserNotification : ITimeStamp
    {

        [Key]
        [Column(Order = 1)]
        public string UserId { get; private set; }

        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; private set; }

        public ApplicationUser User { get; private set ; }

        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if(user == null)
                throw new ArgumentNullException(nameof(user));

            if (notification == null)
                throw new ArgumentNullException(nameof(notification));

            User = user;
            Notification = notification;
        }

        protected UserNotification()
        {
            
        }

        public void Read()
        {
            IsRead = true;
        }
    }
}