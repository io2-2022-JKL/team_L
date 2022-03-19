using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VaccinationSystem.Controllers
{
    [Route("")]
    [ApiController]
    public class DefaultController : ControllerBase
    {

        [Route("/register")]
        [HttpPost]
        public IActionResult Register([FromBody] Patient patient)
        {
            return NotFound("Not implemented yet");
        }
        [Route("/Signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] Login login)
        {
            return NotFound("Not implemented yet");

        }
        [Route("/user/Logout/{userId}")]
        [HttpPost]
        public IActionResult Post([FromRoute] string userId)
        {
            return NotFound("Not implemented yet");

        }

    }
}
