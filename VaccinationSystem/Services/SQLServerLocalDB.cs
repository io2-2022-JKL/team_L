using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;
using Microsoft.EntityFrameworkCore;


namespace VaccinationSystem.Services
{
    public class SQLServerLocalDB:IDatabase
    {
        private AppDBContext dbContext;
        public SQLServerLocalDB(AppDBContext context)
        {
            dbContext = context;
        }

        public void AddPatient(RegisteringPatient patient)
        {
            Patient p = new Patient
            {
                pesel = patient.PESEL,
                firstName = patient.Names,
                lastName = patient.Password,
                mail = patient.Mail,
                password = patient.Password,
                phoneNumber = patient.PhoneNumber,
                active = true,
                certificates = { },
                vaccinationHistory = { },
                futureVaccinations = { }
            };


            dbContext.Patients.Add(p);
            dbContext.SaveChanges();

        }

        public Guid AreCredentialsValid(Login login)
        {
            var patient = dbContext.Patients.Where(p => p.mail.CompareTo(login.mail)==0).FirstOrDefault();
            if (patient != null && patient.password.CompareTo(login.password) == 0)
                return patient.id;

            return Guid.Empty;
        }

        public List<Patient> GetPatients()
        {
            return dbContext.Patients.ToList();
        }

        public bool IsUserInDatabase(string email)
        {
            int emailOccurance = dbContext.Patients.Where(p => p.mail.CompareTo(email)==0).Count();

            if (emailOccurance > 0)
                return true;
            
            return false;

        }

        public async Task<List<VaccinationCenter>> GetVaccinationCenters(VCCriteria crietria)
        {
            var centers = await dbContext.VaccinationCenters.ToListAsync();

            return centers.Where(center =>
            {
                if (crietria.Name != null && center.name.CompareTo(crietria.Name) != 0)
                    return false;
                if (crietria.City != null && center.city.CompareTo(crietria.City) != 0)
                    return false;
                if (crietria.Street != null && center.address.CompareTo(crietria.Street) != 0)
                    return false;

                return true;
            }).ToList();
        }

        public async Task<bool> EditVaccinationCenter(EditedVaccinationCenter center)
        {
            var dbCenter = await dbContext.VaccinationCenters.SingleAsync(c => c.id == center.VaccinationCentersId);

            if (dbCenter != null)
            {
                dbCenter.name = center.Name;
                dbCenter.city = center.City;
                dbCenter.address = center.Street;

                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteVaccinationCenter(Guid vaccinationCenterId)
        {
            var dbCenter = await dbContext.VaccinationCenters.SingleAsync(c => c.id == vaccinationCenterId);

            if(dbCenter!=null)
            {
                dbContext.VaccinationCenters.Remove(dbCenter);

                await dbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> EditPatient(EditedPatient patient)
        {
            var dbPatient = await dbContext.Patients.SingleAsync(p => p.id == patient.id);
            if (dbPatient != null)
            {
                dbPatient.dateOfBirth = patient.dateOfBirth;
                dbPatient.firstName = patient.firstName;
                dbPatient.lastName = patient.lastName;
                dbPatient.mail = patient.mail;
                dbPatient.pesel = patient.pesel;
                dbPatient.phoneNumber = patient.phoneNumber;

                return true;
            }
            return false;
        }

        public async Task<bool> DeletePatient(Guid patientId)
        {
            var dbPatient = await dbContext.Patients.SingleAsync(p => p.id == patientId);
            if (dbPatient != null)
            {
                dbContext.Patients.Remove(dbPatient);
                await dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
