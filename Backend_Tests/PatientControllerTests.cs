using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using VaccinationSystem.Services;
using VaccinationSystem.Models;
using VaccinationSystem.DTOs;
using VaccinationSystem.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Tests
{
    public class PatientControllerTests
    {
        private Guid patientID = new Guid("11B34C8E-C680-49A9-E753-08DA13EF87A5");
        
        private List<CertificatesResponse> GetCertificates()
        {
            var certs = new List<CertificatesResponse>();
            certs.Add(new CertificatesResponse
            {
                id = new Guid("F35C3BF8-1AC3-487C-A5E7-08DA13EF87AA"),
                url = "placeholder",
                vaccineName = "Pfeizer vaccine",
                vaccineCompany = "Pfeizer",
                virusType = "Coronavirus"
            });
            return certs;
        }
        [Fact]
        public async Task GetCertificatesReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetCertificates(patientID)).ReturnsAsync(GetCertificates);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var certs = await controller.GetCertificates(patientID);

            var okResult = Assert.IsType<OkObjectResult>(certs);
            var returnValue = Assert.IsType<List<CertificatesResponse>>(okResult.Value);
            Assert.Single(returnValue); // zamiast Assert.Equal(1, returnValue.Count)
        }

        [Fact]
        public async Task GetCertificatesReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetCertificates(patientID)).ReturnsAsync(new List<CertificatesResponse>());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var certs = await controller.GetCertificates(patientID);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(certs);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
    }
}
