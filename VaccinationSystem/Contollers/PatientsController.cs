using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.Data;
using VaccinationSystem.Models;

namespace VaccinationSystem.Contollers
{
    [Route("")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PatientsController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Patients

        [Route("/admin/patients/showPatients")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients(Patient patient)
        {
            var patients = _context.Patients.Where(p =>  p.pesel==patient.pesel &&
                                                p.firstName == patient.firstName && 
                                                p.lastName == patient.lastName && 
                                                p.mail == patient.mail && 
                                                p.dateOfBirth == patient.dateOfBirth &&
                                                p.phoneNumber == patient.phoneNumber
                ).ToListAsync();
            return await patients;
        }
    }
}
