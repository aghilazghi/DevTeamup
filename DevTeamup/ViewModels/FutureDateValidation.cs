using System;
using System.ComponentModel.DataAnnotations;

namespace DevTeamup.ViewModels
{
    public class FutureDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParse(Convert.ToString(value), out dateTime);

            return isValid && dateTime > DateTime.Now
                ? ValidationResult.Success
                : new ValidationResult("You must enter future date");
        }
    }
}