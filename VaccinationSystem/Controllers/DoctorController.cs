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
using VaccinationSystem.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace VaccinationSystem.Contollers
{
    [Route("doctor")]
    [ApiController]
    [Authorize(Roles = "Doctor")]
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
        [HttpGet]
        [Route("timeSlots/{doctorId}")]
        public async Task<IActionResult> GetTimeSlots([FromRoute] Guid doctorId)
        {
            List<TimeSlotsResponse> timeSlots;
            try
            {
                timeSlots = await dbManager.GetTimeSlots(doctorId);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (timeSlots == null || timeSlots.Count == 0)
                return NotFound("Data not found");

            return Ok(timeSlots);
        }
        [HttpPost]
        [Route("timeSlots/create/{doctorId}")]
        public async Task<IActionResult> CreateTimeSlots([FromRoute] Guid doctorId, [FromBody] CreateNewVisitRequest newVisit)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            try
            {
                await dbManager.CreateTimeSlots(doctorId, newVisit);

            }
            catch (ArgumentException _)
            {
                return NotFound("Data not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Something went wrong");
            }


            return Ok("TIme slot added");
        }

        [HttpPost]
        [Route("timeSlots/delete/{doctorId}")]
        public async Task<IActionResult> DeleteTimeSlot([FromRoute] Guid doctorId, [FromBody] List<DeleteTimeSlot> ids)
        {

            bool deleted = false;
            try
            {
                deleted = await dbManager.DeleteTimeSlots(doctorId, ids);
            }
            catch (ArgumentException )
            {
                return StatusCode(403,"Usr forbidden from deleting");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok("Time slots deleted");

            return NotFound("Data not found");
        }

        [HttpPost]
        [Route("timeSlots/modify/{doctorId}/{timeSlotId}")]
        public async Task<IActionResult> ModifyTimeSlot([FromRoute] Guid doctorId, [FromRoute] Guid timeSlotId, [FromBody] EditedTimeSlot slot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            bool deleted = false;
            try
            {
                deleted = await dbManager.EditTimeSlot(doctorId, timeSlotId, slot);
            }
            catch (ArgumentException)
            {
                return StatusCode(403,"Usr forbidden from modifying");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok("Time slots modified");

            return NotFound("Data not found");
        }
    }
}
