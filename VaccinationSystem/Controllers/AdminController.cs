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
using VaccinationSystem.Services;

namespace VaccinationSystem.Contollers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IDatabase dbManager;

        public AdminController(IDatabase db)
        {
            dbManager = db;
        }

        [Route("patients")]
        [HttpGet]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            var patients = await dbManager.GetPatients();
            if (patients != null || patients.Count != 0)
                return Ok(patients);
            else
                return NotFound("Data not found");
        }

        [Route("patients/{patientId}")]
        [HttpGet]
        public async Task<ActionResult<Patient>> GetPatient([FromRoute] int patientId)
        {
            var patient = await dbManager.GetPatient(patientId);
            if (patient != null)
                return Ok(patient);
            else
                return NotFound("Data not found");
        }
    }
}
