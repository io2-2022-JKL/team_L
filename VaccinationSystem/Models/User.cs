using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace VaccinationSystem.Models
{
    public abstract class User
    {
        public string Id { get; set; }
        public string PESEL { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateOfBirth { get; set; }
        string Mail { get; set; }
        string Password { get; set; }
        string PhoneNumber { get; set; }
    }
}
