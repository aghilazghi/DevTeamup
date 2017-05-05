using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class DevelopmentType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}