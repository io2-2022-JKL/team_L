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

                var openingHours = new OpeningHours
                {
                    from = new TimeSpan(8, 0, 0),
                    to = new TimeSpan(20, 0, 0)
                };

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
                centerList[0].availableVaccines = new Vaccine[] { pfeizer, jj, moderna };
                centerList[1].availableVaccines = new Vaccine[] { pfeizer, jj, moderna };
                for (int i = 0; i < 7; i++)
                {
                    centerList[0].openingHours[i] = openingHours;
                    centerList[1].openingHours[i] = openingHours;
                }
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
                        pesel = "82121211111",
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
                patientWeide.certificates = new Certificate[] { certificateW };//patientWeide.certificates.Append(certificateW);
                patientKowalska.certificates = new Certificate[] { certificateK };

                context.Add(certificateW);
                context.Add(certificateK);
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
                    pesel = "59062011333",
                    dateOfBirth = new DateTime(1959, 06, 20),
                    firstName = "Robert",
                    lastName = "Weide",
                    mail = "robert.b.weide@mail.com",
                    phoneNumber = "+48125200331",
                    password = "123abc!@#",
                    patientAccount = patientWeide,
                    vaccinationCenter = vaccCenter1,
                    active = true
                };
                var doctorKowalska = new Doctor
                {
                    pesel = "74011011111",
                    dateOfBirth = new DateTime(1974, 01, 10),
                    firstName = "Monika",
                    lastName = "Kowalska",
                    mail = "m.kowalska@mail.com",
                    phoneNumber = "+48349824991",
                    password = "wasd1234lkj098",
                    patientAccount = patientKowalska,
                    vaccinationCenter = vaccCenter2,
                    active = true
                };
                vaccCenter1.doctors = new List<Doctor>() { doctorWeide};
                vaccCenter2.doctors = new List<Doctor>() { doctorKowalska };

                context.Doctors.Add(doctorWeide);
                context.Doctors.Add(doctorKowalska);
                context.SaveChanges();
            }
            if (!context.Appointments.Any()) 
            { 

            }
        }
    }
}
