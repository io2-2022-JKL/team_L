using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Doctor : User
    {
        public VaccinationCenter vaccinationCenter { get; set; }
        public List<Appointment> vaccinationsArchive { get; set; }
        public List<Appointment> futureVaccinations { get; set; }
        public Patient patientAccount { get; set; }
        public bool active { get; set; }

        // metody
    }
}
