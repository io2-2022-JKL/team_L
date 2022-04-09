using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Doctor : User
    {
        [ForeignKey("vaccinationCenterId")]
        public VaccinationCenter vaccinationCenter { get; set; }
        public IEnumerable<Appointment> vaccinationsArchive { get; set; }
        public IEnumerable<Appointment> futureVaccinations { get; set; }
        [ForeignKey("patientAccountId")]
        public Patient patientAccount { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
