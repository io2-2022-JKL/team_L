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
    public class DoctorControllerTest
    {
        private Guid timeSlotID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        private Guid timeSlotID2 = new Guid("55A2BBCE-E031-4931-E751-08DA13EF87A5");
        private Guid doctorID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        [Fact]
        public async Task GetTimeSlotsReturnsCenters()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlots(doctorID)).ReturnsAsync(GetTimeSlots);
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var slots = await controller.GetTimeSlots(doctorID);


            var okResult = Assert.IsType<OkObjectResult>(slots);


            var returnValue = Assert.IsType<List<TimeSlotsResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetTimeSlotsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlots(doctorID)).ReturnsAsync(new List<TimeSlotsResponse>());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.GetTimeSlots(doctorID);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(timeSlots);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task GetTimeSlotsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlots(doctorID)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.GetTimeSlots(doctorID);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(timeSlots);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task CreateTimeSlotsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slots = GetCreateNewVisitRequest();
            mockDB.Setup(dB => dB.CreateTimeSlots(doctorID,slots));
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var result = await controller.CreateTimeSlots(doctorID, slots);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task CreateTimeSlotsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slots = GetCreateNewVisitRequest();
            mockDB.Setup(dB => dB.CreateTimeSlots(doctorID, slots)).ThrowsAsync(new ArgumentException());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.CreateTimeSlots(doctorID, slots);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(timeSlots);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task CreateTimeSlotsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slots = GetCreateNewVisitRequest();
            mockDB.Setup(dB => dB.CreateTimeSlots(doctorID, slots)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.CreateTimeSlots(doctorID, slots);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(timeSlots);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task CreateTimeSLotsReturnsBadRequestInvalidModel()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slots = GetCreateNewVisitRequest();
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.CreateTimeSlots(doctorID, slots);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task EditTimeSlotOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot));
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var result = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task EditTimeSlotReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot)).ThrowsAsync(new ArgumentException());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(timeSlots);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task EditTimeSlotReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(timeSlots);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task EditTimeSotReturnsBadRequestInvalidModel()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            var controller = new DoctorsController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            Assert.IsType<BadRequestObjectResult>(result);
        }


        private List<TimeSlotsResponse> GetTimeSlots()
        {
            var timeSlots = new List<TimeSlotsResponse>()
            {
                new TimeSlotsResponse()
                {
                    Id = timeSlotID,
                    From = "2022-01-29T08:00", 
                    To = "2022-01-29T09:00"
                },
                new TimeSlotsResponse()
                {
                    Id = timeSlotID2,
                    From = "2022-01-29T09:00",
                    To = "2022-01-29T10:00"
                }
            };

            return timeSlots;
        }

        private CreateNewVisitRequest GetCreateNewVisitRequest()
        {
            return new CreateNewVisitRequest()
            {
                From = DateTime.Parse("2022-01-29T08:00"),
                To = DateTime.Parse("2022-01-29T09:00"),
                TimeSlotDurationInMinutes = 15
            };
        }

        private EditedTimeSlot GetEditedTimeSlot()
        {
            return new EditedTimeSlot()
            {
                From = DateTime.Parse("2022-01-29T08:00"),
                To = DateTime.Parse("2022-01-29T09:00")
            };
        }
    }
}
