using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Services
{
    public class DefaultSignInManager: IUserSignInManager
    {
        private Dictionary<string, DefaultToken> signedInUsers = new Dictionary<string, DefaultToken>();

        public (bool valid, string user) IsValid(string tokenValue)
        {
            var token = signedInUsers.Where(p => p.Value.Value == tokenValue).Select(p => p.Value).FirstOrDefault();
            if (token == null || !token.IsValid)
                return (false, null);
            else
            {
                token.Refresh();
                return (true, token.Owner);
            }
        }


        public string SignIn(string email, string password)
        {
            if(!signedInUsers.ContainsKey(email))
            {
                var token = new DefaultToken(email);
                signedInUsers.Add(email, new DefaultToken(email));
                return token.Value;
            }

            return null;
        }
        public void SignOut(string tokenValue)
        {
            foreach( var user in signedInUsers.Where(p => p.Value.Value == tokenValue))
            {
                signedInUsers.Remove(user.Key);
            }
        }
        
    }
}
