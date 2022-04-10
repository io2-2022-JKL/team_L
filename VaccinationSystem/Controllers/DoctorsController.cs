using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.DTOs;
using VaccinationSystem.Models;
using VaccinationSystem.Services;

namespace VaccinationSystem.Controllers
{
    [Route("doctor")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private IDatabase dbManager;
        public DoctorsController(IUserSignInManager signInManager, IDatabase db)
        {
            dbManager = db;
        }

        [HttpGet]
        [Route("timeSlots/{doctorId}")]
        public async Task<IActionResult> GetTimeSlots([FromRoute]Guid doctorId)
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
            catch(ArgumentException _)
            {
                return NotFound("Data not found");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Something went wrong");
            }


            return Ok("Doctor added");
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
            catch(ArgumentException _)
            {
                return Forbid("Usr forbidden from deleting");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok("Time slots deleted");

            return NotFound("Data not found");
        }

        [HttpPost]
        [Route("timeSlots/modify/{doctorId}/{timeSlotId}")]
        public async Task<IActionResult> ModifyTimeSlot([FromRoute] Guid doctorId, [FromRoute] Guid timeSlotId, [FromBody]  EditedTimeSlot slot)
        {

            bool deleted = false;
            try
            {
                deleted = await dbManager.EditTimeSlot(doctorId, timeSlotId, slot);
            }
            catch (ArgumentException _)
            {
                return Forbid("Usr forbidden from modifying");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok("Time slots modified");

            return NotFound("Data not found");
        }
    }
}
