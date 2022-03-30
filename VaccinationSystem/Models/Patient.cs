using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Patient : User
    {

        //public virtual Dictionary<Virus, int> vaccinationsCount { get; set; }
        public IEnumerable<Appointment> vaccinationHistory { get; set; }
        public IEnumerable<Appointment> futureVaccinations { get; set; }
        public IEnumerable<Certificate> certificates { get; set; }
        [Required]
        public bool active { get; set; }

    }
}
