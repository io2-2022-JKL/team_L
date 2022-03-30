using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.Data;
using VaccinationSystem.Models;
using VaccinationSystem.Repositories;

namespace VaccinationSystem.Contollers
{
    [Route("")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientsController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // GET: api/Patients

        [Route("/admin/patients")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
        {
            var patients = _patientRepository.Get();
            return Ok(await patients);
        }

        [Route("/admin/patients/{patientId}")]
        [Route("/doctor/patients/{patientId}")]
        [HttpGet]
        public async Task<ActionResult<Patient>> GetPatients([FromRoute] int patientId)
        {
            return Ok(await _patientRepository.Get(patientId));
        }
    }
}
