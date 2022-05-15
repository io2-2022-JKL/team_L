using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.DTOs
{
    public class CreateNewVisitRequest
    {
        public string windowBegin { get; set; }
        public string windowEnd { get; set; }
        public int timeSlotDurationInMinutes { get; set; }
    }
}
