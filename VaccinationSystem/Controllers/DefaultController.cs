using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using VaccinationSystem.Services;
using VaccinationSystem.Data;
using VaccinationSystem.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.Extensions.Configuration;


namespace VaccinationSystem.Controllers
{
    [Route("")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IUserSignInManager signInManager;
        private IDatabase dbManager;
        private IConfiguration configuration;
        public DefaultController(IUserSignInManager signInManager, IDatabase db, IConfiguration config = null)
        {
            this.signInManager = signInManager;
            dbManager = db;
            configuration = config;
        }

        [Route("/register")]
        [HttpPost]
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
                    //if(userId!=Guid.Empty)
                      //  token = signInManager.SignIn(login.mail, login.password);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                if (lR.userId == Guid.Empty)
                    return BadRequest("Credentials are not valid");

                string jwtToken = CreateAuthorizationToken(login, lR);
                HttpContext.Response.Headers.Add(HeaderNames.Authorization, jwtToken);

                return Ok(lR);
            }
            else
                return BadRequest("Bad arguments");
        }

        private string CreateAuthorizationToken(Login login, LoginResponse loginResponse)
        {
            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.AddHours(2).ToString()),
                        new Claim("UserId", loginResponse.userId.ToString()),
                        new Claim("Email", login.mail)
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Route("/user/logout/{userId}")]
        [HttpGet]
        public IActionResult Logout([FromRoute] string userId)
        {

            //signInManager.SignOut(userId);
            return Ok("OK, successfully logged out");
        }

        [Route("/cities")]
        [HttpGet]
        public async Task<IActionResult> GetCities()
        {
            List<CityResponse> cities;
            try
            {
                cities = await dbManager.GetCities();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (cities == null ||cities.Count == 0)
                return NotFound("Data not found");

            return Ok(cities);
        }

        [Route("/viruses")]
        [HttpGet]
        public async Task<IActionResult> GetViruses()
        {
            List<VirusResponse> viruses;
            try
            {
                viruses = dbManager.GetViruses();
            }
            catch (Exception)
            {
                return BadRequest("Something went wrong");
            }

            if (viruses == null || viruses.Count == 0)
                return NotFound("Data not found");

            return Ok(viruses);
        }

    }
}
