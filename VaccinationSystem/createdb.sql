USE master
GO
if exists (select * from sysdatabases where name='Vaccination_DB')
		drop database Vaccination_DB
go

DECLARE @device_directory NVARCHAR(520)
SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

EXECUTE (N'CREATE DATABASE Vaccination_DB
  ON PRIMARY (NAME = N''Vaccination_DB'', FILENAME = N''' + @device_directory + N'Vaccination_DB.mdf'')
  LOG ON (NAME = N''Vaccination_DB_log'',  FILENAME = N''' + @device_directory + N'Vaccination_DB.ldf'')')
go

USE Vaccination_DB;
SET DATEFORMAT mdy
GO

if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Virus'))
	alter table "dbo"."Vaccines" drop constraint "FK_Vaccine_Virus"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."Doctors" drop constraint "FK_Doctor_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.VaccCentersVaccines'))
	alter table "dbo"."VaccCentersVaccines" drop constraint "FK_Vaccine_VaccinationCenter"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.VaccCentersVaccines'))
	alter table "dbo"."VaccCentersVaccines" drop constraint "FK_VaccinationCenter_Vaccine"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.VaccCentersDoctors'))
	alter table "dbo"."VaccCentersDoctors" drop constraint "FK_Doctor_VaccinationCenter"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.VaccCentersDoctors'))
	alter table "dbo"."VaccCentersDoctors" drop constraint "FK_VaccinationCenter_Doctor"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Virus'))
	alter table "dbo"."VaccinationCount" drop constraint "FK_Patient_Virus"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."VaccinationCount" drop constraint "FK_Virus_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.VaccinationCertificates'))
	alter table "dbo"."PatientsCertificates" drop constraint "FK_Patient_Certificate"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."PatientsCertificates" drop constraint "FK_Certificate_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Doctors'))
	alter table "dbo"."TimeSlots" drop constraint "FK_TimeSlot_Doctor"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.TimeSlots'))
	alter table "dbo"."Appointments" drop constraint "FK_Appointment_TimeSlot"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."Appointments" drop constraint "FK_Appointment_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Vaccines'))
	alter table "dbo"."Appointments" drop constraint "FK_Appointment_Vaccine"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Doctors'))
	alter table "dbo"."DoctorArchivedAppointments" drop constraint "FK_A_Appointment_Doctor"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Appointments'))
	alter table "dbo"."DoctorArchivedAppointments" drop constraint "FK_A_Doctor_Appointment"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Doctors'))
	alter table "dbo"."DoctorFutureAppointments" drop constraint "FK_F_Appointment_Doctor"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Appointments'))
	alter table "dbo"."DoctorFutureAppointments" drop constraint "FK_F_Doctor_Appointment"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."PatientArchivedAppointments" drop constraint "FK_A_Appointment_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Appointments'))
	alter table "dbo"."PatientArchivedAppointments" drop constraint "FK_A_Patient_Appointment"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Patients'))
	alter table "dbo"."PatientFutureAppointments" drop constraint "FK_F_Appointment_Patient"
GO
if exists(select * from sys.foreign_keys where referenced_object_id=object_id('dbo.Appointments'))
	alter table "dbo"."PatientFutureAppointments" drop constraint "FK_F_Patient_Appointment"
GO

if exists (select * from sysobjects where id = OBJECT_ID('dbo.Virus') and sysstat & 0xf=3)
	drop table "dbo"."Virus"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.Vaccines') and sysstat & 0xf=3)
	drop table "dbo"."Vaccine"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.Doctors') and sysstat & 0xf=3)
	drop table "dbo"."Doctors"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.VaccinationCenters') and sysstat & 0xf=3)
	drop table "dbo"."VaccinationCenters"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.VaccCentersVaccines') and sysstat & 0xf=3)
	drop table "dbo"."VaccCentersVaccines"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.VaccCentersDoctors') and sysstat & 0xf=3)
	drop table "dbo"."VaccCentersDoctors"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.Patients') and sysstat & 0xf=3)
	drop table "dbo"."Patients"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.VaccinationCount') and sysstat & 0xf=3)
	drop table "dbo"."VaccinationCount"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.VaccinationCertificates') and sysstat & 0xf=3)
	drop table "dbo"."VaccinationCertificates"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.PatientsCertificates') and sysstat & 0xf=3)
	drop table "dbo"."PatientsCertificates"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.TimeSlots') and sysstat & 0xf=3)
	drop table "dbo"."TimeSlots"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.Appointments') and sysstat & 0xf=3)
	drop table "dbo"."Appointments"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.DoctorArchivedAppointments') and sysstat & 0xf=3)
	drop table "dbo"."DoctorArchivedAppointments"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.DoctorFutureAppointments') and sysstat & 0xf=3)
	drop table "dbo"."DoctorFutureAppointments"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.PatientArchivedAppointments') and sysstat & 0xf=3)
	drop table "dbo"."PatientArchivedAppointments"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.DoctorFutureAppointments') and sysstat & 0xf=3)
	drop table "dbo"."PatientFutureAppointments"
GO
if exists (select * from sysobjects where id = OBJECT_ID('dbo.Admins') and sysstat & 0xf=3)
	drop table "dbo"."Admins"
GO

CREATE TABLE Virus (
	"virus_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"virus_name" nvarchar(25) NOT NULL
);

CREATE TABLE Vaccines (
	"vaccine_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"vaccine_name" nvarchar(50) NOT NULL,
	"company" nvarchar(50) NOT NULL,
	"numberOfDoses" int NOT NULL,
	"minDaysBetweenDoses" int,
	"maxDaysBetweenDoses" int,
	"virus_id" int NOT NULL,
	"minPatientAge" int NOT NULL,
	"maxPatientAge" int,
	"used" bit NOT NULL, -- bit - 0/1 -- like bool
	CONSTRAINT "FK_Vaccine_Virus" FOREIGN KEY ("virus_id")
	REFERENCES "dbo"."Virus"("virus_id")
);

CREATE TABLE Patients (
	"patient_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"firstName" nvarchar(25) NOT NULL,
	"lastName" nvarchar(25) NOT NULL,
	"pesel" nvarchar(11) NOT NULL,
	"dateOfBirth" datetime NOT NULL,
	"mail" nvarchar(25) NOT NULL,
	"password" nvarchar(25) NOT NULL,
	"phoneNumber" nvarchar(12) NOT NULL,
	"active" bit NOT NULL
);	


CREATE TABLE Doctors (
	"doctor_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"firstName" nvarchar(25) NOT NULL,
	"lastName" nvarchar(25) NOT NULL,
	"pesel" nvarchar(11) NOT NULL,
	"dateOfBirth" datetime NOT NULL,
	"mail" nvarchar(25) NOT NULL,
	"password" nvarchar(25) NOT NULL,
	"phoneNumber" nvarchar(12) NOT NULL,
	"patient_id" int NOT NULL,
	"active" bit NOT NULL,
	CONSTRAINT "FK_Doctor_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id")
);

