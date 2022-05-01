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

namespace VaccinationSystem.Contollers
{
    [Route("doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private IDatabase dbManager;
        private ICertificateGenerator certGenerator;

        public DoctorController(IUserSignInManager signInManager, IDatabase db, ICertificateGenerator certificateGenerator = null)
        {
            dbManager = db;
            certGenerator = certificateGenerator;
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
                return StatusCode(403, "Usr forbidden from modifying");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok("Time slots modified");

            return NotFound("Data not found");
        }
        
        [Route("info/{doctorId}")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorInfo([FromRoute] Guid doctorId)
        {
            DoctorInfoResponse doctorInfo;
            try
            {
                doctorInfo = await dbManager.GetDoctorInfo(doctorId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }
            if (doctorInfo != null)
                return Ok(doctorInfo);
            return NotFound("Data not found");
        }
        [HttpPost]
        [Route("/doctor/vaccinate/certify/{doctorId}/{appointmentId}")]
        public async Task<IActionResult> Certify([FromRoute] Guid doctorId, [FromRoute] Guid appointmentId)
        {

            bool created;
            try
            {
                var a = await dbManager.GetAppointment(appointmentId);
                var d = await dbManager.GetDoctor(doctorId);

                if (a == null || d == null)
                    return NotFound("Data not found");

                var p = a.patient;
                var vc = d.vaccinationCenter;

                string url = await certGenerator.Generate(p.firstName + " " + p.lastName, p.dateOfBirth, p.pesel, vc.name, vc.city + " " 
                    + vc.address, a.vaccine.name, a.whichDose, a.vaccineBatchNumber);

                created = await dbManager.CreateCertificate(doctorId, appointmentId, url);
            }
            catch (ArgumentException)
            {
                return StatusCode(403, "User forbidden from creating vaccine certification");
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (created)
                return Ok("Certificate created");
            return NotFound("Data not found");
        }
        [Route("incomingAppointments/{doctorId}")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorIncomingAppointments([FromRoute] Guid doctorId)
        {
            List<DoctorIncomingAppResponse> incApps;
            try
            {
                incApps = await dbManager.GetDoctorIncomingAppointments(doctorId);
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
        [Route("formerAppointments/{doctorId}")]
        [HttpGet]
        public async Task<IActionResult> GetDoctorFormerAppointments([FromRoute] Guid doctorId)
        {
            List<DoctorFormerAppResponse> formerApps;
            try
            {
                formerApps = await dbManager.GetDoctorFormerAppointments(doctorId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (formerApps == null || formerApps.Count == 0)
                return NotFound("Data not found");
            return Ok(formerApps);
        }
    }
}
