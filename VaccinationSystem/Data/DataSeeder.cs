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
                    //id = 1,
                    pesel = "81111111111",
                    dateOfBirth = new DateTime(1981, 11, 11),
                    firstName = "Jan",
                    lastName = "Kowalski",
                    mail = "superadmin@mail.com",
                    phoneNumber = "+48111222333",
                    password = "superadmin!23"
                };
                //context.AddRange(list);
                context.Add(admin);
                context.SaveChanges();
            }
            if (!context.Patients.Any())
            {
                var patient = new Patient
                {
                    pesel = "82121211111",
                    dateOfBirth = new DateTime(1982, 12, 12),
                    firstName = "Jan",
                    lastName = "Nowak",
                    mail = "j.nowak@mail.com",
                    phoneNumber = "+48555221331",
                    password = "password123()",
                    vaccinationHistory = { },
                    futureVaccinations = { },
                    certificates = { },
                    active = true
                };
                var certificate = new Certificate
                {
                    url = "placeholder"
                };
                //patient.certificates = patient.certificates.Concat(new[] { new Certificate { url = "placeholder" } });
                /*var patients = new List<Patient>
                {
                    new Patient {
                        pesel = "82121211111",
                        dateOfBirth = new DateTime(1982, 12, 12),
                        firstName = "Jan",
                        lastName = "Nowak",
                        mail = "j.nowak@mail.com",
                        phoneNumber = "+48555221331",
                        password = "password123()",
                        vaccinationHistory = { },
                        futureVaccinations = { },
                        certificates = { }
                    }
                };*/
                context.Add(patient);
                context.Add(certificate);
                //context.Certificates.First
                context.SaveChanges();
            }

            var tmp = context.Certificates.First();
        }
    }
}
