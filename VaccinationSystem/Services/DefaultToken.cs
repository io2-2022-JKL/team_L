using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Services
{
    public class DefaultToken : IToken
    {
        public string Value { get; private set; }
        public DateTime LastAccessDate { get; private set; }
        public string Owner { get; private set; }
        public bool IsValid { get => LastAccessDate.AddHours(1) <= DateTime.Now; }
        
        public DefaultToken(string owner)
        {
            Owner = owner;
            LastAccessDate = DateTime.Now;
            Value = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }

        public void Refresh()
        {
            LastAccessDate = DateTime.Now;
        }
    }
}
