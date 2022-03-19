using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.Models
{
    public class Login
    {
        [Required]
        public string mail { get; set; }
        [Required]
        public string password { get; set; }
    }
}
