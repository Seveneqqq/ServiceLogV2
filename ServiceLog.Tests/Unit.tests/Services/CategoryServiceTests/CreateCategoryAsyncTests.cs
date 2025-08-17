using FluentValidation;
using Moq;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Domain.Validation;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories.CategoryRepository;
using ServiceLog.Services;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Tests.Unit.tests.Services.CategoryServiceTests
{
    public class CreateCategoryAsyncTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly ICategoryService _categoryService;
        private readonly IValidator<Category> _categoryValidator;

        public CreateCategoryAsyncTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryValidator = new CategoryValidator();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _categoryValidator);
        }

        [Fact]
        public async Task CreateCategoryAsync_Should_Return_Success_When_Category_Is_Valid()
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
                ServiceOptions = new List<ServiceOption>
                {

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

        [Fact]
        public async Task CreateCategoryAsync_Should_ReturnFailed_When_Name_Is_Empty()
        {
            // Arrange

            NewCategoryRequestDto newCategoryRequestDto = new NewCategoryRequestDto
            {
                Description = "Test Description",
                ServiceOptions = new List<ServiceOption>
                {
                    new ServiceOption{
                        Name = "Option 1",
                        Description = "Option 1 Description",
                        Note = "Option 1 Note"
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
            Assert.False(result.Success);
            Assert.Equal("Category name cannot be empty.", result.Message);

        }


    }
}
