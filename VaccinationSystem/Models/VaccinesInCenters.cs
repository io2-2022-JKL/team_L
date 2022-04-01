using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class VaccinesInCenters
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [ForeignKey("vaccineId")]
        public Vaccine vaccine { get; set; }
        [Required]
        [ForeignKey("vaccineCenterId")]
        public VaccinationCenter vaccinationCenter { get; set; }
    }
}
