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
        private Guid appointmentID = new Guid("33E18E13-8F45-4766-A0C0-08DA13EF5847");
        private string url = "jakistamurl";
        private string batch = "batch01234";
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
                windowBegin = "2022-01-29T08:00",
                windowEnd = "2022-01-29T09:00",
                timeSlotDurationInMinutes = 15
            };
        }

        private EditedTimeSlot GetEditedTimeSlot()
        {
            return new EditedTimeSlot()
            {
                timeFrom = "2022-01-29T08:00",
                timeTo = "2022-01-29T09:00"
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
        [Fact]
        public async Task CertifyReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var mockCertGen = new Mock<ICertificateGenerator>();

            mockDB.Setup(dB => dB.CreateCertificate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(() => true);
            mockDB.Setup(db => db.GetAppointment(appointmentID)).ReturnsAsync(GetAppointment);
            mockDB.Setup(db => db.GetDoctor(doctorID)).ReturnsAsync(GetDoctor);
            mockCertGen.Setup(gen => gen.Generate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => "https://abc.com");
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object, mockCertGen.Object);


            var result = await controller.Certify(doctorID, appointmentID);

            var okResult = Assert.IsType<OkObjectResult>(result);

        }

        [Fact]
        public async Task CertifyReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var mockCertGen = new Mock<ICertificateGenerator>();

            mockDB.Setup(dB => dB.CreateCertificate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(() => false);
            mockDB.Setup(db => db.GetAppointment(appointmentID)).ReturnsAsync(GetAppointment);
            mockDB.Setup(db => db.GetDoctor(doctorID)).ReturnsAsync(GetDoctor);
            mockCertGen.Setup(gen => gen.Generate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => "https://abc.com");
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object, mockCertGen.Object);


            var result = await controller.Certify(doctorID, appointmentID);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Data not found", notFoundResult.Value);
        }

        [Fact]
        public async Task CertifyReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var mockCertGen = new Mock<ICertificateGenerator>();

            mockDB.Setup(dB => dB.CreateCertificate(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<string>()))
                .ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            mockDB.Setup(db => db.GetAppointment(appointmentID)).ReturnsAsync(GetAppointment);
            mockDB.Setup(db => db.GetDoctor(doctorID)).ReturnsAsync(GetDoctor);
            mockCertGen.Setup(gen => gen.Generate(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(() => "https://abc.com");
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object, mockCertGen.Object);


            var result = await controller.Certify(doctorID, appointmentID);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
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
        private Appointment GetAppointment()
        {
            return new Appointment()
            {
                id = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE"),
                whichDose = 1,
                timeSlot = It.IsAny<TimeSlot>(),
                patient = new Patient
                {
                    id = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE"),
                    pesel = "82121211111",
                    dateOfBirth = new DateTime(1982, 12, 12),
                    firstName = "Jan",
                    lastName = "Nowak",
                    mail = "j.nowak@mail.com",
                    phoneNumber = "+48555221331",
                    password = "password123()",
                    active = true
                },
                vaccine = new Vaccine
                {
                    id = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE"),
                    company = "Moderna",
                    name = "Moderna vaccine",
                    numberOfDoses = 2,
                    minDaysBetweenDoses = 30,
                    minPatientAge = 18,
                    maxPatientAge = 99,
                    virus = Virus.Coronavirus,
                    active = true
                },
                state = AppointmentState.Finished,
                vaccineBatchNumber = "AB-123-nie-wiem",
                certifyState = CertificateState.NotLast
            };
        }
        private Doctor GetDoctor()
        {
            return new Doctor
            {
                doctorId = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE"),
                patientAccount = It.IsAny<Patient>(),
                vaccinationCenter = new VaccinationCenter
                {
                    id = new Guid("255E18E1-8FF7-4766-A0C0-08DA13EF87AE"),
                    name = "Punkt Szczepień Populacyjnych",
                    city = "Warszawa",
                    address = "Żwirki i Wigury 95/97",
                    active = true
                },
                active = true
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
        [Fact]
        public async Task GetDoctorIncomingAppointmentsReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorIncomingAppointments(doctorID)).ReturnsAsync(GetDoctorIncomingAppointments);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetDoctorIncomingAppointments(doctorID);
            var okResult = Assert.IsType<OkObjectResult>(incApps);
            var returnValue = Assert.IsType<List<DoctorIncomingAppResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }
        [Fact]
        public async Task GetDoctorIncomingAppointmentsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorIncomingAppointments(doctorID)).ReturnsAsync(new List<DoctorIncomingAppResponse>());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetDoctorIncomingAppointments(doctorID);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(incApps);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task GetDoctorIncomingAppointmentsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorIncomingAppointments(doctorID)).
                                    ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var incApps = await controller.GetDoctorIncomingAppointments(doctorID);
            var badResult = Assert.IsType<BadRequestObjectResult>(incApps);
            Assert.Equal("Something went wrong", badResult.Value.ToString());
        }
        private List<DoctorIncomingAppResponse> GetDoctorIncomingAppointments()
        {
            var incApps = new List<DoctorIncomingAppResponse>();
            incApps.Add(
                new DoctorIncomingAppResponse()
                {
                    vaccineName = "Pfeizer vaccine",
                    vaccineCompany = "Pfeizer",
                    vaccineVirus = "Coronavirus",
                    whichVaccineDose = 1,
                    appointmentId = new Guid("12b6f4f2-24f5-4c65-0746-08da26242468"),
                    from = "24-04-2022 12:30",
                    to = "24-04-2022 12:45",
                    patientFirstName = "Janina",
                    patientLastName = "Nowakowa"
                });
            return incApps;
        }
        [Fact]
        public async Task GetDoctorFormerAppointmentsReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorFormerAppointments(doctorID)).ReturnsAsync(GetDoctorFormerAppointments);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetDoctorFormerAppointments(doctorID);
            var okResult = Assert.IsType<OkObjectResult>(formApps);
            var returnValue = Assert.IsType<List<DoctorFormerAppResponse>>(okResult.Value);
            Assert.Single(returnValue);
        }
        [Fact]
        public async Task GetDoctorFormerAppointmentsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorFormerAppointments(doctorID)).ReturnsAsync(new List<DoctorFormerAppResponse>());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetDoctorFormerAppointments(doctorID);
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(formApps);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task GetDoctorFormerAppointmentsReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctorFormerAppointments(doctorID)).
                                    ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var formApps = await controller.GetDoctorFormerAppointments(doctorID);
            var badResult = Assert.IsType<BadRequestObjectResult>(formApps);
            Assert.Equal("Something went wrong", badResult.Value.ToString());

        }
        private List<DoctorFormerAppResponse> GetDoctorFormerAppointments()
        {
            var formApps = new List<DoctorFormerAppResponse>();
            formApps.Add(
                new DoctorFormerAppResponse()
                {
                    vaccineName = "Pfeizer vaccine",
                    vaccineCompany = "Pfeizer",
                    vaccineVirus = "Coronavirus",
                    whichVaccineDose = 2,
                    appointmentId = new Guid("c3382e67-9640-4239-0748-08da26242468"),
                    from = "20-03-2022 09:15",
                    to = "20-03-2022 09:30",
                    patientFirstName = "Robert",
                    patientLastName = "Weide",
                    state = "Finished",
                    PESEL = "59062011333",
                    batchNumber = "AB-123-nie-wiem"
                });
            return formApps;
        }

        [Fact]
        public async Task ConfirmVaccinationOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.UpdateVaccinationCount(doctorID, appointmentID)).ReturnsAsync(() => true);
            mockDB.Setup(dB => dB.UpdateBatchInAppointment(doctorID, appointmentID, batch)).ReturnsAsync(() => true);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var result = await controller.ConfirmVaccination(doctorID, appointmentID, batch);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task ConfirmVaccinationForbiddenOnUpdateVCount()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.UpdateVaccinationCount(doctorID, appointmentID)).ThrowsAsync(new ArgumentException());
            mockDB.Setup(dB => dB.UpdateBatchInAppointment(doctorID, appointmentID, batch)).ReturnsAsync(() => true);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var confirm = await controller.ConfirmVaccination(doctorID, appointmentID, batch);
            var result = Assert.IsType<ObjectResult>(confirm);
            Assert.Equal(403, result.StatusCode);
        }
        [Fact]
        public async Task ConfirmVaccinationForbiddenOnUpdateBatch()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.UpdateVaccinationCount(doctorID, appointmentID)).ReturnsAsync(() => true);
            mockDB.Setup(dB => dB.UpdateBatchInAppointment(doctorID, appointmentID, batch)).ThrowsAsync(new ArgumentException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var confirm = await controller.ConfirmVaccination(doctorID, appointmentID, batch);
            var result = Assert.IsType<ObjectResult>(confirm);
            Assert.Equal(403, result.StatusCode);
        }

        [Fact]
        public async Task ConfirmVaccinationExceptionOnUpdateVCount()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.UpdateVaccinationCount(doctorID, appointmentID)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            mockDB.Setup(dB => dB.UpdateBatchInAppointment(doctorID, appointmentID, batch)).ReturnsAsync(() => true);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var confirm = await controller.ConfirmVaccination(doctorID, appointmentID, batch);
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(confirm);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task ConfirmVaccinationExceptionOnUpdateBatch()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.UpdateVaccinationCount(doctorID, appointmentID)).ReturnsAsync(() => true);
            mockDB.Setup(dB => dB.UpdateBatchInAppointment(doctorID, appointmentID, batch)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);

            var confirm = await controller.ConfirmVaccination(doctorID, appointmentID, batch);
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(confirm);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task ConfirmVaccinationBadRequest()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.ConfirmVaccination(doctorID, appointmentID, batch);
            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task StartVaccinationReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetStartedAppointmentInfo(doctorID, appointmentID)).ReturnsAsync(GetStartedAppInfo);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var info = await controller.StartVaccination(doctorID,appointmentID);
            var okResult = Assert.IsType<OkObjectResult>(info);
            var returnValue = Assert.IsType<StartVaccinationResponse>(okResult.Value);
        }
        [Fact]
        public async Task StartVaccinationReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            StartVaccinationResponse response = null;
            mockDB.Setup(dB => dB.GetStartedAppointmentInfo(doctorID, appointmentID)).ReturnsAsync(response);
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var info = await controller.StartVaccination(doctorID, appointmentID);
            var notFound = Assert.IsType<NotFoundObjectResult>(info);
            Assert.Equal("Data not found", notFound.Value.ToString());
        }
        [Fact]
        public async Task StartVaccinationReturnsBadRequest()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetStartedAppointmentInfo(doctorID, appointmentID))
                .ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var info = await controller.StartVaccination(doctorID, appointmentID);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(info);
            Assert.Equal("Something went wrong", badRequestResult.Value.ToString());
        }
        [Fact]
        public async Task StartVaccinationReturnsForbidden()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetStartedAppointmentInfo(doctorID, appointmentID))
                .ThrowsAsync(new ArgumentException());
            var controller = new DoctorController(mockSignIn.Object, mockDB.Object);
            var info = await controller.StartVaccination(doctorID, appointmentID);
            var forbiddentResult = Assert.IsType<ObjectResult>(info);
            Assert.Equal(403, forbiddentResult.StatusCode);
        }
        private StartVaccinationResponse GetStartedAppInfo()
        {
            return new StartVaccinationResponse()
            {
                  vaccineName= "Pfeizer vaccine",
                  vaccineCompany= "Pfeizer",
                  numberOfDoses= 2,
                  minDaysBetweenDoses= 30,
                  maxDaysBetweenDoses= 100,
                  virusName= "Coronavirus",
                  minPatientAge= 12,
                  maxPatientAge= 80,
                  patientFirstName= "Janina",
                  patientLastName= "Nowakowa",
                  PESEL= "92120211122",
                  dateOfBirth= "02-00-1992",
                  from= "25-05-2022 12:45",
                  to= "25-05-2022 13:00"
            };
        }
    }
}
