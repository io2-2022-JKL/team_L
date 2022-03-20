using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class DoctorArchivedAppointment
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
