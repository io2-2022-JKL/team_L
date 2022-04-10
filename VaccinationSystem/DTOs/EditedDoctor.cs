﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Validation;

namespace VaccinationSystem.Models
{
    public class EditedDoctor 
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        [StringLength(11)]
        public string pesel { get; set; }

        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string mail { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public Guid vaccinationCenterId { get; set; }
    }
}
