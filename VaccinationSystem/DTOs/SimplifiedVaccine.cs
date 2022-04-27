using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class SimplifiedVaccine
    {
        public Guid vaccineId { get; set; }
        public string company { get; set; }
        public string name { get; set; }
        public int numberOfDoses { get; set; }
        public int minDaysBetweenDoses { get; set; }
        public int maxDaysBetweenDoses { get; set; }
        public string virus { get; set; }
        public int minPatientAge { get; set; }
        public int maxPatientAge { get; set; }
    }
}
