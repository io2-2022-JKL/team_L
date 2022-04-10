using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace VaccinationSystem.Services
{
    public class SQLServerLocalDB : IDatabase
    {
        private AppDBContext dbContext;
        public SQLServerLocalDB(AppDBContext context)
        {
            dbContext = context;
        }
        public async Task<Patient> GetPatient(int id)
        {
            return await dbContext.Patients.FindAsync(id);
        }
        public async Task<List<Patient>> GetPatients()
        {
            return await dbContext.Patients.ToListAsync();
        }
    }
}