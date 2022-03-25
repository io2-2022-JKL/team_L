using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Appointment
    {
        public int whichDose { get; set; }
        public TimeSlot timeSlot { get; set; }
        public Patient patient { get; set; }
        public Vaccine vaccine { get; set; }
        public bool completed { get; set; }
        public string vaccineBatchNumber { get; set; }
    }
}
