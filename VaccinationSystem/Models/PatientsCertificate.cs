using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class PatientsCertificate
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int CertificateId { get; set; }

        public virtual VaccinationCertificate Certificate { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
