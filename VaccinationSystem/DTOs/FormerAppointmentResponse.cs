using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class FormerAppointmentResponse
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
        public string windowBegin { get; set; }
        [Required]
        public string windowEnd { get; set; }
        [Required]
        public string vaccinationCenterName { get; set; }
        [Required]
        public string vaccinationCenterCity { get; set; }
        [Required]
        public string vaccinationCenterStreet { get; set; }
        [Required]
        public string doctorFirstName { get; set; }
        [Required]
        public string doctorLastName { get; set; }
        [Required]
        public string visitState { get; set; }
    }
}