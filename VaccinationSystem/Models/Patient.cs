using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Patient : User
    {
        public virtual Dictionary<Virus, int> vaccinationsCount { get; set; }
        public virtual ICollection<Appointment> vaccinationHistory { get; set; }
        public virtual ICollection<Appointment> futureVaccinations { get; set; }
        public virtual ICollection<Certificate> certificates { get; set; }
        public bool active { get; set; }

    }
}
