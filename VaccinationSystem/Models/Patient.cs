using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public class Patient : User
    {

        //public virtual Dictionary<Virus, int> vaccinationsCount { get; set; }
        public virtual IEnumerable<Appointment> vaccinationHistory { get; set; }
        public virtual IEnumerable<Appointment> futureVaccinations { get; set; }
        public virtual IEnumerable<Certificate> certificates { get; set; }
        public bool active { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<PatientArchivedAppointment> PatientArchivedAppointments { get; set; }
        public virtual ICollection<PatientFutureAppointment> PatientFutureAppointments { get; set; }
        public virtual ICollection<PatientsCertificate> PatientsCertificates { get; set; }
        public virtual ICollection<VaccinationCount> VaccinationCounts { get; set; }
    }
}
