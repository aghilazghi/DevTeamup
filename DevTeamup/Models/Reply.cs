using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTeamup.Models
{
    public class Reply : ITimeStamp
    {
        public int Id { get; set; }

        [Required]
        public string RepliedBy { get; set; }

        [Required]
        [StringLength(250)]
        public string Body { get; set; }

        public Discussion Discussion { get; set; }

        [Required]
        public int DiscussionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}