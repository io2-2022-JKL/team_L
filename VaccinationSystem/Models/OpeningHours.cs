using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class OpeningHours
    {
        [Key]
        public int id { get; set; }
        public TimeSpan from { get; set; }
        public TimeSpan to { get; set; }
    }
}
