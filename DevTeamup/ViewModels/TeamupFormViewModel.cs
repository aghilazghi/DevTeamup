using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DevTeamup.Models;

namespace DevTeamup.ViewModels
{
    public class TeamupFormViewModel
    {
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

        public IEnumerable<DevelopmentLanguage> DevelopmentLanguages { get; set; }

        public IEnumerable<DevelopmentType> DevelopmentTypes { get; set; }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}