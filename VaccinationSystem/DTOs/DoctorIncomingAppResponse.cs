using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class DoctorIncomingAppResponse
    {
        [Required]
        public string vaccineName { get; set; }
        [Required]
        public string vaccineCompany { get; set; }
        [Required]
        public string vaccineVirus { get; set; }
        [Required]
        public int whichVaccineDose { get; set; }
        [Required]
        public Guid appointmentId { get; set; }
        [Required]
        public string from { get; set; }
        [Required]
        public string to { get; set; }
        [Required]
        public string patientFirstName { get; set; }
        [Required]
        public string patientLastName { get; set; }
    }
}