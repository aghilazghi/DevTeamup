using System;

namespace DevTeamup.Dtos
{
    public class TeamupDto
    {
        public int Id { get; set; }
        public ApplicationUserDto Organizer { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public DevelopmentTypeDto DevelopmentType { get; set; }
        public DevelopmentLanguageDto DevelopmentLanguage { get; set; }
        public string Description { get; set; }
        public bool IsCanceled { get; set; }
    }
}