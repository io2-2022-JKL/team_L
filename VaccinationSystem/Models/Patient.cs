using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Patient : User
    {

        //public virtual Dictionary<Virus, int> vaccinationsCount { get; set; }
        public virtual IEnumerable<Appointment> vaccinationHistory { get; set; }
        public virtual IEnumerable<Appointment> futureVaccinations { get; set; }
        public virtual IEnumerable<Certificate> certificates { get; set; }
        public bool active { get; set; }

    }
}
