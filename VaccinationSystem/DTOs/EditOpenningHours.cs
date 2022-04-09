using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.Models
{
    public class OpeningHoursDays
    {
        [Required]
        public string from { get; set; }
        [Required]
        public string to { get; set; }
    }
}
