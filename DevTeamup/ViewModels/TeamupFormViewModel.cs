using DevTeamup.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.ViewModels
{
    public class TeamupFormViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [FutureDateValidation]
        [DateFormatValidation]
        public string Date { get; set; }

        [Required]
        [TimeFormatValidation]
        [TimeRangeValidation]
        public string Time { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Language")]
        public byte DevelopmentLanguage { get; set; }

        [Required]
        [Display(Name = "Development Type")]
        public byte DevelopmentType { get; set; }

        public string Title { get; set; }

        public IEnumerable<DevelopmentLanguage> DevelopmentLanguages { get; set; }

        public IEnumerable<DevelopmentType> DevelopmentTypes { get; set; }

        public string Action => Id != 0 ? CrudAction.Update : CrudAction.Create;

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }

}