using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class TimeSlot
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public DateTime from { get; set; }
        [Required]
        public DateTime to { get; set; }
        [Required]

        [ForeignKey("doctorId")]
        public  Doctor doctor { get; set; }
        [Required]
        public bool isFree { get; set; }
        [Required]
        public bool active { get; set; }
    }
}
