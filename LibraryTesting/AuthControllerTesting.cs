using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using CA02_ASP.NET_Core.Controllers;
using CA02_ASP.NET_Core.Data.Services;
using CA02_ASP.NET_Core.Data.DTO;

namespace LibraryTesting
{
    [TestFixture]
    public class AuthControllerTests
    {
        private Mock<ILoginService> _loginServiceMock;
        private AuthController _authController;

        [SetUp]
        public void Setup()
        {
            // Initialize mock service and controller
            _loginServiceMock = new Mock<ILoginService>();
            _authController = new AuthController(_loginServiceMock.Object);
        }




        [Test]
        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange: Invalid credentials setup
            var invalidLoginDTO = new CA02_ASP.NET_Core.Data.Services.LoginDTO { Email = "johndoe@example.com", Password = "11111" };
            _loginServiceMock.Setup(service => service.Login(invalidLoginDTO.Email, invalidLoginDTO.Password))
                             .Returns((string)null);  // Simulate failed login

            // Act: Call the Login method of the controller
            var result = _authController.Login(invalidLoginDTO);

            // Assert: Check that the result is Unauthorized
            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
            var unauthorizedResult = result as UnauthorizedObjectResult;
            Assert.That(unauthorizedResult?.Value, Is.EqualTo("Invalid login attempt"));
        }

        [Test]
        public void Login_ShouldReturnOk_WithToken_WhenCredentialsAreValid()
        {
            // Arrange: Valid credentials setup
            var validLoginDTO = new CA02_ASP.NET_Core.Data.Services.LoginDTO { Email = "johndoe@example.com", Password = "11111" };
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJqb2huZG9lQGV4YW1wbGUuY29tIiwiZXhwIjoxNzMyOTUyNDMyLCJpc3MiOiJDQTAyX0FTUC5ORVRfQ29yZSIsImF1ZCI6IkNBMDJfQVNQLk5FVF9Db3JlX0F1ZGllbmNlIn0.TwbUQjVu7M-9UNp8_1i4WKleLcFq0SICflnj6zARjdk";
            var fakeToken = new { Token = token };
            _loginServiceMock.Setup(service => service.Login(validLoginDTO.Email, validLoginDTO.Password))
                             .Returns(token);  // Simulate successful login with token

            // Act: Call the Login method of the controller
            var result = _authController.Login(validLoginDTO);

            // Assert: Check that the result is Ok and contains a token
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            var okResult = result as OkObjectResult;
            Assert.That(okResult?.Value, Is.Not.Null);
            var response = okResult?.Value as dynamic;
            
            Assert.That(response, Is.EqualTo(response));  // Check that the token is returned
        }
    }
}
