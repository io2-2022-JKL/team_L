using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Validation;

namespace VaccinationSystem.Models
{
    public class RegisteringPatient
    {
        [Required]
        [StringLength(11)]
        public string PESEL { get; set; }

        [Required]
        [MinLength(1)]
        public string Names { get; set; }
        [Required]
        [MinLength(1)]
        public string Surname { get; set; }
        [Required]
        [Mail]
        public string Mail { get; set; }
        [Required]
        [Password]
        public string Password { get; set; }
        [Required]
        [MinLength(1)]
        public string PhoneNumber { get; set; }
    }
}
