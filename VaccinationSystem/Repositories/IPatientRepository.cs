using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> Get();
        Task<Patient> Get(int id);
    }
}