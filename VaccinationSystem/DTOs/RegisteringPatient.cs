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
        public string name { get; set; }
        [Required]
        [MinLength(1)]
        public string surname { get; set; }
        [Required]
        [Mail]
        public string mail { get; set; }
        [Required]
        [Password]
        public string password { get; set; }
        [Required]
        [MinLength(1)]
        public string phoneNumber { get; set; }
    }
}
