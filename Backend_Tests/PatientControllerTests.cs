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
    public class PatientControllerTests
    {
        private Guid timeSlotID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        private Guid patientID = new Guid("55A2BBCE-E031-4931-E751-08DA13EF87A5");
        private Guid vaccineID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        [Fact]
        public async Task MakeAppointmentReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.MakeAppointment(patientID,timeSlotID,vaccineID)).ReturnsAsync(() => true);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

        }

        [Fact]
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

        }


        [Fact]
        public async Task MakeAppointmentReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.MakeAppointment(patientID, timeSlotID, vaccineID)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.MakeAppointment(patientID, timeSlotID, vaccineID);

            var result = Assert.IsType<BadRequestObjectResult>(timeSlots);
            Assert.Equal("Something went wrong", result.Value.ToString());
        }
        [Fact]
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }

    }
}
