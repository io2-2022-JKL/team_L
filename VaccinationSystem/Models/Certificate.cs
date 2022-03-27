﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class Certificate
    {
        [Key]
        public int id { get; set; }
        public string url { get; set; }
    }
}