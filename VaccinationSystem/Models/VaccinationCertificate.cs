using System;
using System.Collections.Generic;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class VaccinationCertificate
    {
        public VaccinationCertificate()
        {
            PatientsCertificates = new HashSet<PatientsCertificate>();
        }

        public int CertificateId { get; set; }
        public string Url { get; set; }

        public virtual ICollection<PatientsCertificate> PatientsCertificates { get; set; }
    }
}
