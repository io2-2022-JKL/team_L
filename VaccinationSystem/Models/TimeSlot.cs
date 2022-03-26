using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class TimeSlot
    {
        public TimeSlot()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int TimeSlotId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int DoctorId { get; set; }
        public bool IsFree { get; set; }
        public bool Active { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
