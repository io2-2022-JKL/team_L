using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.DTOs
{
    public class CreateNewVisitRequest
    {
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public int timeSlotDurationInMinutes { get; set; }
    }
}
