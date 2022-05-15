using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.DTOs
{
    public class CityResponse
    {
        [Required]
        public string city { get; set; }
    }
}
