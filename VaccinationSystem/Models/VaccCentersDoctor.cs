using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class VaccCentersDoctor
    {
        public int Id { get; set; }
        public int VaccCenterId { get; set; }
        public int DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual VaccinationCenter VaccCenter { get; set; }
    }
}
