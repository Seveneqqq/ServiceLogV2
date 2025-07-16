using Microsoft.AspNetCore.Identity;
using Moq;
using ServiceLog.Services.interfaces;
using ServiceLog.Services;
using ServiceLog.Models.Dto;

namespace ServiceLog.Tests.Unit.tests.Services.AuthServiceTests
{
    public class RegisterAsyncTests
    {

        static Mock<UserManager<IdentityUser>> GetMockUserManager()
        {
            var store = new Mock<IUserStore<IdentityUser>>();
            return new Mock<UserManager<IdentityUser>>(
                store.Object,
                null, // IOptions<IdentityOptions>
                null, // IPasswordHasher<IdentityUser>
                new List<IUserValidator<IdentityUser>>(),
                new List<IPasswordValidator<IdentityUser>>(),
                null, // ILookupNormalizer
                null, // IdentityErrorDescriber
                null, // IServiceProvider
                null  // ILogger<UserManager<IdentityUser>>
            );
        }

        static void mockUserManagerAndTokenServiceSetup(Mock<UserManager<IdentityUser>> mockUserManager, Mock<ITokenService> mockTokenService)
        {
            mockUserManager.Setup(m => m.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync((IdentityUser)null);

            mockUserManager.Setup(m => m.FindByEmailAsync(It.IsAny<string>()))
                           .ReturnsAsync((IdentityUser)null);

            mockUserManager.Setup(m => m.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success);

            mockUserManager.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                           .ReturnsAsync(IdentityResult.Success);

            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<IdentityUser>(), It.IsAny<string>()))
                            .Returns("mock-token");
        }

        [Fact]
        public async Task Should_Register_User_With_Email_Successfully()
        {
            //Arrange
            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);
            
            //Act
            RegisterDto registerDto = new RegisterDto() { 
                Email = "test123@admin123.com",
                Password = "Test1234",
                RepeatPassword = "Test1234",
                Role = "Client",
            };

            mockUserManagerAndTokenServiceSetup(mockUserManager, mockTokenService);

            
            var result = await authService.RegisterAsync(registerDto);

            //Assert
            Assert.True(result.Success);
            Assert.Equal("User registered successfully !", result.Message);
        }

        [Fact]
        public async Task Should_Register_User_With_Username_Successfully()
        {
            //Arrange
            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);
            
            //Act
            RegisterDto registerDto = new RegisterDto()
            {
               Username = "test123",
                Password = "Test1234",
                RepeatPassword = "Test1234",
                Role = "Client",
            };

            mockUserManagerAndTokenServiceSetup(mockUserManager, mockTokenService);


            var result = await authService.RegisterAsync(registerDto);

            //Assert
            Assert.True(result.Success);
            Assert.Equal("User registered successfully !", result.Message);

        }

        [Fact]
        public async Task Should_Not_Register_User_With_Existing_Username_Successfully()
        {
            //Arrange
            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);

            //Act
            RegisterDto registerDto = new RegisterDto()
            {
                Username = "test1234",
                Password = "Test1234",
                RepeatPassword = "Test1234",
                Role = "Client",
            };

            mockUserManager.Setup(m => m.FindByNameAsync("test1234"))
                           .ReturnsAsync(new IdentityUser { UserName = "test1234" });

            var result = await authService.RegisterAsync(registerDto);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("A user with this username or email already exists.", result.Message);
            
        }

        [Fact]
        public async Task Should_Not_Register_User_With_Existing_Email_Successfully()
        {
            //Arrange
            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);

            //Act
            RegisterDto registerDto = new RegisterDto()
            {
                Email = "test1234@test1234.pl",
                Password = "Test1234",
                RepeatPassword = "Test1234",
                Role = "Client",
            };

            mockUserManager.Setup(m => m.FindByEmailAsync("test1234@test1234.pl"))
                           .ReturnsAsync(new IdentityUser { UserName = "test1234@test1234.pl" });

            var result = await authService.RegisterAsync(registerDto);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("A user with this username or email already exists.", result.Message);

        }

        [Fact]
        public async Task Should_Returns_Empty_Username_Or_Email_Message() {

            //Arrange

            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);

            //Act

            mockUserManagerAndTokenServiceSetup(mockUserManager, mockTokenService);

            RegisterDto registerDto = new RegisterDto()
            {
                Password = "Test1234",
                RepeatPassword = "Test1234",
                Role = "Client"
            };

            var result = await authService.RegisterAsync(registerDto);

            //Assert

            Assert.False(result.Success);
            Assert.Equal("Username or email cannot be empty.", result.Message);

        }

        [Fact]
        public async Task Should_Returns_Different_Passwords_Message()
        {

            //Arrange

            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);

            //Act

            RegisterDto registerDto = new RegisterDto()
            {
                Username = "Test1234",
                Password = "Test1234",
                RepeatPassword = "Test12345",
                Role = "Client"
            };

            var result = await authService.RegisterAsync(registerDto);

            //Assert

            Assert.False(result.Success);
            Assert.Equal("Passwords do not match.", result.Message);

        }

        [Fact]
        public async Task Should_Returns_Error_While_Request_is_empty()
        {

            //Arrange

            var mockUserManager = GetMockUserManager();
            var mockTokenService = new Mock<ITokenService>();
            var authService = new AuthService(mockUserManager.Object, mockTokenService.Object);

            //Act

            RegisterDto registerDto = new RegisterDto()
            {
                
            };

            var result = await authService.RegisterAsync(registerDto);

            //Assert

            Assert.False(result.Success);
            Assert.Contains("Username or email cannot be empty.", result.Message);

        }

    }
}
