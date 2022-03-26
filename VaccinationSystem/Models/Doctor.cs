using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public class Doctor : User
    {
        public Doctor()
        {
            this.vaccinationsArchive = new HashSet<Appointment>();
            this.futureVaccinations = new HashSet<Appointment>();
        }
        public VaccinationCenter vaccinationCenter { get; set; }
        public virtual ICollection<Appointment> vaccinationsArchive { get; set; }
        public virtual ICollection<Appointment> futureVaccinations { get; set; }
        public virtual Patient patientAccount { get; set; }
        public bool active { get; set; }
    }
}
