//using NUnit.Framework;
//using Moq;
//using Microsoft.AspNetCore.Mvc;
//using CA02_ASP.NET_Core.Controllers;
//using CA02_ASP.NET_Core.Data.Services;

//namespace CA02_ASP.NET_Core.Tests
//{
//    [TestFixture]
//    public class AuthControllerTests
//    {
//        private Mock<ILoginService> _loginServiceMock;
//        private AuthController _authController;

//        [SetUp]
//        public void Setup()
//        {
//             Initialize mock and controller
//            _loginServiceMock = new Mock<ILoginService>();
//            _authController = new AuthController(_loginServiceMock.Object);
//        }

//        [Test]
//        public void Login_ShouldReturnUnauthorized_WhenCredentialsAreInvalid()
//        {
//             Arrange
//            var invalidLoginDTO = new LoginDTO
//            {
//                Email = "johndoe@example.com",
//                Password = "12345"
//            };

//             Simulate failed login (null result)
//            _loginServiceMock.Setup(service => service.Login(invalidLoginDTO.Email, invalidLoginDTO.Password))
//                             .Returns((string)null);

//             Act
//            var result = _authController.Login(invalidLoginDTO);

//             Assert
//            Assert.That(result, Is.InstanceOf<UnauthorizedObjectResult>());
//            var unauthorizedResult = result as UnauthorizedObjectResult;
//            Assert.That(unauthorizedResult?.Value, Is.EqualTo("Invalid login attempt"));
//        }

//        [Test]
//        public void Login_ShouldReturnOk_WithToken_WhenCredentialsAreValid()
//        {
//             Arrange
//            var validLoginDTO = new LoginDTO
//            {
//                Email = "johndoe@example.com",
//                Password = "12345"
//            };

//            var fakeToken = "fakeToken";

//             Simulate successful login
//            _loginServiceMock.Setup(service => service.Login(validLoginDTO.Email, validLoginDTO.Password))
//                             .Returns(fakeToken);

//             Act
//            var result = _authController.Login(validLoginDTO);

//             Assert
//            Assert.That(result, Is.InstanceOf<OkObjectResult>());
//            var okResult = result as OkObjectResult;

//            Assert.That(okResult?.Value, Is.Not.Null);
//            var response = okResult?.Value as dynamic;

//            Assert.That(response?.Token, Is.EqualTo(fakeToken));
//        }
//    }
//}
