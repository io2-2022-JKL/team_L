using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> Get();
        //Task<Doctor> Get(int id);
        //Task<Patient> Create(Patient patient);
        //Task Update(Patient patient);
        //Task Delete(int id);
    }
}