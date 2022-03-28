using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaccinationSystem.Data;
using VaccinationSystem.Models;

namespace VaccinationSystem.Contollers
{
    public class DoctorsController : Controller
    {
        private readonly AppDBContext _context;

        public DoctorsController(AppDBContext context)
        {
            _context = context;
        }

        [Route("/admin/doctors/showDoctors")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors(Doctor doctor)
        {
            var doctors = _context.Doctors.Where(d => d.pesel == doctor.pesel &&
                                                d.firstName == doctor.firstName &&
                                                d.lastName == doctor.lastName &&
                                                d.mail == doctor.mail &&
                                                d.dateOfBirth == doctor.dateOfBirth &&
                                                d.phoneNumber == doctor.phoneNumber &&
                                                d.vaccinationCenter == doctor.vaccinationCenter
                ).ToListAsync();
            return await doctors;
        }
    }
}
