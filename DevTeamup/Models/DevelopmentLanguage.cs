using System.ComponentModel.DataAnnotations;

namespace DevTeamup.Models
{
    public class DevelopmentLanguage
    {
        public byte Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}