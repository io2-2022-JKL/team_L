using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class PatientFutureAppointment
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
