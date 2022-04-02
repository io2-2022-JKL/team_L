using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.Models
{
    public class EditedVaccinationCenter
    {
        [Required]
        public Guid VaccinationCentersId { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [MinLength(1)]
        public string City { get; set; }
        [MinLength(1)]
        public string Street { get; set; }
    }
}
