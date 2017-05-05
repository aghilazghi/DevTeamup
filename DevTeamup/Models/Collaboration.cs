using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTeamup.Models
{
    public class Collaboration
    {
        public Teamup Teamup { get; set; }

        public ApplicationUser Contributor { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TeamupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ContributorId { get; set; }
    }
}