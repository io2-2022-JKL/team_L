using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<VaccinationCount> VaccinationCounts { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<OpeningHours> OpeningHours { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<VaccinationCenter> VaccinationCenters { get; set; }        public DbSet<VaccinesInCenters> VaccinesInCenters { get; set; }        public DbSet<Admin> Admins { get; set; }        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().Ignore(patient => patient.vaccinationsCount);
            modelBuilder.Entity<Patient>().HasMany(patient => patient.vaccinationHistory);
            modelBuilder.Entity<Patient>().HasMany(patient => patient.futureVaccinations);
            modelBuilder.Entity<Patient>().HasMany(patient => patient.certificates);
            modelBuilder.Entity<Doctor>().HasMany(doc => doc.vaccinationsArchive);
            modelBuilder.Entity<Doctor>().HasMany(doc => doc.futureVaccinations);
            modelBuilder.Entity<VaccinationCenter>().HasMany(vc => vc.availableVaccines);
            modelBuilder.Entity<VaccinationCenter>().HasMany(vc => vc.doctors);
            modelBuilder.Entity<Appointment>().HasOne(appointment => appointment.patient);
            modelBuilder.Entity<Appointment>().HasOne(appointment => appointment.timeSlot);
        }    }
}
