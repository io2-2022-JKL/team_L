using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class DoctorResponse
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
        [Required]
        public Guid vaccinationCenterId { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string street { get; set; }
    }
}

