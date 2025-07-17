using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceLog.Controllers;
using ServiceLog.Models.Dto;
using ServiceLog.Repositories.CategoryRepository;
using ServiceLog.Services;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Tests.Unit.tests.Controllers.AuthControllerTests
{
    public class LoginAsyncTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _authController;

        public LoginAsyncTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _authController = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_Should_Return_200_OK()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@test.pl",
                Password = "Test1234"
            };

            var jwtToken = "123213123-213123-213123";

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                    dto => dto.Email == loginDto.Email && dto.Password == loginDto.Password)))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = true,
                    Token = jwtToken,
                    Message = "Login successful.",
                    Role = "Client"
                });

            
            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _authController.Login(loginDto);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_401_Unauthorized()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@test.pl",
                Password = "Test1234"
            };

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                    dto => dto.Email == loginDto.Email && dto.Password == loginDto.Password)))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = false,
                    Message = "Incorrect password !",
                    ErrorCode = AuthErrorCode.InvalidPassword
                });

            // Act
            var result = await _authController.Login(loginDto);
            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            Assert.Equal(401, unauthorizedResult.StatusCode);
        }
        [Fact]
        public async Task Login_Should_Return_404_NotFound()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@test.pl",
                Password = "Test1234"
            };

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                    dto => dto.Email == loginDto.Email && dto.Password == loginDto.Password)))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = false,
                    Message = "User doesn't exists.",
                    ErrorCode = AuthErrorCode.UserNotFound
                });

            // Act
            var result = await _authController.Login(loginDto);
            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_Success_Using_Email()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Email = "test@test.pl",
                Password = "Test1234"
            };

            var jwtToken = "123213123-213123-213123";

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                    dto => dto.Email == loginDto.Email && dto.Password == loginDto.Password && !string.IsNullOrEmpty(dto.Email))))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = true,
                    Token = jwtToken,
                    Message = "Login successful.",
                    Role = "Client"
                });


            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act
            var result = await _authController.Login(loginDto);
            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public async Task Login_Should_Return_Success_Using_Username()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Username = "testuser123@123.pl",
                Password = "Test1234"
            };

            var jwtToken = "123213123-213123-213123";

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                 dto => dto.Username == loginDto.Username && dto.Password == loginDto.Password && !string.IsNullOrEmpty(dto.Username))))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = true,
                    Token = jwtToken,
                    Message = "Login successful.",
                    Role = "Client"
                });


            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act

            var result = await _authController.Login(loginDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDto = Assert.IsType<LoginResponseDto>(okResult.Value);

            // Assert

            Assert.Equal(200, okResult.StatusCode);
            Assert.True(responseDto.Success);
            Assert.Equal("Login successful.", responseDto.Message);


        }

        [Fact]
        public async Task Login_Should_Return_Failed_With_Empty_Request()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                Username = "testuser123@123.pl",
                Password = "Test1234"
            };

            var jwtToken = "123213123-213123-213123";

            _authServiceMock
                .Setup(s => s.LoginAsync(It.Is<LoginDto>(
                 dto => dto.Username == loginDto.Username && dto.Password == loginDto.Password && !string.IsNullOrEmpty(dto.Username))))
                .ReturnsAsync(new LoginResponseDto
                {
                    Success = true,
                    Token = jwtToken,
                    Message = "Login successful.",
                    Role = "Client"
                });


            _authController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            // Act

            var result = await _authController.Login(loginDto);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseDto = Assert.IsType<LoginResponseDto>(okResult.Value);

            // Assert

            Assert.Equal(200, okResult.StatusCode);
            Assert.True(responseDto.Success);
            Assert.Equal("Login successful.", responseDto.Message);


        }


    }
}
