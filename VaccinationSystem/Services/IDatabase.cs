using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;

namespace VaccinationSystem.Services
{
    public interface IDatabase
    {
        public void AddPatient(RegisteringPatient patient);
        public bool IsUserInDatabase(string email);
        public Guid AreCredentialsValid(Login login);
        public List<Patient> GetPatients();
        public Task<List<VaccinationCenter>> GetVaccinationCenters(VCCriteria crietria);
        public Task<bool> EditVaccinationCenter(EditedVaccinationCenter center);
        public Task<bool> DeleteVaccinationCenter(Guid vaccinationCenterId);
        public Task<bool> EditPatient(EditedPatient patient);
        public Task<bool> DeletePatient(Guid patientId);
        public Task<bool> AddDoctor(RegisteringDoctor doctor);
        public Task<bool> EditDoctor(EditedDoctor doctor);
        public Task<bool> DeleteDoctor(Guid doctorId);
    }
}
