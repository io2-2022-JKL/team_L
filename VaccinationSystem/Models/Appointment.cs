using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public enum AppointmentState
    {
        Cancelled,
        Planned,
        Finished
    }

    public class Appointment
    {
        [Key]
        public int id { get; set; }
        public int whichDose { get; set; }
        public virtual TimeSlot timeSlot { get; set; }
        public virtual Patient patient { get; set; }
        public virtual Vaccine vaccine { get; set; }
        public AppointmentState state { get; set; }
        public string vaccineBatchNumber { get; set; }
    }
}
