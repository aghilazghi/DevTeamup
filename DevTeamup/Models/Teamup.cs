using DevTeamup.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DevTeamup.Models
{
    public class Teamup : ITimeStamp
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
        [StringLength(250)]
        public string Description { get; set; }

        public bool IsCanceled { get; private set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public ICollection<Collaboration> Collaborations { get; private set; }

        public ICollection<Discussion> Discussions { get; set; }

        public Teamup()
        {
            Collaborations = new Collection<Collaboration>();
            Discussions = new Collection<Discussion>();
        }

        public void Cancel()
        {
            IsCanceled = true;

            var notification = Notification.TeamupCanceled(this);

            foreach (var contributor in Collaborations.Select(c => c.Contributor))
                contributor.Notify(notification);

        }

        public void Modify(TeamupFormViewModel viewModel)
        {
            var notification = Notification.TeamupModified(this, DateTime, Address);

            Address = viewModel.Address;
            DateTime = viewModel.GetDateTime();
            Description = viewModel.Description;
            DevelopmentLanguageId = viewModel.DevelopmentLanguage;
            DevelopmentTypeId = viewModel.DevelopmentType;

            foreach (var contributor in Collaborations.Select(c => c.Contributor))
                contributor.Notify(notification);         
        }
    }
}