using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.DTOs
{
    public class WholeTimeSlotsResponse
    {
        [Required]
        public Guid id { get; set; }
        [Required]
        public string from { get; set; }
        [Required]
        public string to { get; set; }
        [Required]
        public bool isFree { get; set; }
        [Required]
        public bool active { get; set; }

    }
}
