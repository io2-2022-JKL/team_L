using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Vaccine
    {
        public Vaccine()
        {
            Appointments = new HashSet<Appointment>();
            VaccCentersVaccines = new HashSet<VaccCentersVaccine>();
        }

        public int VaccineId { get; set; }
        public string VaccineName { get; set; }
        public string Company { get; set; }
        public int NumberOfDoses { get; set; }
        public int? MinDaysBetweenDoses { get; set; }
        public int? MaxDaysBetweenDoses { get; set; }
        public int VirusId { get; set; }
        public int MinPatientAge { get; set; }
        public int? MaxPatientAge { get; set; }
        public bool Used { get; set; }

        public virtual Virus Virus { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<VaccCentersVaccine> VaccCentersVaccines { get; set; }
    }
}
