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

            var result = await controller.MakeAppointment(patientID, timeSlotID, vaccineID);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task MakeAppointmentReturnsForbidden()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();

            mockDB.Setup(dB => dB.MakeAppointment(patientID, timeSlotID, vaccineID)).ThrowsAsync(new ArgumentException());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.MakeAppointment(patientID, timeSlotID, vaccineID);

            var result = Assert.IsType<ObjectResult>(timeSlots);
            Assert.Equal(403, result.StatusCode);
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
        public async Task MakeAppointmentReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.MakeAppointment(patientID, timeSlotID, vaccineID)).ReturnsAsync(()=>false);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.MakeAppointment(patientID, timeSlotID, vaccineID);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(timeSlots);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task GetTimeSlotsReturnsTimeSlots()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var response = GetFilterTimeSlotResponse();
            if (response == null)
                throw new ArgumentException();

            mockDB.Setup(dB => dB.GetTimeSlotsWithFiltration(It.IsAny<TimeSlotsFilter>())).ReturnsAsync(()=>response);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var slots = await controller.GetTimeSlots(city: "Warszawa",virus: "Coronavirus",dateFrom: "12-12-2022", dateTo: "22-12-2022");

            var okResult = Assert.IsType<OkObjectResult>(slots);


            var returnValue = Assert.IsType<FilterTimeSlotsControllerResponse>(okResult.Value);
            Assert.Equal(1, returnValue.data.Count);

        }

        [Fact]
        public async Task GetTimeSlotsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlotsWithFiltration(It.IsAny<TimeSlotsFilter>())).ReturnsAsync(new List<FilterTimeSlotResponse>());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var slots = await controller.GetTimeSlots("Warszawa", "Coronavirus", "12-12-2022", "22-12-2022");

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(slots);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task GetTimeSlotsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlotsWithFiltration(It.IsAny<TimeSlotsFilter>())).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);

            var slots = await controller.GetTimeSlots("Warszawa", "Coronavirus", "12-12-2022", "22-12-2022");

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(slots);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }
        

        private List<FilterTimeSlotResponse> GetFilterTimeSlotResponse()
        {
            var tS = new FilterTimeSlotResponse()
            {
                availableVaccines = new List<SimplifiedVaccine>(),
                doctorLastName = "BBB",
                doctorFirstName = "AAA",
                from = "12-12-2000",
                to = "12-12-3000",
                id = new Guid(),
                openingHours = new List<OpeningHoursDays>(),
                vaccinationCenterCity = "Warszawa",
                vaccinationCenterName = "ABC",
                vaccinationCenterStreet = "askskaks"
       
            };

            var list = new List<FilterTimeSlotResponse>();
            list.Add(tS);

            return list;
        }

    }
}
