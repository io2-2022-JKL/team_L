using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Validation;

namespace VaccinationSystem.Models
{
    public class RegisteringDoctor
    {
        [Required]
        [StringLength(11)]
        public string PESEL { get; set; }

        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        [Mail]
        public string mail { get; set; }
        [Required]
        [Password]
        public string password { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public Guid vaccinationCenterId { get; set; }
    }
}
