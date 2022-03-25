using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public enum Virus
    {
        Coronavirus
    }
    public class Vaccine
    {
        [Key]
        public int id { get; set; }
        public string company { get; set; }
        public string name { get; set; }
        public int numberOfDoses { get; set; }
        public int minDaysBetweenDoses { get; set; }
        public int maxDaysBetweenDoses { get; set; }
        public Virus virus { get; set; }
        public int minPatientAge { get; set; }
        public int maxPatientAge { get; set; }
        public bool used { get; set; }

        // metody
    }
}