CREATE TABLE VaccinationCenters (
	"vaccCenter_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"vaccCenter_name" nvarchar(100) NOT NULL,
	"city" nvarchar(50) NOT NULL,
	"address" nvarchar(50) NOT NULL,
	"openingHour_mon" time, 
	"closingHour_mon" time,
	"openingHour_tue" time, 
	"closingHour_tue" time,
	"openingHour_wed" time, 
	"closingHour_wed" time,
	"openingHour_thu" time, 
	"closingHour_thu" time,
	"openingHour_fri" time, 
	"closingHour_fri" time,
	"openingHour_sat" time, 
	"closingHour_sat" time,
	"openingHour_sun" time, 
	"closingHour_sun" time,
	"active" bit NOT NULL
);	

CREATE TABLE VaccCentersVaccines (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"vaccCenter_id" int NOT NULL,
	"vaccine_id" int NOT NULL,
	CONSTRAINT "FK_Vaccine_VaccinationCenter" FOREIGN KEY ("vaccCenter_id")
	REFERENCES "dbo"."VaccinationCenters"("vaccCenter_id"),
	CONSTRAINT "FK_VaccinationCenter_Vaccine" FOREIGN KEY ("vaccine_id")
	REFERENCES "dbo"."Vaccines"("vaccine_id")
);

CREATE TABLE VaccCentersDoctors (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"vaccCenter_id" int NOT NULL,
	"doctor_id" int NOT NULL,
	CONSTRAINT "FK_Doctor_VaccinationCenter" FOREIGN KEY ("vaccCenter_id")
	REFERENCES "dbo"."VaccinationCenters"("vaccCenter_id"),
	CONSTRAINT "FK_VaccinationCenter_Doctor" FOREIGN KEY ("doctor_id")
	REFERENCES "dbo"."Doctors"("doctor_id")
);

