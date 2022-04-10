using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.DTOs
{
    public class CreateNewVisitRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int TimeSlotDurationInMinutes { get; set; }
    }
}
