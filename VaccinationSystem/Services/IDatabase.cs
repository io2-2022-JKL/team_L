using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;
using VaccinationSystem.DTOs;

namespace VaccinationSystem.Services
{
    public interface IDatabase
    {
        public void AddPatient(RegisteringPatient patient);
        public bool IsUserInDatabase(string email);
        public Guid AreCredentialsValid(Login login);
        public List<Patient> GetPatients();
        public Task<List<VaccinationCenterResponse>> GetVaccinationCenters();
        public Task<bool> EditVaccinationCenter(EditedVaccinationCenter center);
        public Task<bool> DeleteVaccinationCenter(Guid vaccinationCenterId);
        public Task<Vaccine> GetVaccine(Guid vaccineId);
        public Task<List<Vaccine>> GetVaccinesFromVaccinationCenter(Guid vaccinationCenterId);
        public Task<List<OpeningHoursDays>> GetOpeningHoursFromVaccinationCenter(Guid vaccinationCenterId);
        public Task<List<Doctor>> GetDoctorsFromVaccinationCenter(Guid vaccinationCenterId);
        public Task AddVaccinationCenter(AddVaccinationCenterRequest center);
        public Task<bool> EditPatient(EditedPatient patient);
        public Task<bool> DeletePatient(Guid patientId);
    }
}
