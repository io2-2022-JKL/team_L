using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Services
{
    interface IUserSignInManager
    {
        public void SignIn(string email, string password);
        public (bool valid, string user) IsValid(string token);
        public void SignOut(string token);
    }
}
