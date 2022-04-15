using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;

namespace VaccinationSystem.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly AppDBContext _context;

        public DoctorRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Doctor>> Get()
        {
            return await _context.Doctors.ToListAsync();
        }
    }
}