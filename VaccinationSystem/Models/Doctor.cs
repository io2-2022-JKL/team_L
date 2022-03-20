using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            DoctorArchivedAppointments = new HashSet<DoctorArchivedAppointment>();
            DoctorFutureAppointments = new HashSet<DoctorFutureAppointment>();
            TimeSlots = new HashSet<TimeSlot>();
            VaccCentersDoctors = new HashSet<VaccCentersDoctor>();
        }

        public int DoctorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int PatientId { get; set; }
        public bool Active { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual ICollection<DoctorArchivedAppointment> DoctorArchivedAppointments { get; set; }
        public virtual ICollection<DoctorFutureAppointment> DoctorFutureAppointments { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
        public virtual ICollection<VaccCentersDoctor> VaccCentersDoctors { get; set; }
    }
}
