using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Models
{
    public enum AppointmentState
    {
        Cancelled,
        Planned,
        Finished
    }
    public enum CertificateState
    {
        NotLast,
        LastNotCertified,
        Certified
    }

    public class Appointment
    {
        [Key]
        public Guid id { get; set; }
        [Required]
        public int whichDose { get; set; }
        [Required]
        [ForeignKey("timeSlotId")]
        public TimeSlot timeSlot { get; set; }
        [Required]
        [ForeignKey("patientId")]
        public Patient patient { get; set; }
        [Required]
        [ForeignKey("vaccineId")]
        public Vaccine vaccine { get; set; }
        [Required]
        public AppointmentState state { get; set; }
        public string vaccineBatchNumber { get; set; }
        public CertificateState certifyState { get; set; }
    }
}
