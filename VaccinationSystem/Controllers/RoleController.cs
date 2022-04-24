﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Controllers
{
    public class RoleController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }
        //public ViewResult Index() => View(roleManager.Roles);
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
    }
}
