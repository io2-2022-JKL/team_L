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
        public Guid patientId { get; set; }
        [Required]
        public Guid vaccinationCenterId { get; set; }
    }
}
