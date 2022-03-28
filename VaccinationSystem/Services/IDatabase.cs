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
        public bool AreCredentialsValid(Login login);
        public List<Patient> GetPatients();
    }
}
