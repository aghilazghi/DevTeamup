using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevTeamup.Models
{
    public class Favorite
    {
        public Teamup Teamup { get; set; }

        public ApplicationUser FavoringUser { get; set; }

        [Key]
        [Column(Order = 1)]
        public int TeamupId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string FavoringUserId { get; set; }
        
    }
}