using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace VaccinationSystem.Models
{
    public partial class Vaccination_DBContext : DbContext
    {
        public Vaccination_DBContext()
        {
        }

        public Vaccination_DBContext(DbContextOptions<Vaccination_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }
        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorArchivedAppointment> DoctorArchivedAppointments { get; set; }
        public virtual DbSet<DoctorFutureAppointment> DoctorFutureAppointments { get; set; }
        public virtual DbSet<Patient> Patients { get; set; }
        public virtual DbSet<PatientArchivedAppointment> PatientArchivedAppointments { get; set; }
        public virtual DbSet<PatientFutureAppointment> PatientFutureAppointments { get; set; }
        public virtual DbSet<PatientsCertificate> PatientsCertificates { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<VaccCentersDoctor> VaccCentersDoctors { get; set; }
        public virtual DbSet<VaccCentersVaccine> VaccCentersVaccines { get; set; }
        public virtual DbSet<VaccinationCenter> VaccinationCenters { get; set; }
        public virtual DbSet<VaccinationCertificate> VaccinationCertificates { get; set; }
        public virtual DbSet<VaccinationCount> VaccinationCounts { get; set; }
        public virtual DbSet<Vaccine> Vaccines { get; set; }
        public virtual DbSet<Virus> Viruses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(optionsBuilder.);
            }*/ // jest skonfigurowane w Startup.cs
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("lastName");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("mail");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("pesel");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.Completed).HasColumnName("completed");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.TimeSlotId).HasColumnName("timeSlot_id");

                entity.Property(e => e.VaccineBatchNumber)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("vaccineBatchNumber");

                entity.Property(e => e.VaccineId).HasColumnName("vaccine_id");

                entity.Property(e => e.WhichDose).HasColumnName("whichDose");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Patient");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.TimeSlotId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_TimeSlot");

                entity.HasOne(d => d.Vaccine)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.VaccineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appointment_Vaccine");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("lastName");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("mail");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("pesel");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("phoneNumber");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_Patient");
            });

            modelBuilder.Entity<DoctorArchivedAppointment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.DoctorArchivedAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_A_Doctor_Appointment");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorArchivedAppointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_A_Appointment_Doctor");
            });

            modelBuilder.Entity<DoctorFutureAppointment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.DoctorFutureAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_F_Doctor_Appointment");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.DoctorFutureAppointments)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_F_Appointment_Doctor");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("dateOfBirth");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("lastName");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("mail");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.Pesel)
                    .IsRequired()
                    .HasMaxLength(11)
                    .HasColumnName("pesel");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<PatientArchivedAppointment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientArchivedAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_A_Patient_Appointment");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientArchivedAppointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_A_Appointment_Patient");
            });

            modelBuilder.Entity<PatientFutureAppointment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Appointment)
                    .WithMany(p => p.PatientFutureAppointments)
                    .HasForeignKey(d => d.AppointmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_F_Patient_Appointment");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientFutureAppointments)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_F_Appointment_Patient");
            });

            modelBuilder.Entity<PatientsCertificate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.HasOne(d => d.Certificate)
                    .WithMany(p => p.PatientsCertificates)
                    .HasForeignKey(d => d.CertificateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Certificate");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.PatientsCertificates)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Certificate_Patient");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.Property(e => e.TimeSlotId).HasColumnName("timeSlot_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.From)
                    .HasColumnType("datetime")
                    .HasColumnName("from");

                entity.Property(e => e.IsFree).HasColumnName("isFree");

                entity.Property(e => e.To)
                    .HasColumnType("datetime")
                    .HasColumnName("to");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TimeSlot_Doctor");
            });

            modelBuilder.Entity<VaccCentersDoctor>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DoctorId).HasColumnName("doctor_id");

                entity.Property(e => e.VaccCenterId).HasColumnName("vaccCenter_id");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.VaccCentersDoctors)
                    .HasForeignKey(d => d.DoctorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VaccinationCenter_Doctor");

                entity.HasOne(d => d.VaccCenter)
                    .WithMany(p => p.VaccCentersDoctors)
                    .HasForeignKey(d => d.VaccCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Doctor_VaccinationCenter");
            });

            modelBuilder.Entity<VaccCentersVaccine>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.VaccCenterId).HasColumnName("vaccCenter_id");

                entity.Property(e => e.VaccineId).HasColumnName("vaccine_id");

                entity.HasOne(d => d.VaccCenter)
                    .WithMany(p => p.VaccCentersVaccines)
                    .HasForeignKey(d => d.VaccCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vaccine_VaccinationCenter");

                entity.HasOne(d => d.Vaccine)
                    .WithMany(p => p.VaccCentersVaccines)
                    .HasForeignKey(d => d.VaccineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VaccinationCenter_Vaccine");
            });

            modelBuilder.Entity<VaccinationCenter>(entity =>
            {
                entity.HasKey(e => e.VaccCenterId)
                    .HasName("PK__Vaccinat__1D6A5C1B469ABCB1");

                entity.Property(e => e.VaccCenterId).HasColumnName("vaccCenter_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.ClosingHourFri).HasColumnName("closingHour_fri");

                entity.Property(e => e.ClosingHourMon).HasColumnName("closingHour_mon");

                entity.Property(e => e.ClosingHourSat).HasColumnName("closingHour_sat");

                entity.Property(e => e.ClosingHourSun).HasColumnName("closingHour_sun");

                entity.Property(e => e.ClosingHourThu).HasColumnName("closingHour_thu");

                entity.Property(e => e.ClosingHourTue).HasColumnName("closingHour_tue");

                entity.Property(e => e.ClosingHourWed).HasColumnName("closingHour_wed");

                entity.Property(e => e.OpeningHourFri).HasColumnName("openingHour_fri");

                entity.Property(e => e.OpeningHourMon).HasColumnName("openingHour_mon");

                entity.Property(e => e.OpeningHourSat).HasColumnName("openingHour_sat");

                entity.Property(e => e.OpeningHourSun).HasColumnName("openingHour_sun");

                entity.Property(e => e.OpeningHourThu).HasColumnName("openingHour_thu");

                entity.Property(e => e.OpeningHourTue).HasColumnName("openingHour_tue");

                entity.Property(e => e.OpeningHourWed).HasColumnName("openingHour_wed");

                entity.Property(e => e.VaccCenterName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("vaccCenter_name");
            });

            modelBuilder.Entity<VaccinationCertificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId)
                    .HasName("PK__Vaccinat__E2256D316698F2AD");

                entity.Property(e => e.CertificateId).HasColumnName("certificate_id");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(2000)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<VaccinationCount>(entity =>
            {
                entity.ToTable("VaccinationCount");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.PatientId).HasColumnName("patient_id");

                entity.Property(e => e.VirusId).HasColumnName("virus_id");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.VaccinationCounts)
                    .HasForeignKey(d => d.PatientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Virus_Patient");

                entity.HasOne(d => d.Virus)
                    .WithMany(p => p.VaccinationCounts)
                    .HasForeignKey(d => d.VirusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Patient_Virus");
            });

            modelBuilder.Entity<Vaccine>(entity =>
            {
                entity.Property(e => e.VaccineId).HasColumnName("vaccine_id");

                entity.Property(e => e.Company)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("company");

                entity.Property(e => e.MaxDaysBetweenDoses).HasColumnName("maxDaysBetweenDoses");

                entity.Property(e => e.MaxPatientAge).HasColumnName("maxPatientAge");

                entity.Property(e => e.MinDaysBetweenDoses).HasColumnName("minDaysBetweenDoses");

                entity.Property(e => e.MinPatientAge).HasColumnName("minPatientAge");

                entity.Property(e => e.NumberOfDoses).HasColumnName("numberOfDoses");

                entity.Property(e => e.Used).HasColumnName("used");

                entity.Property(e => e.VaccineName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("vaccine_name");

                entity.Property(e => e.VirusId).HasColumnName("virus_id");

                entity.HasOne(d => d.Virus)
                    .WithMany(p => p.Vaccines)
                    .HasForeignKey(d => d.VirusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Vaccine_Virus");
            });

            modelBuilder.Entity<Virus>(entity =>
            {
                entity.ToTable("Virus");

                entity.Property(e => e.VirusId).HasColumnName("virus_id");

                entity.Property(e => e.VirusName)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("virus_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
