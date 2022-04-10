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
    public class AdminControllerTests
    {

        private Guid vCenterId = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f");
        private Guid patientID = new Guid("55A2BBCE-E031-4931-E751-08DA13EF87A5");
        [Fact]
        public async Task ShowVaccinationCentersReturnsCenters()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetVaccinationCenters()).ReturnsAsync(GetCenters);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var centers = await controller.ShowVaccinationCenters();


            var okResult = Assert.IsType<OkObjectResult>(centers);


            var returnValue = Assert.IsType<List<VaccinationCenterResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task ShowVaccinationCentersReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetVaccinationCenters()).ReturnsAsync(new List<VaccinationCenterResponse>());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var centers = await controller.ShowVaccinationCenters();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(centers);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task ShowVaccinationCentersReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetVaccinationCenters()).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var centers = await controller.ShowVaccinationCenters();

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(centers);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task AddVaccinationCentersReturnsBadRequestInvalidModel()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.AddVaccinationCenter(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task AddVaccinationCentersReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetAddingVaccinationCenter();

            mockDB.Setup(dB => dB.AddVaccinationCenter(center)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.AddVaccinationCenter(center);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task AddVaccinationCentersReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetAddingVaccinationCenter();
            mockDB.Setup(dB => dB.AddVaccinationCenter(center));
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.AddVaccinationCenter(center);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditVaccinationCentersReturnsBadRequestInvalidModel()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.EditVaccinationCenter(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }
        [Fact]
        public async Task EditVaccinationCentersReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetEditingVaccinationCenter();

            mockDB.Setup(dB => dB.EditVaccinationCenter(center)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditVaccinationCenter(center);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task EditVaccinationCentersReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetEditingVaccinationCenter();
            mockDB.Setup(dB => dB.EditVaccinationCenter(center)).ReturnsAsync(true);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditVaccinationCenter(center);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task EditVaccinationCentersReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetEditingVaccinationCenter();
            mockDB.Setup(dB => dB.EditVaccinationCenter(center)).ReturnsAsync(false);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditVaccinationCenter(center);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task DeleteVaccinationCentersReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();

            mockDB.Setup(dB => dB.DeleteVaccinationCenter(vCenterId)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeleteVaccinationCenter(vCenterId);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

        [Fact]
        public async Task DeleteVaccinationCentersReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetEditingVaccinationCenter();
            mockDB.Setup(dB => dB.DeleteVaccinationCenter(vCenterId)).ReturnsAsync(true);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeleteVaccinationCenter(vCenterId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task DeleteVaccinationCentersReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var center = GetEditingVaccinationCenter();
            mockDB.Setup(dB => dB.DeleteVaccinationCenter(vCenterId)).ReturnsAsync(false);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeleteVaccinationCenter(vCenterId);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        private List<VaccinationCenterResponse> GetCenters()
        {

            var centers = new List<VaccinationCenterResponse>();

            centers.Add(new VaccinationCenterResponse()
            {
                Id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                Name = "Punkt Szczepień Populacyjnych",
                City = "Warszawa",
                Street = "Żwirki i Wigury 95/97",
                Active = true,
                OpeningHoursDays = new[]
                 {
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "10:00",
                        }

                    },
                Vaccines = new List<Vaccine>()
                    {
                        new Vaccine()
                        {
                             id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                            company = "Pfeizer",
                            name = "Pfeizer vaccine",
                            numberOfDoses = 2,
                            minDaysBetweenDoses = 30,
                            minPatientAge = 12,
                            virus = Virus.Coronavirus,
                            active = true
                        },
                       new Vaccine()
                        {
                             id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                            company = "Moderna",
                            name = "Moderna vaccine",
                            numberOfDoses = 2,
                            minDaysBetweenDoses = 30,
                            minPatientAge = 18,
                            maxPatientAge = 99,
                            virus = Virus.Coronavirus,
                            active = true
                        },
                    }

            });
            centers.Add(new VaccinationCenterResponse()
            {
                Id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                Name = "Apteczny Punkt Szczepień",
                City = "Warszawa",
                Street = "Mokotowska 27/Lok.1 i 4",
                OpeningHoursDays = new[]
                 {
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "10:00",
                        }

                    },
                Vaccines = new List<Vaccine>()
                    {
                        new Vaccine()
                        {
                            id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                            company = "Pfeizer",
                            name = "Pfeizer vaccine",
                            numberOfDoses = 2,
                            minDaysBetweenDoses = 30,
                            minPatientAge = 12,
                            virus = Virus.Coronavirus,
                            active = true
                        },
                       new Vaccine()
                        {
                            id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                            company = "Moderna",
                            name = "Moderna vaccine",
                            numberOfDoses = 2,
                            minDaysBetweenDoses = 30,
                            minPatientAge = 18,
                            maxPatientAge = 99,
                            virus = Virus.Coronavirus,
                            active = true
                        },
                    }

            });


            return centers;
        }

        private AddVaccinationCenterRequest GetAddingVaccinationCenter()
        {
            return new AddVaccinationCenterRequest
            {
                Name = "Punkt Szczepień Populacyjnych",
                City = "Warszawa",
                Street = "Żwirki i Wigury 95/97",
                Active = true,
                openingHoursDays = new[]
                 {
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "10:00",
                        }

                    },
                VaccineIds = new List<Guid>()
                    {
                        new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                        new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f")

                    }
            };
        }
        private EditedVaccinationCenter GetEditingVaccinationCenter()
        {
            return new EditedVaccinationCenter
            {
                Id = new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                Name = "Punkt Szczepień Populacyjnych",
                City = "Warszawa",
                Street = "Żwirki i Wigury 95/97",
                Active = true,
                openingHoursDays = new[]
                 {
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "13:00",
                        },
                        new OpeningHoursDays()
                        {
                            from = "8:00",
                            to = "10:00",
                        }

                    },
                VaccineIds = new List<Guid>()
                    {
                        new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f"),
                                new Guid("1c8ddbb7-06c8-44ec-893e-f936607aa36f")

                    }
            };
        }

        private EditedPatient GetEditedPatient()
        {
            return new EditedPatient
            {
                id = patientID,
                firstName = "Jan",
                lastName = "Nowakowy",
                dateOfBirth = new DateTime(1982, 12, 12),
                pesel = "82121211111",
                mail = "j.nowak@zmienionymail.com",
                phoneNumber = "+48555221331",
                active = true
            };
        }

        [Fact]
        public async Task EditPatientReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var patient = GetEditedPatient();
            mockDB.Setup(dB => dB.EditPatient(patient)).ReturnsAsync(true);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditPatient(patient);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task EditPatientReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var patient = GetEditedPatient();
            mockDB.Setup(dB => dB.EditPatient(patient)).ReturnsAsync(false);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditPatient(patient);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task EditPatientReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var patient = GetEditedPatient();

            mockDB.Setup(dB => dB.EditPatient(patient)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.EditPatient(patient);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task EditPatientReturnsBadRequestInvalidModel()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);
            controller.ModelState.AddModelError("id", "Bad format");

            var result = await controller.EditPatient(null);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task DeletePatientReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.DeletePatient(patientID)).ReturnsAsync(true);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeletePatient(patientID);

            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task DeletePatientReturnsBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();

            mockDB.Setup(dB => dB.DeletePatient(patientID)).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeletePatient(patientID);

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }
        [Fact]
        public async Task DeletePatientReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.DeletePatient(patientID)).ReturnsAsync(false);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var result = await controller.DeletePatient(patientID);

            Assert.IsType<NotFoundObjectResult>(result);
        }
        [Fact]
        public async Task GetDoctorsReturnsCenters()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctors()).ReturnsAsync(GetDoctors);
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var doctors = await controller.GetDoctors();

            var okResult = Assert.IsType<OkObjectResult>(doctors);

            var returnValue = Assert.IsType<List<DoctorResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetDoctorsReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetDoctors()).ReturnsAsync(new List<DoctorResponse>());
            var controller = new AdminController(mockSignIn.Object, mockDB.Object);

            var doctors = await controller.GetDoctors();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(doctors);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }
        private List<DoctorResponse> GetDoctors()
        {
            var doctors = new List<DoctorResponse>();
            doctors.Add(new DoctorResponse()
            {
                id = new Guid("98A1B9A6-0E4E-46C7-443A-08DA1B08BAE6"),
                pesel = "59062011333",
                firstName = "Robert",
                lastName = "Weide",
                dateOfBirth = new DateTime(1959, 06, 20),
                mail = "robert.b.weide@mail.com",
                phoneNumber = "+48125200331",
                active = true,
                vaccinationCenterId = new Guid("99766467-2246-4DE2-FF97-08DA1B08BA99"),
                name = "Punkt Szczepień Populacyjnych",
                city = "Warszawa",
                street = "Żwirki i Wigury 95/97",
            });
            doctors.Add(new DoctorResponse()
            {
                pesel = "74011011111",
                dateOfBirth = new DateTime(1974, 01, 10),
                firstName = "Monika",
                lastName = "Kowalska",
                mail = "m.kowalska@mail.com",
                phoneNumber = "+48349824991",
                vaccinationCenterId = new Guid("B8FA9079-6FD0-4759-FF98-08DA1B08BA99"),
                name = "Apteczny Punkt Szczepień",
                city = "Warszawa",
                street = "Mokotowska 27/Lok.1 i 4",
            });
            return doctors;
        }
    }
}