CREATE TABLE VaccinationCount (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"patient_id" int NOT NULL,
	"virus_id" int NOT NULL,
	"count" int NOT NULL,
	CONSTRAINT "FK_Patient_Virus" FOREIGN KEY ("virus_id")
	REFERENCES "dbo"."Virus"("virus_id"),
	CONSTRAINT "FK_Virus_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id")
);

CREATE TABLE VaccinationCertificates (
	"certificate_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"url" nvarchar(2000) NOT NULL
);

CREATE TABLE PatientsCertificates (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"patient_id" int NOT NULL,
	"certificate_id" int NOT NULL,
	CONSTRAINT "FK_Patient_Certificate" FOREIGN KEY ("certificate_id")
	REFERENCES "dbo"."VaccinationCertificates"("certificate_id"),
	CONSTRAINT "FK_Certificate_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id")
);

CREATE TABLE TimeSlots (
	"timeSlot_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"from" datetime NOT NULL,
	"to" datetime NOT NULL,
	"doctor_id" int NOT NULL,
	"isFree" bit NOT NULL,
	"active" bit NOT NULL,
	CONSTRAINT "FK_TimeSlot_Doctor" FOREIGN KEY ("doctor_id")
	REFERENCES "dbo"."Doctors"("doctor_id")
);

CREATE TABLE Appointments (
	"appointment_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"whichDose" int NOT NULL,
	"timeSlot_id" int NOT NULL,
	"patient_id" int NOT NULL,
	"vaccine_id" int NOT NULL,
	"completed" bit NOT NULL,
	"vaccineBatchNumber" nvarchar(25) NOT NULL,
	CONSTRAINT "FK_Appointment_TimeSlot" FOREIGN KEY ("timeSlot_id")
	REFERENCES "dbo"."TimeSlots"("timeSlot_id"),
	CONSTRAINT "FK_Appointment_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id"),
	CONSTRAINT "FK_Appointment_Vaccine" FOREIGN KEY ("vaccine_id")
	REFERENCES "dbo"."Vaccines"("vaccine_id"),
);

CREATE TABLE DoctorArchivedAppointments (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"doctor_id" int NOT NULL,
	"appointment_id" int NOT NULL,
	CONSTRAINT "FK_A_Appointment_Doctor" FOREIGN KEY ("doctor_id")
	REFERENCES "dbo"."Doctors"("doctor_id"),
	CONSTRAINT "FK_A_Doctor_Appointment" FOREIGN KEY ("appointment_id")
	REFERENCES "dbo"."Appointments"("appointment_id")
);

CREATE TABLE DoctorFutureAppointments (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"doctor_id" int NOT NULL,
	"appointment_id" int NOT NULL,
	CONSTRAINT "FK_F_Appointment_Doctor" FOREIGN KEY ("doctor_id")
	REFERENCES "dbo"."Doctors"("doctor_id"),
	CONSTRAINT "FK_F_Doctor_Appointment" FOREIGN KEY ("appointment_id")
	REFERENCES "dbo"."Appointments"("appointment_id")
);

CREATE TABLE PatientArchivedAppointments (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"patient_id" int NOT NULL,
	"appointment_id" int NOT NULL,
	CONSTRAINT "FK_A_Appointment_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id"),
	CONSTRAINT "FK_A_Patient_Appointment" FOREIGN KEY ("appointment_id")
	REFERENCES "dbo"."Appointments"("appointment_id")
);

CREATE TABLE PatientFutureAppointments (
	"id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"patient_id" int NOT NULL,
	"appointment_id" int NOT NULL,
	CONSTRAINT "FK_F_Appointment_Patient" FOREIGN KEY ("patient_id")
	REFERENCES "dbo"."Patients"("patient_id"),
	CONSTRAINT "FK_F_Patient_Appointment" FOREIGN KEY ("appointment_id")
	REFERENCES "dbo"."Appointments"("appointment_id")
);

CREATE TABLE Admins (
	"admin_id" int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	"firstName" nvarchar(25) NOT NULL,
	"lastName" nvarchar(25) NOT NULL,
	"pesel" nvarchar(11) NOT NULL,
	"dateOfBirth" datetime NOT NULL,
	"mail" nvarchar(25) NOT NULL,
	"password" nvarchar(25) NOT NULL,
	"phoneNumber" nvarchar(12) NOT NULL
);