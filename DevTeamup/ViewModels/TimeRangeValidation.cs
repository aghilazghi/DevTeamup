using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DevTeamup.ViewModels
{
    public class TimeRangeValidation : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            TimeSpan outTimeSpan;
            var starTimeSpan = new TimeSpan(8, 0, 0);
            var endTimeSpan = new TimeSpan(18, 0, 0);

            var isValid = TimeSpan.TryParse(Convert.ToString(value), out outTimeSpan);

            return isValid && outTimeSpan >= starTimeSpan && outTimeSpan <= endTimeSpan
                ? ValidationResult.Success
                : new ValidationResult("The time must be between 08:00 and 18:00 inclusive");
        }
    }
}