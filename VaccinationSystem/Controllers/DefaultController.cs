using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Services;
using VaccinationSystem.Data;


namespace VaccinationSystem.Controllers
{
    [Route("")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IUserSignInManager signInManager;
        private IDatabase dbManager;
        public DefaultController(IUserSignInManager signInManager, IDatabase db)
        {
            this.signInManager = signInManager;
            dbManager = db;
        }

        [Route("/register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisteringPatient patient)
        {
            if (ModelState.IsValid)
            {
                if (dbManager.AddPatient(patient))
                    return Ok();
            }
            
            return BadRequest();

        }
        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] Login login) 
        {
            if (ModelState.IsValid)
            {
                var token = signInManager.SignIn(login.mail, login.password);
                if (token == null)
                    return BadRequest();

                return Ok(new { token = token });
            }
            else
                return BadRequest();
        }
        [Route("/user/Logout/{userId}")]
        [HttpPost]
        public IActionResult Post([FromRoute] string userId)
        {

            signInManager.SignOut(userId);
            return Ok();
        }

    }
}
