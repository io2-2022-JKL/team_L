using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class DoctorInfoResponse
    {
        [Required]
        public Guid vaccinationCenterId { get; set; }
        [Required]
        public string vaccinationCenterName { get; set; }
        [Required]
        public string vaccinationCenterCity { get; set; }
        [Required]
        public string vaccinationCenterStreet { get; set; }
        [Required]
        public Guid patientAccountId { get; set; }
    }
}