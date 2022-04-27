using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using VaccinationSystem.Models;

namespace VaccinationSystem.DTOs
{
    public class FilterTimeSlotResponse
    {
        public Guid timeSlotId { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string vaccinationCenterName { get; set; }
        public string vaccinationCenterCity { get; set; }
        public string vaccinationCenterStreet { get; set; }
        public List <SimplifiedVaccine> availableVaccines { get; set; }
        public List<OpeningHoursDays> openingHours { get; set; }
        public string doctorFirstName { get; set; }
        public string doctorLastName { get; set; }
    }
}
