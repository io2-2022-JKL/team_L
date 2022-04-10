using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class AddVaccinationCenterRequest
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [MinLength(1)]
        public string City { get; set; }
        [Required]
        [MinLength(1)]
        public string Street { get; set; }
        [Required]
        public List<Guid> VaccineIds { get; set; } = new List<Guid>();
        [Required]
        public OpeningHoursDays[] openingHoursDays { get; set; } = new OpeningHoursDays[7];
        [Required]
        public bool Active { get; set; }
    }
}
