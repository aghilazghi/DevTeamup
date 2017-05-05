using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DevTeamup.ViewModels
{
    public class DateFormatValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dateTime;

            var isValid = DateTime.TryParseExact(
                Convert.ToString(value),
                "MMM d yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, 
                out dateTime
                );

            return isValid 
                ? ValidationResult.Success 
                : new ValidationResult("The date format must be like: Jan 1 2017");
        }
    }
}