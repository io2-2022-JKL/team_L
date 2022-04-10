using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace VaccinationSystem.Models
{
    public class LoginResponse
    {
        [Required]
        public Guid userId { get; set; }
        [Required]
        public string userType { get; set; }
    }
}
