using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.Data;
using VaccinationSystem.Models;
using VaccinationSystem.Repositories;

namespace VaccinationSystem.Contollers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: api/Patients

        [Route("/admin/doctors")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            var patients = _doctorRepository.Get();
            return Ok(await patients);
        }
    }
}
