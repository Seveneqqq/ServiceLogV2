using Moq;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories;
using ServiceLog.Services;

namespace ServiceLog.Tests.Unit.tests.Services
{
    public class CategoryServiceTest
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryService _categoryService;

        public CategoryServiceTest()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnSuccess_WhenCategoryIsValid()
        {
            // Arrange
            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                Name = "Test Category",
                Description = "Test Description",
                ServiceOptions = new List<ServiceOption> {
                    new ServiceOption{
                        Name = "Option 1",
                        Description = "Option 1 Description",
                        Note = "Option 1 Note",
                    }
                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);


            // Act

            var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);

            // Assert

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Category created successfully.", result.Message);

        }
        [Fact]
        public async Task CreateCategoryAsync_Should_ReturnSuccess_When_Note_Is_Null_In_ServiceOptions()
        {
            // Arrange
            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                Name = "Test Category",
                Description = "Test Description",
                ServiceOptions = new List<ServiceOption> {
                    new ServiceOption{
                        Name = "Option 1",
                        Description = "Option 1 Description",
                    }
                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);


            // Act

            var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);

            // Assert

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Category created successfully.", result.Message);

        }
        [Fact]
        public async Task CreateCategoryAsync_Should_ReturnSuccess_When_Many_ServiceOptions()
        {
            // Arrange
            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                Name = "Test Category",
                Description = "Test Description",
                ServiceOptions = new List<ServiceOption> {
                    new ServiceOption{
                        Name = "Option 1",
                        Description = "Option 1 Description",
                        Note = "Option 1 Note"
                    },
                    new ServiceOption{
                        Name = "Option 2",
                        Description = "Option 2 Description",
                        Note = "Option 2 Note"
                    }
                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);


            // Act

            var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);

            // Assert

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Category created successfully.", result.Message);

        }
        [Fact]
        public async Task CreateCategoryAsync_Should_ReturnFailed_When_ServiceOpions_Empty()
        {
            // Arrange
            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                Name = "Test Category",
                Description = "Test Description",
                ServiceOptions = new List<ServiceOption> {

                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);


            // Act

            var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Service options cannot be empty.", result.Message);

        }
        [Fact]
        public async Task CreateCategoryAsync_Should_ReturnFailed_When_Request_Empty()
        {
            // Arrange

            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                
            };

            _categoryRepositoryMock
                .Setup(repo => repo.CreateCategoryAsync(It.IsAny<Category>()))
                .Returns(Task.CompletedTask);


            // Act

            var result = await _categoryService.CreateCategoryAsync(newCategoryRequestDto);

            // Assert

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Equal("Category request cannot be empty.", result.Message);

        }

    }
}
