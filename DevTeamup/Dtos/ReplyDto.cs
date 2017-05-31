using System;

namespace DevTeamup.Dtos
{
    public class ReplyDto
    {
        public string RepliedByName { get; set; }

        public string RepliedById { get; set; }

        public string Body { get; set; }

        public int DiscussionId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }
    }
}