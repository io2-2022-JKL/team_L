set identity_insert "Patients" on
go
ALTER TABLE "Patients" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(0, 'Jan', 'Kowalski', '90345678900', '05/02/1990', 'j.kowalski@mail.com', 'haslo', '+48123456789', 1)   
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(1, 'Janina', 'Kowalska', '94347778900', '12/15/1994', 'j.kowalska@mail.pl', 'haslo123', '+48124356755', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(2, 'Mateusz', 'Nowak', '00305678911', '10/27/2000', 'm.nowak@mail.pl', 'password', '+48125026789', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(3, 'Ala', 'Makota', '99345678910', '05/02/1999', 'alamakota99@mail.com', 'haslo99', '+48620456589', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(4, 'Jan', 'Abecadlo', '89435678900', '02/05/1989', 'abcd.jan@mail.com', 'abcdef123456', '+48321446889', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(5, 'Robert', 'Baranowski', '02435679810', '07/13/2002', 'robertb@mail.com', 'PASSWORD', '+48321476787', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(6, 'Benedykt', 'Lubomirski', '53435678900', '01/01/1953', 'lubomirskib@mail.pl', 'benedykt', '+48621466869', 0)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(7, 'Tomasz', 'Lubomirski', '76430078922', '05/02/1976', 't.lubo@mail.com', 'qwerty12345', '+48601246089', 1)
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(8, 'Albert', 'Kowalski', '90345768901', '02/15/1990', 'a.kowalski@mail.com', 'haslo', '+48523556759', 1)   
INSERT INTO "Patients"("patient_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "active")
VALUES(9, 'Mateusz', 'Leopold', '80305577911', '10/27/1980', 'm.leopold@mail.pl', 'password1230', '+48659123990', 1)
GO
set identity_insert "Patients" off
go

set identity_insert "Doctors" on
go
ALTER TABLE "Doctors" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Doctors"("doctor_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "patient_id", "active")
VALUES(0, 'Albert', 'Kowalski', '90345768901', '02/15/1990', 'a.kowalski@mail.com', 'haslo', '+48523556759', 8, 1)   
INSERT INTO "Doctors"("doctor_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "patient_id", "active")
VALUES(1, 'Janina', 'Kowalska', '94347778900', '12/15/1994', 'j.kowalska@mail.pl', 'haslo123', '+48124356755', 1, 1)
INSERT INTO "Doctors"("doctor_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "patient_id", "active")
VALUES(2, 'Mateusz', 'Leopold', '80305577911', '10/27/1980', 'm.leopold@mail.pl', 'password1230', '+48659123990', 9, 1)
INSERT INTO "Doctors"("doctor_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "patient_id", "active")
VALUES(3, 'Tomasz', 'Lubomirski', '76430078922', '05/02/1976', 't.lubo@mail.com', 'qwerty12345', '+48601246089', 7, 1)
INSERT INTO "Doctors"("doctor_id","firstName" ,"lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber", "patient_id", "active")
VALUES(4, 'Jan', 'Abecadlo', '89435678900', '02/05/1989', 'abcd.jan@mail.com', 'abcdef123456', '+48321446889', 4, 1)
GO
set identity_insert "Doctors" off
go

set identity_insert "Virus" on
go
ALTER TABLE "Virus" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Virus"("virus_id", "virus_name")
VALUES(0, 'Coronavirus')
GO
set identity_insert "Virus" off
go

set identity_insert "Vaccines" on
go
ALTER TABLE "Vaccines" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Vaccines"("vaccine_id", "vaccine_name", "company", "numberOfDoses", "minDaysBetweenDoses", "maxDaysBetweenDoses", "virus_id", "minPatientAge", "maxPatientAge", "used")
VALUES(0, 'COMIRNATY', 'Pfizer, Inc., BioNTech', 2, 21, NULL, 0, 12, NULL, 1)
INSERT INTO "Vaccines"("vaccine_id", "vaccine_name", "company", "numberOfDoses", "minDaysBetweenDoses", "maxDaysBetweenDoses", "virus_id", "minPatientAge", "maxPatientAge", "used")
VALUES(1, 'Spikevax', 'ModernaTX, Inc.', 2, 28, NULL, 0, 18, NULL, 1)
INSERT INTO "Vaccines"("vaccine_id", "vaccine_name", "company", "numberOfDoses", "minDaysBetweenDoses", "maxDaysBetweenDoses", "virus_id", "minPatientAge", "maxPatientAge", "used")
VALUES(2, 'JNJ-78436735', 'Johnson & Johnson', 1, NULL, NULL, 0, 18, NULL, 1)
GO
set identity_insert "Vaccines" off
go

set identity_insert "VaccinationCenters" on
go
ALTER TABLE "VaccinationCenters" NOCHECK CONSTRAINT ALL
go
INSERT INTO "VaccinationCenters"("vaccCenter_id", "vaccCenter_name", "city", "address", "openingHour_mon", "closingHour_mon", "openingHour_tue", "closingHour_tue", "openingHour_wed", "closingHour_wed", "openingHour_thu", "closingHour_thu", "openingHour_fri", "closingHour_fri", "openingHour_sat", "closingHour_sat", "openingHour_sun", "closingHour_sun", "active")
VALUES(0, 'SZPZLO Warszawa Praga Południe', 'Warszawa', 'ul. Styrska 44', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', NULL, NULL, NULL, NULL, 1)
INSERT INTO "VaccinationCenters"("vaccCenter_id", "vaccCenter_name", "city", "address", "openingHour_mon", "closingHour_mon", "openingHour_tue", "closingHour_tue", "openingHour_wed", "closingHour_wed", "openingHour_thu", "closingHour_thu", "openingHour_fri", "closingHour_fri", "openingHour_sat", "closingHour_sat", "openingHour_sun", "closingHour_sun", "active")
VALUES(1, 'SPZOZ Warszawa-Białołęka', 'Warszawa', 'ul. Milenijna 4', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', NULL, NULL, NULL, NULL, 1)
INSERT INTO "VaccinationCenters"("vaccCenter_id", "vaccCenter_name", "city", "address", "openingHour_mon", "closingHour_mon", "openingHour_tue", "closingHour_tue", "openingHour_wed", "closingHour_wed", "openingHour_thu", "closingHour_thu", "openingHour_fri", "closingHour_fri", "openingHour_sat", "closingHour_sat", "openingHour_sun", "closingHour_sun", "active")
VALUES(2, 'SZPZLO Warszawa-Wawer', 'Warszawa', 'ul. J. Strusia 4/8', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', '8:00 AM', '7:00 PM', NULL, NULL, NULL, NULL, 1)
GO
set identity_insert "VaccinationCenters" off
go

set identity_insert "VaccCentersVaccines" on
go
ALTER TABLE "VaccCentersVaccines" NOCHECK CONSTRAINT ALL
go
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(0, 0, 0)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(1, 0, 1)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(2, 0, 2)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(3, 1, 0)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(4, 1, 1)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(5, 1, 2)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(6, 2, 0)
INSERT INTO "VaccCentersVaccines"("id", "vaccCenter_id", "vaccine_id")
VALUES(7, 2, 1)
GO
set identity_insert "VaccCentersVaccines" off
go

set identity_insert "VaccCentersDoctors" on
go
ALTER TABLE "VaccCentersDoctors" NOCHECK CONSTRAINT ALL
go
INSERT INTO "VaccCentersDoctors"("id", "vaccCenter_id", "doctor_id")
VALUES(0, 0, 0)
INSERT INTO "VaccCentersDoctors"("id", "vaccCenter_id", "doctor_id")
VALUES(1, 0, 1)
INSERT INTO "VaccCentersDoctors"("id", "vaccCenter_id", "doctor_id")
VALUES(2, 1, 2)
INSERT INTO "VaccCentersDoctors"("id", "vaccCenter_id", "doctor_id")
VALUES(3, 2, 3)
INSERT INTO "VaccCentersDoctors"("id", "vaccCenter_id", "doctor_id")
VALUES(4, 1, 4)
GO
set identity_insert "VaccCentersDoctors" off
go

set identity_insert "VaccinationCount" on
go
ALTER TABLE "VaccinationCount" NOCHECK CONSTRAINT ALL
go
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(0, 0, 0, 2)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(1, 1, 0, 1)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(2, 2, 0, 2)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(3, 3, 0, 2)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(4, 4, 0, 1)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(5, 5, 0, 2)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(7, 7, 0, 2)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(8, 8, 0, 1)
INSERT INTO "VaccinationCount"("id", "patient_id", "virus_id", "count")
VALUES(9, 9, 0, 2)
GO
set identity_insert "VaccinationCount" off
go

set identity_insert "VaccinationCertificates" on
go
ALTER TABLE "VaccinationCertificates" NOCHECK CONSTRAINT ALL
go
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(0, 'placeholder')
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(1, 'placeholder')
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(2, 'placeholder')
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(3, 'placeholder')
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(4, 'placeholder')
INSERT INTO "VaccinationCertificates"("certificate_id", "url")
VALUES(5, 'placeholder')
GO
set identity_insert "VaccinationCertificates" off
go

set identity_insert "PatientsCertificates" on
go
ALTER TABLE "PatientsCertificates" NOCHECK CONSTRAINT ALL
go
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(0, 0, 0)
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(1, 2, 1)
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(2, 3, 2)
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(3, 5, 3)
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(4, 7, 4)
INSERT INTO "PatientsCertificates"("id", "patient_id", "certificate_id")
VALUES(5, 9, 5)
GO
set identity_insert "PatientsCertificates" off
go

set identity_insert "TimeSlots" on
go
ALTER TABLE "TimeSlots" NOCHECK CONSTRAINT ALL
go
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(0, '04/04/2022 11:00 AM', '11:15 AM', 2, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(1, '04/04/2022 11:15 AM', '11:30 AM', 2, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(2, '04/04/2022 11:30 AM', '11:45 AM', 2, 0, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(3, '04/04/2022 09:00 AM', '09:15 AM', 0, 0, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(4, '04/04/2022 09:15 AM', '09:30 AM', 0, 0, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(5, '04/04/2022 09:30 AM', '09:45 AM', 0, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(6, '04/05/2022 11:00 AM', '11:15 AM', 3, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(7, '04/05/2022 11:15 AM', '11:30 AM', 3, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(8, '04/05/2022 11:30 AM', '11:45 AM', 3, 1, 1)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(9, '03/04/2022 09:00 AM', '09:15 AM', 1, 0, 0)
INSERT INTO "TimeSlots"("timeSlot_id", "from", "to", "doctor_id", "isFree", "active")
VALUES(10, '03/04/2022 09:15 AM', '09:30 AM', 1, 0, 0)
GO
set identity_insert "TimeSlots" off
go

set identity_insert "Appointments" on
go
ALTER TABLE "Appointments" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Appointments"("appointment_id", "whichDose", "timeSlot_id", "patient_id", "vaccine_id", "completed", "vaccineBatchNumber")
VALUES(0, 2, 4, 1, 1, 0, 'abc123')
INSERT INTO "Appointments"("appointment_id", "whichDose", "timeSlot_id", "patient_id", "vaccine_id", "completed", "vaccineBatchNumber")
VALUES(1, 2, 2, 4, 1, 0, 'abc123')
INSERT INTO "Appointments"("appointment_id", "whichDose", "timeSlot_id", "patient_id", "vaccine_id", "completed", "vaccineBatchNumber")
VALUES(2, 2, 3, 8, 1, 0, 'abc123')
INSERT INTO "Appointments"("appointment_id", "whichDose", "timeSlot_id", "patient_id", "vaccine_id", "completed", "vaccineBatchNumber")
VALUES(3, 2, 9, 0, 0, 1, 'abcd1234')
INSERT INTO "Appointments"("appointment_id", "whichDose", "timeSlot_id", "patient_id", "vaccine_id", "completed", "vaccineBatchNumber")
VALUES(4, 2, 10, 2, 0, 1, 'abcd1234')
GO
set identity_insert "Appointments" off
go

set identity_insert "DoctorArchivedAppointments" on
go
ALTER TABLE "DoctorArchivedAppointments" NOCHECK CONSTRAINT ALL
go
INSERT INTO "DoctorArchivedAppointments"("id", "doctor_id", "appointment_id")
VALUES(0, 1, 3)
INSERT INTO "DoctorArchivedAppointments"("id", "doctor_id", "appointment_id")
VALUES(1, 1, 4)
GO
set identity_insert "DoctorArchivedAppointments" off
go

set identity_insert "DoctorFutureAppointments" on
go
ALTER TABLE "DoctorFutureAppointments" NOCHECK CONSTRAINT ALL
go
INSERT INTO "DoctorFutureAppointments"("id", "doctor_id", "appointment_id")
VALUES(0, 0, 0)
INSERT INTO "DoctorFutureAppointments"("id", "doctor_id", "appointment_id")
VALUES(1, 2, 1)
INSERT INTO "DoctorFutureAppointments"("id", "doctor_id", "appointment_id")
VALUES(2, 0, 2)
GO
set identity_insert "DoctorFutureAppointments" off
go

set identity_insert "PatientArchivedAppointments" on
go
ALTER TABLE "PatientArchivedAppointments" NOCHECK CONSTRAINT ALL
go
INSERT INTO "PatientArchivedAppointments"("id", "patient_id", "appointment_id")
VALUES(0, 0, 3)
INSERT INTO "PatientArchivedAppointments"("id", "patient_id", "appointment_id")
VALUES(1, 2, 4)
GO
set identity_insert "PatientArchivedAppointments" off
go

set identity_insert "PatientFutureAppointments" on
go
ALTER TABLE "PatientFutureAppointments" NOCHECK CONSTRAINT ALL
go
INSERT INTO "PatientFutureAppointments"("id", "patient_id", "appointment_id")
VALUES(0, 1, 0)
INSERT INTO "PatientFutureAppointments"("id", "patient_id", "appointment_id")
VALUES(1, 4, 1)
INSERT INTO "PatientFutureAppointments"("id", "patient_id", "appointment_id")
VALUES(2, 8, 2)
GO
set identity_insert "PatientFutureAppointments" off
go

set identity_insert "Admins" on
go
ALTER TABLE "Admins" NOCHECK CONSTRAINT ALL
go
INSERT INTO "Admins"("admin_id", "firstName", "lastName", "pesel", "dateOfBirth", "mail", "password", "phoneNumber")
VALUES(0, 'Antoni', 'Nowak', '85101078900', '10/10/1985', 'admin@mail.com', 'SuperAdmin123', '+48606505123')
GO
set identity_insert "Admins" off
go