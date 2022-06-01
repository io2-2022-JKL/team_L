using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.DTOs
{
    public class EditVaccine
    {
        [Required]
        public Guid vaccineId { get; set; }
        [Required]
        public string company { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int numberOfDoses { get; set; }
        [Required]
        public int minDaysBetweenDoses { get; set; }
        [Required]
        public int maxDaysBetweenDoses { get; set; }
        [Required]
        public string virus { get; set; }
        [Required]
        public int minPatientAge { get; set; }
        [Required]
        public int maxPatientAge { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
