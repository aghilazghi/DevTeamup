using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace DevTeamup.ViewModels
{
    public class TimeFormatValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "HH:mm",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, 
                out dateTime
            );

            return isValid 
                ? ValidationResult.Success 
                : new ValidationResult("The time must be in 24 hours format: 15:45 instead of 3:45");
        }
    }
}