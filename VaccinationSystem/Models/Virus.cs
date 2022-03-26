using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Virus
    {
        public Virus()
        {
            VaccinationCounts = new HashSet<VaccinationCount>();
            Vaccines = new HashSet<Vaccine>();
        }

        public int VirusId { get; set; }
        public string VirusName { get; set; }

        public virtual ICollection<VaccinationCount> VaccinationCounts { get; set; }
        public virtual ICollection<Vaccine> Vaccines { get; set; }
    }
}
