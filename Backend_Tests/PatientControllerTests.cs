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
            mockDB.Setup(dB => dB.MakeAppointment(patientID, timeSlotID, vaccineID)).ReturnsAsync(() => true);
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
            mockDB.Setup(dB => dB.MakeAppointment(patientID, timeSlotID, vaccineID)).ReturnsAsync(() => false);
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
            Assert.Single(returnValue.data);

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

        private List<CertificatesResponse> GetCertificates()
        {
            var certs = new List<CertificatesResponse>();
            certs.Add(new CertificatesResponse
            {
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
        [Fact]
        public async Task GetFormerAppointmentsReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetFormerAppointments(patientID)).ReturnsAsync(GetFormerAppointments);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetFormerAppointments(patientID);
            var okResult = Assert.IsType<OkObjectResult>(formApps);
            var returnValue = Assert.IsType<List<FormerAppointmentResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }
        [Fact]
        public async Task GetFormerAppointmentsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetFormerAppointments(patientID)).ReturnsAsync(new List<FormerAppointmentResponse>());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetFormerAppointments(patientID);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(formApps);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task GetFormerAppointmentsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetFormerAppointments(patientID)).
                                    ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetFormerAppointments(patientID);
            var badResult = Assert.IsType<BadRequestObjectResult>(formApps);
            Assert.Equal("Something went wrong", badResult.Value.ToString());

        }
        private List<FormerAppointmentResponse> GetFormerAppointments()
        {
            var formApps = new List<FormerAppointmentResponse>();
            formApps.Add(
                new FormerAppointmentResponse()
                {
                    vaccineName = "Pfeizer vaccine",
                    vaccineCompany = "Pfeizer",
                    vaccineVirus = "Coronavirus",
                    whichVaccineDose = 2,
                    appointmentId = new Guid("91b8d82a-75d7-4791-d2c8-08da1b08bb0d"),
                    windowBegin = "3/20/2022 9:15:00 AM",
                    windowEnd = "3/20/2022 9:30:00 AM",
                    vaccinationCenterName = "Apteczny Punkt Szczepien",
                    vaccinationCenterCity = "Warszawa",
                    vaccinationCenterStreet = "Mokotowska 27/Lok.1 i 4",
                    doctorFirstName = "Monika",
                    doctorLastName = "Kowalska",
                    visitState = "Finished",
                });
            return formApps;
        }
        [Fact]
        public async Task GetIncomingAppointmentsReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetIncomingAppointments(patientID)).ReturnsAsync(GetIncomingAppointments);
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetIncomingAppointments(patientID);
            var okResult = Assert.IsType<OkObjectResult>(incApps);
            var returnValue = Assert.IsType<List<IncomingAppointmentResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }
        [Fact]
        public async Task GetIncomingAppointmentsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetIncomingAppointments(patientID)).ReturnsAsync(new List<IncomingAppointmentResponse>());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetIncomingAppointments(patientID);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(incApps);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task GetIncomingAppointmentsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB=>dB.GetIncomingAppointments(patientID)).
                                    ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new PatientController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetIncomingAppointments(patientID);
            var badResult = Assert.IsType<BadRequestObjectResult>(incApps);
            Assert.Equal("Something went wrong", badResult.Value.ToString());

        }
        private List<IncomingAppointmentResponse> GetIncomingAppointments()
        {
            var incApps = new List<IncomingAppointmentResponse>();
            incApps.Add(
                new IncomingAppointmentResponse()
                {
                    vaccineName= "Pfeizer vaccine",
                    vaccineCompany= "Pfeizer",
                    vaccineVirus= "Coronavirus",
                    whichVaccineDose= 1,
                    appointmentId= new Guid("0a7f23ee-99e6-4358-d2c5-08da1b08bb0d"),
                    windowBegin= "4/25/2022 12:30:00 PM",
                    windowEnd= "4/25/2022 12:45:00 PM",
                    vaccinationCenterName = "Apteczny Punkt Szczepien",
                    vaccinationCenterCity= "Warszawa",
                    vaccinationCenterStreet= "Mokotowska 27/Lok.1 i 4",
                    doctorFirstName= "Monika",
                    doctorLastName= "Kowalska",
                });
            return incApps;
        }
    }
}