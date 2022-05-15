using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class StartVaccinationResponse
    {
        [Required]
        public string vaccineName { get; set; }
        [Required]
        public string vaccineCompany { get; set; }
        [Required]
        public int numberOfDoses { get; set; }
        [Required]
        public int minDaysBetweenDoses { get; set; }
        [Required]
        public int maxDaysBetweenDoses { get; set; }
        [Required]
        public string virusName { get; set; }
        [Required]
        public int minPatientAge { get; set; }
        [Required]
        public int maxPatientAge { get; set; }
        [Required]
        public string patientFirstName { get; set; }
        [Required]
        public string patientLastName { get; set; }
        [Required]
        public string PESEL { get; set; }
        [Required]
        public string dateOfBirth { get; set; }
        [Required]
        public string from { get; set; }
        [Required]
        public string to { get; set; }
    }
}