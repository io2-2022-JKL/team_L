using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class VaccinationCount
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int VirusId { get; set; }
        public int Count { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Virus Virus { get; set; }
    }
}
