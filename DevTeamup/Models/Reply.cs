using System;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class Reply
    {
        public int Id { get; set; }

        public ApplicationUser RepliedBy { get; set; }

        [Required]
        public string RepliedById { get; set; }

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