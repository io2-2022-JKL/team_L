using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.Validation
{
    public class MailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value as string;

            if (password.Length < 6)
                return new ValidationResult("Mail too short");

            return ValidationResult.Success;
        }
    }
}
