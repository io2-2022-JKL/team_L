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
        private AppDBContext dbContext;
        public DefaultController(IUserSignInManager signInManager, AppDBContext dbContext)
        {
            this.signInManager = signInManager;
            this.dbContext = dbContext;
        }

        [Route("/register")]
        [HttpPost]
        public IActionResult Register([FromBody] RegisteringPatient patient)
        {
            if (ModelState.IsValid)
            {
                return Ok("Not implemented yet");
            }
            else return BadRequest();

        }
        [Route("/signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] Login login)
        {
            Console.WriteLine("abc");
            if (dbContext.Database.EnsureCreated())
                Console.WriteLine(true);
            else
                Console.WriteLine(false);
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
