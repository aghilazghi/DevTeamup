using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class Discussion : ITimeStamp
    {
        public int Id { get; set; }

        public ApplicationUser PostedBy{ get; set; }

        [Required]
        public string PostedById { get; set; }

        [Required]
        [StringLength(250)]
        public string Body { get; set; }

        public Teamup Teamup { get; set; }

        [Required]
        public int TeamupId { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public Discussion()
        {
            Replies = new Collection<Reply>();
        }

    }
}