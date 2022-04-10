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

            var result= await controller.AddVaccinationCenter(center);

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

    }
}
