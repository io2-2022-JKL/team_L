using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            DoctorArchivedAppointments = new HashSet<DoctorArchivedAppointment>();
            DoctorFutureAppointments = new HashSet<DoctorFutureAppointment>();
            PatientArchivedAppointments = new HashSet<PatientArchivedAppointment>();
            PatientFutureAppointments = new HashSet<PatientFutureAppointment>();
        }

        public int AppointmentId { get; set; }
        public int WhichDose { get; set; }
        public int TimeSlotId { get; set; }
        public int PatientId { get; set; }
        public int VaccineId { get; set; }
        public bool Completed { get; set; }
        public string VaccineBatchNumber { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual TimeSlot TimeSlot { get; set; }
        public virtual Vaccine Vaccine { get; set; }
        public virtual ICollection<DoctorArchivedAppointment> DoctorArchivedAppointments { get; set; }
        public virtual ICollection<DoctorFutureAppointment> DoctorFutureAppointments { get; set; }
        public virtual ICollection<PatientArchivedAppointment> PatientArchivedAppointments { get; set; }
        public virtual ICollection<PatientFutureAppointment> PatientFutureAppointments { get; set; }
    }
}
