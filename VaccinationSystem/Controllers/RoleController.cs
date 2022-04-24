using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccinationSystem.Models;

namespace VaccinationSystem.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;
        public RoleController(RoleManager<IdentityRole> roleMgr, UserManager<User> userMgr)
        {
            roleManager = roleMgr;
            userManager = userMgr;
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        public async Task AddRolesAsync()
        {
            IdentityResult result = await roleManager.CreateAsync(new IdentityRole("Admin"));
            if (!result.Succeeded)
                Errors(result);
            result = await roleManager.CreateAsync(new IdentityRole("Doctor"));
            if (!result.Succeeded)
                Errors(result);
            result = await roleManager.CreateAsync(new IdentityRole("Patient"));
            if (!result.Succeeded)
                Errors(result);
        }
        public async void Update(string name)
        {
            IdentityRole role = await roleManager.FindByNameAsync(name);
            List<User> members = new List<User>();
            List<User> nonMembers = new List<User>();
            foreach(User user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
        }
    }
}
