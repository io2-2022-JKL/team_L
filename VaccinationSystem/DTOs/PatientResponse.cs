using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class PatientResponse
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string PESEL { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string dateOfBirth { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public bool active { get; set; }
    }
}

