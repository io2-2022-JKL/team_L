using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.DTOs
{
    public class CreateNewVisitRequest
    {
        [Required]
        public DateTime windowBegin { get; set; }
        [Required]
        public DateTime windowEnd { get; set; }
        [Required]
        public int timeSlotDurationInMinutes { get; set; }
    }
}
