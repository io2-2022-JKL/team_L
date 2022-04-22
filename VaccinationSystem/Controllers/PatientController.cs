using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Services;
using VaccinationSystem.DTOs;

namespace VaccinationSystem.Controllers
{
    [Route("patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private IUserSignInManager signInManager;
        private IDatabase dbManager;
        public PatientController(IUserSignInManager signInManager, IDatabase db)
        {
            this.signInManager = signInManager;
            dbManager = db;
        }

        [Route("timeSlots/book/{patientId}/{timeSlotId}/{vaccineId}")]
        [HttpPost]
        public async Task<IActionResult> MakeAppointment([FromRoute] Guid patientId, [FromRoute] Guid timeSlotId, [FromRoute] Guid vaccineId)
        {
            bool made = false;
            try
            {
                made = await dbManager.MakeAppointment(patientId, timeSlotId, vaccineId);
            }
            catch (ArgumentException)
            {
                return StatusCode(403, "User forbidden from booking");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (made)
                return Ok("Booked a time slot");

            return NotFound("Data not found");
        }

        [Route("certificates/{patientId}")]
        [HttpPost]
        public async Task<IActionResult> GetCertificates([FromRoute] Guid patientId)
        {
            List<CertificatesResponse> certs;
            try
            {
                certs = await dbManager.GetCertificates(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (certs == null || certs.Count == 0)
                return NotFound("Data not found");

            return Ok(certs);
        }
    }
}