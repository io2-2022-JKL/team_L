using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public abstract class User
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public string pesel { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public DateTime dateOfBirth { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string phoneNumber { get; set; }
    }
}
