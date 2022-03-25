using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public class VaccinationCount
    {
        [Key]
        public int id { get; set; }
        public virtual Virus virus { get; set; }
        public int count { get; set; }
        public virtual Patient patient { get; set; }
    }
}
