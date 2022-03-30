using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Certificate
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string url { get; set; }
        [Required]
        [ForeignKey("vaccineId")]
        public Guid vaccineId { get; set; }
        [Required]
        [ForeignKey("patientId")]
        public Guid patientId { get; set; }
    }
}
