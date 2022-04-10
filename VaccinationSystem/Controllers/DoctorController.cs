using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.Data;
using VaccinationSystem.Models;
using VaccinationSystem.Services;

namespace VaccinationSystem.Contollers
{
    [Route("doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDatabase dbManager;

        public DoctorController(IUserSignInManager signInManager, IDatabase db)
        {
            dbManager = db;
        }
        [Route("patients/{patientId}")]
        [HttpGet]
        public async Task<IActionResult> GetPatient([FromRoute] Guid patientId)
        {
            var patient = await dbManager.GetPatient(patientId);
            if (patient != null)
                return Ok(patient);
            else
                return NotFound("Data not found");
        }
    }
}
