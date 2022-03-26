using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class VaccinationCenter
    {
        public VaccinationCenter()
        {
            this.availableVaccines = new HashSet<Vaccine>();
            this.doctors = new HashSet<Doctor>();
        }

        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public virtual ICollection<Vaccine> availableVaccines { get; set; }
        public DateTime[] openingHours = new DateTime[7];
        public DateTime[] closingHours = new DateTime[7];
        public  virtual ICollection<Doctor> doctors { get; set; }
        public bool active { get; set; }
    }
}
