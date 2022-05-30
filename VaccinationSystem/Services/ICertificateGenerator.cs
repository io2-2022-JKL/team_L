using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VaccinationSystem.Services
{
    public interface ICertificateGenerator
    {
        public Task<string> Generate(string patientName, DateTime dateOfBirth, string pesel, string vcName, string vcAddress, string vaccine, int dose, string batch, DateTime vaccDose);
    }
}
