using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevTeamup.Models
{
    public class Teamup
    {
        public int Id { get; set; }

        public ApplicationUser Organizer { get; set; }

        [Required]
        public string OrganizerId { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public DateTime DateTime { get; set; }

        public DevelopmentType DevelopmentType { get; set; }

        [Required]
        public byte DevelopmentTypeId { get; set; }

        public DevelopmentLanguage DevelopmentLanguage { get; set; }

        [Required]
        public byte DevelopmentLanguageId { get; set; }

        [Required]
        [StringLength(1024)]
        public string Description { get; set; }

        public bool IsCanceled { get; private set; }

        public ICollection<Collaboration> Collaborations { get; private set; }

        public Teamup()
        {
            Collaborations = new Collection<Collaboration>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            foreach (var contributor in Collaborations.Select(c => c.Contributor))
            {
                contributor.Notify(new Notification(this, NotificationType.TeamupCanceled));

            }
        }
    }
}