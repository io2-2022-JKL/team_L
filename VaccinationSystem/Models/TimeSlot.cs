using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public class TimeSlot
    {
        [Key]
        public int id { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public virtual Doctor doctor { get; set; }
        public bool isFree { get; set; }
        public bool active { get; set; }
    }
}
