using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class CertificatesResponse
    {
        [Required]
        public string url { get; set; }
        [Required]
        public string vaccineName { get; set; }
        [Required]
        public string vaccineCompany { get; set; }
        [Required]
        public string virusType { get; set; } // string or Virus?
    }
}
