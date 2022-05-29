using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public enum Virus
    {
        Coronavirus,
    }
    public class Vaccine
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string company { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int numberOfDoses { get; set; }
        public int minDaysBetweenDoses { get; set; }
        public int maxDaysBetweenDoses { get; set; }
        [Required]
        public Virus virus { get; set; }
        [Required]
        public int minPatientAge { get; set; }
        public int maxPatientAge { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
