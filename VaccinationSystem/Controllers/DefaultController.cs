using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Services;
using VaccinationSystem.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace VaccinationSystem.Controllers
{
    [Route("")]
    [ApiController]
    [Authorize]
    public class DefaultController : ControllerBase
    {
        private IUserSignInManager signInManager;
        private IDatabase dbManager;
        //private UserManager<User> userManager;
        public DefaultController(IUserSignInManager signInManager, IDatabase db)
        {
            this.signInManager = signInManager;
            dbManager = db;
            //userManager = userMgr;
        }

        [Route("/register")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register([FromBody] RegisteringPatient patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (!dbManager.IsUserInDatabase(patient.mail))
                        dbManager.AddPatient(patient);
                    else
                        return BadRequest("Patient already exists");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest();
                }
                
                return Ok();
            }
            
            return BadRequest("Bad arguments");

        }
        [Route("/signin")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SignIn([FromBody] Login login) 
        {
            if (ModelState.IsValid)
            {
                string token = "";
                LoginResponse lR = new LoginResponse() { userId = Guid.Empty, userType = "" };
                try
                {
                    lR = dbManager.AreCredentialsValid(login);
                    
                    //Console.WriteLine(userId);
                    if(lR.userId!=Guid.Empty)
                        token = signInManager.SignIn(login.mail, login.password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (lR.userId == Guid.Empty)
                    return BadRequest("Credentials are not valid");

                return Ok(lR);
            }
            else
                return BadRequest("Bad arguments");
        }
        [Route("/user/logout/{userId}")]
        [HttpGet]
        public IActionResult Logout([FromRoute] string userId)
        {

            //signInManager.SignOut(userId);
            return Ok("OK, successfully logged out");
        }

    }
}
