using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VaccinationSystem.Services
{
    interface IToken
    {
        public void Refresh();

    }
}
