using IdentityServer4.Services;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using VaccinationSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Backend_Tests
{
    [TestClass]
    public class BaseTest
    {
        /*private readonly Mock<IProfileService> _profileServiceMock = new();

        private HttpClient _httpClient = null!;
        [TestMethod]

        public async Task InitializeAsync()
        {
            var hostBuilder = Program.CreateHostBuilder(new string[0]) // jak nie jak tak???
                .ConfigureWebHost(webHostBuilder =>
                {
                    webHostBuilder.UseTestServer();
                })
                .ConfigureServices((_, services) =>
                {
                    services.AddSingleton(_profileServiceMock.Object);
                });

            var host = await hostBuilder.StartAsync();
            _httpClient = host.GetTestClient();
        }

        public Task DisposeAsync()
        {
            return Task.CompletedTask;
        }

        public void TestMethod1()
        {
            Login adminLogin = new Login { mail = "superadmin@mail.com", password = "superadmin!23" };
            
        }*/
    }
}
