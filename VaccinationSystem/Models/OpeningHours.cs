using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public enum WeekDay
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public class OpeningHours
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public TimeSpan from { get; set; }
        [Required]
        public TimeSpan to { get; set; }
        [Required]
        [ForeignKey("vaccinationCenterId")]
        public VaccinationCenter vaccinationCenter { get; set; }
        [Required]
        public WeekDay day { get; set; }
    }
}
