using System;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType NotificationType { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public string OriginalAdsress { get; set; }

        [Required]
        public Teamup Teamup { get; set; }
    }
}