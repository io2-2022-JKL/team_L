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
        public Task<List<Patient>> GetPatients();
        public Task<Patient> GetPatient(int id);
    }
}