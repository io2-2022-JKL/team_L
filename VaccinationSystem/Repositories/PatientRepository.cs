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

        public async Task<IEnumerable<Patient>> Get()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient> Get(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        //public async Task<Patient> Create(Patient patient)
        //{
        //    _context.Patients.Add(patient);
        //    await _context.SaveChangesAsync();

        //    return book;
        //}

        //public async Task Delete(int id)
        //{
        //    var patientToDelete = await _context.Patients.FindAsync(id);
        //    _context.Patients.Remove(patientToDelete);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task Update(Patient patient)
        //{
        //    _context.Entry(book).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();
        //}
    }
}
