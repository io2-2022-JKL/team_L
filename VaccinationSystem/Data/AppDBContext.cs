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

            // filling db
            /*modelBuilder.Entity<Admin>().HasData(
                    new Admin
                    {
                        id = 1,
                        pesel = "85020219191",
                        firstName = "Jan",
                        lastName = "Nowak",
                        dateOfBirth = new DateTime(1985, 2, 2),
                        mail = "superadmin@mail.com",
                        password = "tajnehaslo123",
                        phoneNumber = "+48111222333"
                    }
                );
            modelBuilder.Entity<Certificate>().HasData(
                    new Certificate
                    {
                        id = 1,
                        url = "placeholder"
                    }
                );
            modelBuilder.Entity<Patient>().HasData(
                    new Patient
                    {
                        id = 1,
                        pesel = "85020319293",
                        firstName = "Jan",
                        lastName = "Kowalski",
                        dateOfBirth = new DateTime(1985, 2, 3),
                        mail = "kowalskij@mail.com",
                        password = "password#123",
                        phoneNumber = "+48444222333",
                        active = true,
                        certificates = { },
                        vaccinationHistory = { },
                        futureVaccinations = { }
                    }
                );
            modelBuilder.Entity<Doctor>().HasData(
                    new Doctor
                    {
                        id = 1,
                        pesel = "70120319293",
                        firstName = "Janina",
                        lastName = "Kowalska",
                        dateOfBirth = new DateTime(1970, 12, 3),
                        mail = "kowalskaj@mail.com",
                        password = "password-456",
                        phoneNumber = "+48444567333",
                        active = true,
                        vaccinationCenter = null,
                        patientAccount = null,
                        vaccinationsArchive = { },
                        futureVaccinations = { }
                    }
                );
            modelBuilder.Entity<TimeSlot>().HasData(
                    new TimeSlot
                    {
                        id = 1,
                        from = new DateTime(2022, 04, 13, 12, 30, 0),
                        to = new DateTime(2022, 04, 13, 13, 0, 0),
                        doctor = null,
                        isFree = true,
                        active = false
                    }
                );
            modelBuilder.Entity<VaccinationCenter>().HasData(
                    new VaccinationCenter
                    {
                        id = 1,
                        name = "Fajny punkt szczepien",
                        city = "Warszawa",
                        address = "ul. Chmielna 15/43",
                        active = true
                        // reszta info
                        //availableVaccines = { },
                        //openingHours = 
                    }
                );
            modelBuilder.Entity<Vaccine>().HasData(
                    new Vaccine
                    {
                        id = 1,
                        company = "Pfeizer",
                        name = "Pfeizer vaccine",
                        numberOfDoses = 2,
                        minDaysBetweenDoses = 30,
                        maxDaysBetweenDoses = 90,
                        minPatientAge = 12,
                        maxPatientAge = 99,
                        used = true,
                        virus = 0
                    }
                );*/
        }    }
}
