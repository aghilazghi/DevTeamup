using DevTeamup.Models;
using System;

namespace DevTeamup.Dtos
{
    public class ReplyDto
    {
        public ApplicationUser RepliedBy { get; set; }

        public string Body { get; set; }

        public Discussion Discussion { get; set; }

        public int DiscussionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}