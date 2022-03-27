using System;
using System.Collections.Generic;


namespace VaccinationSystem.Models
{
    public partial class Virus2
    {
        public Virus2()
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
