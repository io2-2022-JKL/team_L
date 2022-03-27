using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable

namespace VaccinationSystem.Models
{
    public class Doctor : User
    {

        public VaccinationCenter vaccinationCenter { get; set; }
        public virtual IEnumerable<Appointment> vaccinationsArchive { get; set; }
        public virtual IEnumerable<Appointment> futureVaccinations { get; set; }
        public virtual Patient patientAccount { get; set; }
        public bool active { get; set; }
    }
}
