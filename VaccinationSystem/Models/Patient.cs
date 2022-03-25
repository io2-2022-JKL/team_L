using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Patient : User
    {
        public Dictionary<Virus, int> vaccinationsCount { get; set; }
        public List<Appointment> vaccinationHistory { get; set; }
        public List<Appointment> futureVaccinations { get; set; }
        public List<Certificate> certificates { get; set; }
        public bool active { get; set; }

        // metody
    }
}
