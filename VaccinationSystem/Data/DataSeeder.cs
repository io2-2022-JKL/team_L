using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.Data
{
    public class DataSeeder
    {
        public static void SeedData(AppDBContext context)
        {
            if (!context.Admins.Any())
            {
                var admin = new Admin
                {
                    pesel = "81111111111",
                    dateOfBirth = new DateTime(1981, 11, 11),
                    firstName = "Jan",
                    lastName = "Kowalski",
                    mail = "superadmin@mail.com",
                    phoneNumber = "+48111222333",
                    password = "superadmin!23"
                };
                context.Add(admin);
                context.SaveChanges();
            }
            if (!context.Vaccines.Any())
            {
                List<Vaccine> vaccineList = new List<Vaccine>
                {
                new Vaccine
                {
                    company = "Pfeizer",
                    name = "Pfeizer vaccine",
                    numberOfDoses = 2,
                    minDaysBetweenDoses = 30,
                    minPatientAge = 12,
                    virus = Virus.Coronavirus,
                    active = true
                },
                new Vaccine
                {
                    company = "Moderna",
                    name = "Moderna vaccine",
                    numberOfDoses = 2,
                    minDaysBetweenDoses = 30,
                    minPatientAge = 18,
                    maxPatientAge = 99,
                    virus = Virus.Coronavirus,
                    active = true
                },
                new Vaccine
                {
                    company = "Johnson and Johnson",
                    name = "J&J vaccine",
                    numberOfDoses = 1,
                    minPatientAge = 18,
                    virus = Virus.Coronavirus,
                    active = true
                }};
                context.AddRange(vaccineList);
                context.SaveChanges();
            }
            if (!context.VaccinationCenters.Any())
            {
                Vaccine pfeizer = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First();
                Vaccine jj = context.Vaccines.Where(vaccine => vaccine.company == "Johnson and Johnson").ToList().First();
                Vaccine moderna = context.Vaccines.Where(vaccine => vaccine.company == "Moderna").ToList().First();

                List<VaccinationCenter> centerList = new List<VaccinationCenter>
                {
                    new VaccinationCenter
                    {
                        name = "Punkt Szczepień Populacyjnych",
                        city = "Warszawa",
                        address = "Żwirki i Wigury 95/97",
                        active = true
                    },
                    new VaccinationCenter
                    {
                        name = "Apteczny Punkt Szczepień",
                        city = "Warszawa",
                        address = "Mokotowska 27/Lok.1 i 4",
                        active = true
                    }
                };
                context.VaccinationCenters.AddRange(centerList);
                context.SaveChanges();

                var center1 = context.VaccinationCenters.Where(center => center.name == "Punkt Szczepień Populacyjnych").ToList().First();
                var center2 = context.VaccinationCenters.Where(center => center.name == "Apteczny Punkt Szczepień").ToList().First();

                List<OpeningHours> hours = new List<OpeningHours>()
                {
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Monday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Tuesday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Wednesday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Thursday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Friday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Saturday,
                        vaccinationCenter = center1
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Monday,
                        vaccinationCenter = center2
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Tuesday,
                        vaccinationCenter = center2
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Wednesday,
                        vaccinationCenter = center2
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Thursday,
                        vaccinationCenter = center2
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Friday,
                        vaccinationCenter = center2
                    },
                    new OpeningHours
                    {
                        from = new TimeSpan(8, 0, 0),
                        to = new TimeSpan(20, 0, 0),
                        day = WeekDay.Saturday,
                        vaccinationCenter = center2
                    }
                };
                context.OpeningHours.AddRange(hours);
                context.SaveChanges();

                List<VaccinesInCenters> vaccineList = new List<VaccinesInCenters>()
                {
                    new VaccinesInCenters
                    {
                        vaccine = pfeizer,
                        vaccinationCenter = center1
                    },
                    new VaccinesInCenters
                    {
                        vaccine = jj,
                        vaccinationCenter = center1
                    },
                    new VaccinesInCenters
                    {
                        vaccine = moderna,
                        vaccinationCenter = center1
                    },
                    new VaccinesInCenters
                    {
                        vaccine = pfeizer,
                        vaccinationCenter = center2
                    },
                    new VaccinesInCenters
                    {
                        vaccine = jj,
                        vaccinationCenter = center2
                    }
                };
                context.VaccinesInCenters.AddRange(vaccineList);
                context.SaveChanges();
            }
            if (!context.Patients.Any())
            {
                List<Patient> patientList = new List<Patient>
                {
                    new Patient
                    {
                        pesel = "82121211111",
                        dateOfBirth = new DateTime(1982, 12, 12),
                        firstName = "Jan",
                        lastName = "Nowak",
                        mail = "j.nowak@mail.com",
                        phoneNumber = "+48555221331",
                        password = "password123()",
                        active = true
                    },
                    new Patient
                    {
                        pesel = "92120211122",
                        dateOfBirth = new DateTime(1992, 12, 02),
                        firstName = "Janina",
                        lastName = "Nowakowa",
                        mail = "j.nowakowa@mail.com",
                        phoneNumber = "+48576221390",
                        password = "trudne1haslo2#",
                        active = true
                    },
                    new Patient
                    {
                        pesel = "59062011333",
                        dateOfBirth = new DateTime(1959, 06, 20),
                        firstName = "Robert",
                        lastName = "Weide",
                        mail = "robert.b.weide@mail.com",
                        phoneNumber = "+48125200331",
                        password = "123abc!@#",
                        active = true
                    },
                    new Patient
                    {
                        pesel = "74011011111",
                        dateOfBirth = new DateTime(1974, 01, 10),
                        firstName = "Monika",
                        lastName = "Kowalska",
                        mail = "m.kowalska@mail.com",
                        phoneNumber = "+48349824991",
                        password = "wasd1234lkj098",
                        active = true
                    },
                    new Patient
                    {
                        pesel = "82121211133",
                        dateOfBirth = new DateTime(1982, 12, 12),
                        firstName = "Leon",
                        lastName = "Izabelski",
                        mail = "leonizabel@mail.com",
                        phoneNumber = "+48903251026",
                        password = "4bdyw1#8i",
                        active = true
                    }
                };
                context.AddRange(patientList);
                context.SaveChanges();

                var patientWeide = context.Patients.Where(patient => patient.lastName == "Weide").ToList().First();//Find("Weide");
                var patientKowalska = context.Patients.Where(patient => patient.lastName == "Kowalska").ToList().First();
                var patientNowak = context.Patients.Where(patient => patient.lastName == "Nowak").ToList().First();
                var patientNowakowa = context.Patients.Where(patient => patient.lastName == "Nowakowa").ToList().First();
                var vaccinePfeizer = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First();

                var certificateW = new Certificate
                {
                    url = "placeholder",
                    patientId = patientWeide.id,
                    vaccineId = vaccinePfeizer.id
                };
                var certificateK = new Certificate
                {
                    url = "placeholder",
                    patientId = patientKowalska.id,
                    vaccineId = vaccinePfeizer.id
                };
                var certificateN = new Certificate
                {
                    url = "placeholder",
                    patientId = patientNowak.id,
                    vaccineId = vaccinePfeizer.id
                };
                patientWeide.certificates = new Certificate[] { certificateW };
                patientKowalska.certificates = new Certificate[] { certificateK };
                patientNowak.certificates = new Certificate[] { certificateN };

                List<VaccinationCount> count = new List<VaccinationCount>()
                {
                    new VaccinationCount
                    {
                        patient = patientWeide,
                        virus = Virus.Coronavirus,
                        count = 2
                    },
                    new VaccinationCount
                    {
                        patient = patientKowalska,
                        virus = Virus.Coronavirus,
                        count = 2
                    },
                    new VaccinationCount
                    {
                        patient = patientNowak,
                        virus = Virus.Coronavirus,
                        count = 2
                    },
                    new VaccinationCount
                    {
                        patient = patientNowakowa,
                        virus = Virus.Coronavirus,
                        count = 1
                    }
                };

                context.Certificates.Add(certificateW);
                context.Certificates.Add(certificateK);
                context.Certificates.Add(certificateN);
                context.VaccinationCounts.AddRange(count);
                context.SaveChanges();
            }
            if (!context.Doctors.Any())
            {
                var patientWeide = context.Patients.Where(patient => patient.lastName == "Weide").ToList().First();
                var patientKowalska = context.Patients.Where(patient => patient.lastName == "Kowalska").ToList().First();
                var vaccCenter1 = context.VaccinationCenters.Where(center => center.name == "Punkt Szczepień Populacyjnych").ToList().First();
                var vaccCenter2 = context.VaccinationCenters.Where(center => center.name == "Apteczny Punkt Szczepień").ToList().First();
                var doctorWeide = new Doctor
                {
                    patientAccount = patientWeide,
                    vaccinationCenter = vaccCenter1,
                    active = true
                };
                var doctorKowalska = new Doctor
                {
                    patientAccount = patientKowalska,
                    vaccinationCenter = vaccCenter2,
                    active = true
                };
                vaccCenter1.doctors = new List<Doctor>() { doctorWeide };
                vaccCenter2.doctors = new List<Doctor>() { doctorKowalska };

                context.Doctors.Add(doctorWeide);
                context.Doctors.Add(doctorKowalska);
                context.SaveChanges();
            }
            if (!context.Appointments.Any())
            {
                var doctorWeide = context.Doctors.Where(d => d.patientAccount.lastName == "Weide").ToList().First();
                var doctorKowalska = context.Doctors.Where(d => d.patientAccount.lastName == "Kowalska").ToList().First();
                List<TimeSlot> listSlots = new List<TimeSlot>()
                {
                    new TimeSlot
                    {
                        from = new DateTime(2022, 03, 25, 12, 30, 00),
                        to = new DateTime(2022, 03, 25, 12, 45, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 04, 23, 12, 30, 00),
                        to = new DateTime(2022, 04, 23, 12, 45, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 04, 23, 13, 30, 00),
                        to = new DateTime(2022, 04, 23, 13, 45, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 25, 12, 45, 00),
                        to = new DateTime(2022, 05, 25, 13, 00, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 04, 25, 12, 30, 00),
                        to = new DateTime(2022, 04, 25, 12, 45, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 04, 24, 12, 30, 00),
                        to = new DateTime(2022, 04, 24, 12, 45, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 04, 25, 9, 15, 00),
                        to = new DateTime(2022, 04, 25, 9, 30, 00),
                        isFree = false,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 03, 20, 9, 15, 00),
                        to = new DateTime(2022, 03, 20, 9, 30, 00),
                        isFree = false,
                        active = false,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 9, 15, 00),
                        to = new DateTime(2022, 05, 20, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 9, 30, 00),
                        to = new DateTime(2022, 05, 20, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 10, 15, 00),
                        to = new DateTime(2022, 05, 20, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 10, 30, 00),
                        to = new DateTime(2022, 05, 20, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 9, 15, 00),
                        to = new DateTime(2022, 05, 20, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 9, 30, 00),
                        to = new DateTime(2022, 05, 20, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 10, 15, 00),
                        to = new DateTime(2022, 05, 20, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 20, 10, 30, 00),
                        to = new DateTime(2022, 05, 20, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 9, 15, 00),
                        to = new DateTime(2022, 05, 30, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 9, 30, 00),
                        to = new DateTime(2022, 05, 30, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 10, 15, 00),
                        to = new DateTime(2022, 05, 30, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 10, 30, 00),
                        to = new DateTime(2022, 05, 30, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 9, 15, 00),
                        to = new DateTime(2022, 05, 30, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 9, 30, 00),
                        to = new DateTime(2022, 05, 30, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 10, 15, 00),
                        to = new DateTime(2022, 05, 30, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 05, 30, 10, 30, 00),
                        to = new DateTime(2022, 05, 30, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 9, 15, 00),
                        to = new DateTime(2022, 06, 20, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 9, 30, 00),
                        to = new DateTime(2022, 06, 20, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 10, 15, 00),
                        to = new DateTime(2022, 06, 20, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 10, 30, 00),
                        to = new DateTime(2022, 06, 20, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorKowalska
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 9, 15, 00),
                        to = new DateTime(2022, 06, 20, 9, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 9, 30, 00),
                        to = new DateTime(2022, 06, 20, 9, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                     new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 10, 15, 00),
                        to = new DateTime(2022, 06, 20, 10, 30, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    },
                    new TimeSlot
                    {
                        from = new DateTime(2022, 06, 20, 10, 30, 00),
                        to = new DateTime(2022, 06, 20, 10, 45, 00),
                        isFree = true,
                        active = true,
                        doctor = doctorWeide
                    }
                };
                context.TimeSlots.AddRange(listSlots);
                context.SaveChanges();

                List<TimeSlot> slotsList = context.TimeSlots.Where(slot => slot.isFree == false).ToList();

                List<Appointment> list = new List<Appointment>() {
                    new Appointment
                    {
                        whichDose = 1,
                        timeSlot = slotsList[0],
                        patient = context.Patients.Where(patient => patient.pesel == "82121211111").ToList().First(),
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First(),
                        state = AppointmentState.Finished,
                        vaccineBatchNumber = "abcd",
                        certifyState = CertificateState.NotLast
                    },
                    new Appointment
                    {
                        whichDose = 2,
                        timeSlot = slotsList[1],
                        patient = context.Patients.Where(patient => patient.pesel == "82121211111").ToList().First(),
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First(),
                        state = AppointmentState.Finished,
                        vaccineBatchNumber = "abcd",
                        certifyState = CertificateState.Certified
                    },
                    new Appointment
                    {
                        whichDose = 1,
                        timeSlot = slotsList[2],
                        patient = context.Patients.Where(patient => patient.pesel == "92120211122").ToList().First(),
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First(),
                        state = AppointmentState.Finished,
                        vaccineBatchNumber = "abcd",
                        certifyState = CertificateState.NotLast
                    },
                    new Appointment
                    {
                        whichDose = 2,
                        timeSlot = slotsList[3],
                        patient = context.Patients.Where(patient => patient.pesel == "92120211122").ToList().First(),
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First(),
                        state = AppointmentState.Planned,
                        vaccineBatchNumber = "",
                        certifyState = CertificateState.LastNotCertified
                    },
                    new Appointment
                    {
                        whichDose = 1,
                        timeSlot = slotsList[4],
                        patient = context.Patients.Where(patient => patient.pesel == "82121211133").ToList().First(),
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Johnson and Johnson").ToList().First(),
                        state = AppointmentState.Planned,
                        vaccineBatchNumber = "",
                        certifyState = CertificateState.NotLast
                    },
                    new Appointment
                    {
                        whichDose = 2,
                        timeSlot = slotsList[5],
                        patient = context.Patients.Where(patient => patient.pesel == "59062011333").ToList().First(), // doctorWeide szczepil sie u doctorKowalska :)
                        vaccine = context.Vaccines.Where(vaccine => vaccine.company == "Pfeizer").ToList().First(),
                        state = AppointmentState.Finished,
                        vaccineBatchNumber = "abcd",
                        certifyState = CertificateState.Certified
                    }
                };
                context.Appointments.AddRange(list);
                context.SaveChanges();
            }
        }
    }
}
