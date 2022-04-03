using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class VaccinationCenter
    {

        [Key]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string city { get; set; }
        [Required]
        public string address { get; set; }
        public IEnumerable<Vaccine> availableVaccines { get; set; }
        public OpeningHours[] openingHours = new OpeningHours[7];
        public List<Doctor> doctors { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
