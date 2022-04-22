using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        {
            return NotFound("Data not found");
        }
    }
}
