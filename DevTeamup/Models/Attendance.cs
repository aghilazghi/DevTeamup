using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTeamup.Models
{
    public class Attendance
    {
        public Teamup Teamup { get; set; }

        public ApplicationUser Attendee { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TeamupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string AttendeeId { get; set; }
    }
}