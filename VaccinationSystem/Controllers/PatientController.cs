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

        [HttpGet]
        [Route("timeSlots/Filter")]
        public async Task<IActionResult> GetTimeSlots(string city, string virus, string dateFrom, string dateTo)
        {
            if (city == null || virus == null || dateFrom == null || dateTo == null)
                return BadRequest("Invalid model");

            var filter = new TimeSlotsFilter()
            {
                city = city,
                virus = virus,
                dateFrom = dateFrom,
                dateTo = dateTo
            };
            List<FilterTimeSlotResponse> timeSlots;
            try
            {
                timeSlots = await dbManager.GetTimeSlotsWithFiltration(filter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Something went wrong");
            }


            if (timeSlots == null || timeSlots.Count == 0)
                return NotFound("Data not found");

            var response = new FilterTimeSlotsControllerResponse() { data = timeSlots };
            return Ok(response);
        }

        [Route("certificates/{patientId}")]
        [HttpGet]
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

        [Route("appointments/incomingAppointments/{patientId}")]
        [HttpGet]
        public async Task<IActionResult> GetIncomingAppointments([FromRoute] Guid patientId)
        {
            List<IncomingAppointmentResponse> incApps;
            try
            {
                incApps = await dbManager.GetIncomingAppointments(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (incApps == null || incApps.Count == 0)
                return NotFound("Data not found");

            return Ok(incApps);
        }
    }
}