using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class VaccinationCount
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public Virus virus { get; set; }
        [Required]
        public int count { get; set; }
        [Required]
        [ForeignKey("patientId")]
        public virtual Patient patient { get; set; }
    }
}
