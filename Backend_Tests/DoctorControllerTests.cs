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
using VaccinationSystem.Contollers;

namespace Backend_Tests
{
    public class DoctorControllerTests
    {
        [Fact]
        public async Task GetPatientReturnsPatient()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();

            var patient = GetPatient();

            mockDB.Setup(dB => dB.GetPatient(patient.id)).ReturnsAsync(GetPatient);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var patientFromController = await controller.GetPatient(patient.id);
            var okResult = Assert.IsType<OkObjectResult>(patientFromController);

            var returnValue = Assert.IsType<PatientResponse>(okResult.Value);
            Assert.Equal(patient.id, returnValue.id);
        }

        [Fact]
        public async Task GetPatientReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();

            var patient = GetPatient();
            PatientResponse patientRNull = null;

            mockDB.Setup(dB => dB.GetPatient(patient.id)).ReturnsAsync(patientRNull);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var patientFromController = await controller.GetPatient(patient.id);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(patientFromController);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        private PatientResponse GetPatient()
        {
            return new PatientResponse()
            {
                id = new Guid("522A0EC0-1727-44C9-A308-08DA1B08BABF"),
                PESEL = "82121211111",
                dateOfBirth = new DateTime(1982, 12, 12),
                firstName = "Jan",
                lastName = "Nowak",
                mail = "j.nowak@mail.com",
                phoneNumber = "+48555221331",
                active = true,
            };
        }

    }
}
