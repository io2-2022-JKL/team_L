using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;

namespace VaccinationSystem.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDBContext _context;

        public PatientRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<List<Patient>> Get()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> Get(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
    }
}
