using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
