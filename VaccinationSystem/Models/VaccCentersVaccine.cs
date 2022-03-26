using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class VaccCentersVaccine
    {
        public int Id { get; set; }
        public int VaccCenterId { get; set; }
        public int VaccineId { get; set; }

        public virtual VaccinationCenter VaccCenter { get; set; }
        public virtual Vaccine Vaccine { get; set; }
    }
}
