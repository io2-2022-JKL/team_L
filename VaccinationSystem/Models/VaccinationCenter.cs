using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class VaccinationCenter
    {
        public VaccinationCenter()
        {
            VaccCentersDoctors = new HashSet<VaccCentersDoctor>();
            VaccCentersVaccines = new HashSet<VaccCentersVaccine>();
        }

        public int VaccCenterId { get; set; }
        public string VaccCenterName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public TimeSpan? OpeningHourMon { get; set; }
        public TimeSpan? ClosingHourMon { get; set; }
        public TimeSpan? OpeningHourTue { get; set; }
        public TimeSpan? ClosingHourTue { get; set; }
        public TimeSpan? OpeningHourWed { get; set; }
        public TimeSpan? ClosingHourWed { get; set; }
        public TimeSpan? OpeningHourThu { get; set; }
        public TimeSpan? ClosingHourThu { get; set; }
        public TimeSpan? OpeningHourFri { get; set; }
        public TimeSpan? ClosingHourFri { get; set; }
        public TimeSpan? OpeningHourSat { get; set; }
        public TimeSpan? ClosingHourSat { get; set; }
        public TimeSpan? OpeningHourSun { get; set; }
        public TimeSpan? ClosingHourSun { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<VaccCentersDoctor> VaccCentersDoctors { get; set; }
        public virtual ICollection<VaccCentersVaccine> VaccCentersVaccines { get; set; }
    }
}
