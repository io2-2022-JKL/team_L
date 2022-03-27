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
        public int id { get; set; }
        public string pesel { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
    }
}
