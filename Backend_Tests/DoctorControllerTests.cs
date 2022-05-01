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
        private Guid timeSlotID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        private Guid timeSlotID2 = new Guid("55A2BBCE-E031-4931-E751-08DA13EF87A5");
        private Guid doctorID = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE");
        [Fact]
        public async Task GetTimeSlotsReturnsTImeSlots()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetTimeSlots(doctorID)).ReturnsAsync(GetTimeSlots);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            mockDB.Setup(dB => dB.CreateTimeSlots(doctorID, slots));
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
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
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot)).ReturnsAsync(() => true);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var result = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task EditTimeSlotReturnsForbidden()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot)).ThrowsAsync(new ArgumentException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var timeSlots = await controller.ModifyTimeSlot(doctorID, timeSlotID, slot);

            var result = Assert.IsType<ObjectResult>(timeSlots);
            Assert.Equal(403, result.StatusCode);
        }


        [Fact]
        public async Task EditTimeSlotReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var slot = GetEditedTimeSlot();
            mockDB.Setup(dB => dB.EditTimeSlot(doctorID, timeSlotID, slot)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

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
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
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
                    id = timeSlotID,
                    from = "2022-01-29T08:00",
                    to = "2022-01-29T09:00"
                },
                new TimeSlotsResponse()
                {
                    id = timeSlotID2,
                    from = "2022-01-29T09:00",
                    to = "2022-01-29T10:00"
                }
            };

            return timeSlots;
        }

        private CreateNewVisitRequest GetCreateNewVisitRequest()
        {
            return new CreateNewVisitRequest()
            {
                from = DateTime.Parse("2022-01-29T08:00"),
                to = DateTime.Parse("2022-01-29T09:00"),
                timeSlotDurationInMinutes = 15
            };
        }

        private EditedTimeSlot GetEditedTimeSlot()
        {
            return new EditedTimeSlot()
            {
                from = DateTime.Parse("2022-01-29T08:00"),
                to = DateTime.Parse("2022-01-29T09:00")
            };
        }
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
                dateOfBirth = "1982-12-12",
                firstName = "Jan",
                lastName = "Nowak",
                mail = "j.nowak@mail.com",
                phoneNumber = "+48555221331",
                active = true,
            };
        }

        [Fact]
        public async Task GetDoctorInfoReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorInfo(doctorID)).ReturnsAsync(GetDoctorInfo);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var doctorInfo = await controller.GetDoctorInfo(doctorID);
            var okResult = Assert.IsType<OkObjectResult>(doctorInfo);
            var returnValue = Assert.IsType<DoctorInfoResponse>(okResult.Value);
            Assert.Equal(returnValue.patientAccountId, GetDoctorInfo().patientAccountId);
        }
        [Fact]
        public async Task GetDoctorInfoReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            DoctorInfoResponse nullDoctorInfo = null;
            mockDB.Setup(dB => dB.GetDoctorInfo(doctorID)).ReturnsAsync(nullDoctorInfo);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var doctorInfo = await controller.GetDoctorInfo(doctorID);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(doctorInfo);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task GetDoctorInfoReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorInfo(doctorID))
                    .ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var patientInfo = await controller.GetDoctorInfo(doctorID);
            var badResult = Assert.IsType<BadRequestObjectResult>(patientInfo);
            Assert.Equal("Something went wrong", badResult.Value.ToString());
        }
        private DoctorInfoResponse GetDoctorInfo()
        {
            return new DoctorInfoResponse()
            {
                vaccinationCenterId = new Guid("0D96A825-F68A-44CA-ADD8-08DA262423E7"),
                vaccinationCenterName = "Punkt Szczepień Populacyjnych",
                vaccinationCenterCity = "Warszawa",
                vaccinationCenterStreet = "Żwirki i Wigury 95/97",
                patientAccountId = new Guid("F70AA1AD-7E62-44CE-EAE5-08DA26242413"),
            };
        }

    }
}
