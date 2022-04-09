using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Services;
using VaccinationSystem.Models;

namespace VaccinationSystem.Controllers
{
    [Route("admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IDatabase dbManager;
        public AdminController(IUserSignInManager signInManager, IDatabase db)
        {
            dbManager = db;
        }

        [HttpPost]
        [Route("vaccinationCenter/showVaccinationCenters")]
        public async Task<IActionResult> ShowVaccinationCenters([FromBody]VCCriteria criteria)
        {
            //ToDo
            //check if admin


            List<VaccinationCenter> centers;
            try
            {
                centers = await dbManager.GetVaccinationCenters(criteria);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NotFound("Data not found");
            }

            if (centers == null || centers.Count == 0)
                return NotFound("Data not found");

            return Ok(new { data = centers });
        }

        [HttpPost]
        [Route("vaccinationCenter/editVaccinationCenter")]
        public async Task<IActionResult> EditVaccinationCenter([FromBody] EditedVaccinationCenter center)
        {
            //ToDo
            //check rights

            bool edited = false;
            try
            {
                edited = await dbManager.EditVaccinationCenter(center);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if(edited)
                return Ok();

            return NotFound("Data not found");
        }
        [HttpDelete]
        [Route("vaccinationCenter/deleteVaccinationCenter/{vaccinationCenterId}")]
        public async Task<IActionResult> DeleteVaccinationCenter([FromRoute] Guid vaccinationCenterId)
        {
            //ToDo
            //check rights

            bool deleted = false;
            try
            {
                deleted = await dbManager.DeleteVaccinationCenter(vaccinationCenterId);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok();

            return NotFound("Data not found");
        }
        [HttpPost]
        [Route("patients/editPatient")]
        public async Task<IActionResult> EditPatient([FromBody] EditedPatient patient)
        {
            //autoryzacja

            bool edited = false;
            try
            {
                edited = await dbManager.EditPatient(patient);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (edited)
                return Ok();

            return NotFound("Data not found");
        }
        [HttpDelete]
        [Route("patients/deletePatient/{patientId}")]
        public async Task<IActionResult> DeletePatient([FromRoute] Guid patientId)
        {
            //autoryzacja

            bool deleted = false;
            try
            {
                deleted = await dbManager.DeletePatient(patientId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest("Something went wrong");
            }

            if (deleted)
                return Ok();

            return NotFound("Data not found");
        }
    }
}
