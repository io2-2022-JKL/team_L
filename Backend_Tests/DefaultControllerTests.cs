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
    public class DefaultControllerTests
    {
        [Fact]
        public async Task GetCitiesReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetCities()).ReturnsAsync(GetCities);
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var cities = await controller.GetCities();

            var okResult = Assert.IsType<OkObjectResult>(cities);

            var returnValue = Assert.IsType<List<CityResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetCitiesReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetCities()).ReturnsAsync(new List<CityResponse>());
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var cities = await controller.GetCities();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(cities);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task GetCitiesBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetCities()).ThrowsAsync(new System.Data.DeletedRowInaccessibleException());
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var cities = await controller.GetCities();

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(cities);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }

                [Fact]
        public async Task GetVirusesReturnsOk()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetViruses()).Returns(GetViruses);
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var viruses = await controller.GetViruses();


            var okResult = Assert.IsType<OkObjectResult>(viruses);


            var returnValue = Assert.IsType<List<VirusResponse>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetVirusesReturnsNotFound()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetViruses()).Returns(new List<VirusResponse>());
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var viruses = await controller.GetViruses();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(viruses);
            Assert.Equal("Data not found", notFoundResult.Value.ToString());
        }


        [Fact]
        public async Task GetVirusesBadRequestDatabaseException()
        {
            var mockDB = new Mock<IDatabase>();
            var mockSignIn = new Mock<IUserSignInManager>();
            mockDB.Setup(dB => dB.GetViruses()).Throws(new System.Data.DeletedRowInaccessibleException());
            var controller = new DefaultController(mockSignIn.Object, mockDB.Object);

            var viruses = await controller.GetViruses();

            var notFoundResult = Assert.IsType<BadRequestObjectResult>(viruses);
            Assert.Equal("Something went wrong", notFoundResult.Value.ToString());
        }


        private List<CityResponse> GetCities()
        {
            return new List<CityResponse>()
            {
                new CityResponse(){city = "Warszawa"},
                new CityResponse(){city = "Krakow"}
            };
        }

        private List<VirusResponse> GetViruses()
        {
            return new List<VirusResponse>()
            {
                new VirusResponse(){virus = "virus1"},
                new VirusResponse(){virus = "virus2"}
            };
        }
    }
}
