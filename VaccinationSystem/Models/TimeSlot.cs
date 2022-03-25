using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class TimeSlot
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public Doctor doctor { get; set; }
        public bool isFree { get; set; }
        public bool active { get; set; }
    }
}
