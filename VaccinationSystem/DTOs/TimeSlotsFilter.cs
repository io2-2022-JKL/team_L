using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.DTOs
{
    public class TimeSlotsFilter
    {
        [Required]
        public string city { get; set; }
        [Required]
        public string dateFrom { get; set; }
        [Required]
        public string dateTo { get; set; }
        [Required]
        public string virus { get; set; }

    }
}
