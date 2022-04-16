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

namespace VaccinationSystem.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : Controller
    {
        private IDatabase dbManager;

        public PatientController(IUserSignInManager signInManager, IDatabase db)
        {
            dbManager = db;
        }
        [Route("certificates/{patientId}")]
        [HttpGet]
        public async Task<IActionResult> GetCertificates([FromRoute] Guid patientId)
        {
            var certificates = await dbManager.GetCertificates(patientId);
            if (certificates != null && certificates.Count != 0)
                return Ok(certificates);
            else
                return NotFound("Data not found");
        }
    }
}
