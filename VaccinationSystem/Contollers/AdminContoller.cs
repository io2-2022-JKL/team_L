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

        [Route("doctors")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var doctors = await dbManager.GetDoctors();
            if (doctors != null || doctors.Count != 0)
                return Ok(doctors);
            else
                return NotFound("Data not found");
        }
    }
}