using System;
using System.Collections.Generic;

namespace DevTeamup.Dtos
{
    public class DiscussionDto
    {
        public ApplicationUserDto PostedBy { get; set; }
 
        public string Body { get; set; }

        public TeamupDto Teamup { get; set; }
     
        public int TeamupId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<ReplyDto> Replies { get; set; }
    }
}