using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DevTeamup.Dtos
{
    public class DiscussionDto
    {

        public int Id { get; set; }

        public string PostedBy { get; set; }
 
        public string Body { get; set; }

        public TeamupDto Teamup { get; set; }

        public int TeamupId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<ReplyDto> Replies { get; set; }
    }
}