using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            Doctors = new HashSet<Doctor>();
            PatientArchivedAppointments = new HashSet<PatientArchivedAppointment>();
            PatientFutureAppointments = new HashSet<PatientFutureAppointment>();
            PatientsCertificates = new HashSet<PatientsCertificate>();
            VaccinationCounts = new HashSet<VaccinationCount>();
        }

        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<PatientArchivedAppointment> PatientArchivedAppointments { get; set; }
        public virtual ICollection<PatientFutureAppointment> PatientFutureAppointments { get; set; }
        public virtual ICollection<PatientsCertificate> PatientsCertificates { get; set; }
        public virtual ICollection<VaccinationCount> VaccinationCounts { get; set; }
    }
}
