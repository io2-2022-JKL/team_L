﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VaccinationSystem.Data;

namespace VaccinationSystem.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VaccinationSystem.Models.Admin", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Appointment", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("Patientid1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("certifyState")
                        .HasColumnType("int");

                    b.Property<Guid?>("doctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("doctorId1")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("patientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("state")
                        .HasColumnType("int");

                    b.Property<Guid>("timeSlotId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("vaccineBatchNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("vaccineId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("whichDose")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Patientid1");

                    b.HasIndex("doctorId");

                    b.HasIndex("doctorId1");

                    b.HasIndex("patientId");

                    b.HasIndex("timeSlotId");

                    b.HasIndex("vaccineId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Certificate", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("patientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("vaccineId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("patientId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Doctor", b =>
                {
                    b.Property<Guid>("doctorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<Guid?>("patientAccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("vaccinationCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("doctorId");

                    b.HasIndex("patientAccountId");

                    b.HasIndex("vaccinationCenterId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("VaccinationSystem.Models.OpeningHours", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("from")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("to")
                        .HasColumnType("time");

                    b.Property<Guid>("vaccinationCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("vaccinationCenterId");

                    b.ToTable("OpeningHours");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Patient", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<DateTime>("dateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("VaccinationSystem.Models.TimeSlot", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<Guid>("doctorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("from")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isFree")
                        .HasColumnType("bit");

                    b.Property<DateTime>("to")
                        .HasColumnType("datetime2");

                    b.HasKey("id");

                    b.HasIndex("doctorId");

                    b.ToTable("TimeSlots");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinationCenter", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("VaccinationCenters");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinationCount", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("count")
                        .HasColumnType("int");

                    b.Property<Guid>("patientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("virus")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("patientId");

                    b.ToTable("VaccinationCounts");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Vaccine", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VaccinationCenterid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("active")
                        .HasColumnType("bit");

                    b.Property<string>("company")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("maxDaysBetweenDoses")
                        .HasColumnType("int");

                    b.Property<int>("maxPatientAge")
                        .HasColumnType("int");

                    b.Property<int>("minDaysBetweenDoses")
                        .HasColumnType("int");

                    b.Property<int>("minPatientAge")
                        .HasColumnType("int");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numberOfDoses")
                        .HasColumnType("int");

                    b.Property<int>("virus")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("VaccinationCenterid");

                    b.ToTable("Vaccines");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinesInCenters", b =>
                {
                    b.Property<Guid>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("vaccineCenterId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("vaccineId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("id");

                    b.HasIndex("vaccineCenterId");

                    b.HasIndex("vaccineId");

                    b.ToTable("VaccinesInCenters");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Appointment", b =>
                {
                    b.HasOne("VaccinationSystem.Models.Patient", null)
                        .WithMany("futureVaccinations")
                        .HasForeignKey("Patientid1");

                    b.HasOne("VaccinationSystem.Models.Doctor", null)
                        .WithMany("futureVaccinations")
                        .HasForeignKey("doctorId");

                    b.HasOne("VaccinationSystem.Models.Doctor", null)
                        .WithMany("vaccinationsArchive")
                        .HasForeignKey("doctorId1");

                    b.HasOne("VaccinationSystem.Models.Patient", "patient")
                        .WithMany("vaccinationHistory")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VaccinationSystem.Models.TimeSlot", "timeSlot")
                        .WithMany()
                        .HasForeignKey("timeSlotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VaccinationSystem.Models.Vaccine", "vaccine")
                        .WithMany()
                        .HasForeignKey("vaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");

                    b.Navigation("timeSlot");

                    b.Navigation("vaccine");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Certificate", b =>
                {
                    b.HasOne("VaccinationSystem.Models.Patient", null)
                        .WithMany("certificates")
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VaccinationSystem.Models.Doctor", b =>
                {
                    b.HasOne("VaccinationSystem.Models.Patient", "patientAccount")
                        .WithMany()
                        .HasForeignKey("patientAccountId");

                    b.HasOne("VaccinationSystem.Models.VaccinationCenter", "vaccinationCenter")
                        .WithMany("doctors")
                        .HasForeignKey("vaccinationCenterId");

                    b.Navigation("patientAccount");

                    b.Navigation("vaccinationCenter");
                });

            modelBuilder.Entity("VaccinationSystem.Models.OpeningHours", b =>
                {
                    b.HasOne("VaccinationSystem.Models.VaccinationCenter", "vaccinationCenter")
                        .WithMany()
                        .HasForeignKey("vaccinationCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("vaccinationCenter");
                });

            modelBuilder.Entity("VaccinationSystem.Models.TimeSlot", b =>
                {
                    b.HasOne("VaccinationSystem.Models.Doctor", "doctor")
                        .WithMany()
                        .HasForeignKey("doctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("doctor");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinationCount", b =>
                {
                    b.HasOne("VaccinationSystem.Models.Patient", "patient")
                        .WithMany()
                        .HasForeignKey("patientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("patient");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Vaccine", b =>
                {
                    b.HasOne("VaccinationSystem.Models.VaccinationCenter", null)
                        .WithMany("availableVaccines")
                        .HasForeignKey("VaccinationCenterid");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinesInCenters", b =>
                {
                    b.HasOne("VaccinationSystem.Models.VaccinationCenter", "vaccinationCenter")
                        .WithMany()
                        .HasForeignKey("vaccineCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VaccinationSystem.Models.Vaccine", "vaccine")
                        .WithMany()
                        .HasForeignKey("vaccineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("vaccinationCenter");

                    b.Navigation("vaccine");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Doctor", b =>
                {
                    b.Navigation("futureVaccinations");

                    b.Navigation("vaccinationsArchive");
                });

            modelBuilder.Entity("VaccinationSystem.Models.Patient", b =>
                {
                    b.Navigation("certificates");

                    b.Navigation("futureVaccinations");

                    b.Navigation("vaccinationHistory");
                });

            modelBuilder.Entity("VaccinationSystem.Models.VaccinationCenter", b =>
                {
                    b.Navigation("availableVaccines");

                    b.Navigation("doctors");
                });
#pragma warning restore 612, 618
        }
    }
}
