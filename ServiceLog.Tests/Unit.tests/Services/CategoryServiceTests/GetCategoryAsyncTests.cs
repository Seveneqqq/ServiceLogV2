using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Repositories;
using ServiceLog.Services;

namespace ServiceLog.Tests.Unit.tests.Services.CategoryServiceTests
{
    public class GetCategoryAsyncTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CategoryService _categoryService;

        public GetCategoryAsyncTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Category_By_Id_Successfully()
        {
            //Arrange
            string categoryId = "1234";

            Category newCategory = new Category
            {
                Id = "1234",
                Name = "Test",
                Description = "Test123",
                ServiceOptions = new List<ServiceOption>
                {
                   new ServiceOption
                   {
                        Name = "Option1",
                        Note = "Option2",
                        Description = "Option3"
                   }
                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.GetCategoryByIdAsync(It.Is<string>(id => id == "1234")))
                .ReturnsAsync(newCategory);

            //Act

            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            //Assert

            Assert.True(result.Success);
            Assert.Equal("Category retrieved successfully.", result.Message);
            Assert.NotNull(result.Category);
        }


        [Fact]
        public async Task GetCategoryByIdAsync_Should_Return_Failed_When_Category_Not_Found()
        {
            //Arrange
            string categoryId = "4321";

            Category newCategory = new Category
            {
                Id = "1234",
                Name = "Test",
                Description = "Test123",
                ServiceOptions = new List<ServiceOption>
                {
                   new ServiceOption
                   {
                        Name = "Option1",
                        Note = "Option2",
                        Description = "Option3"
                   }
                }
            };

            _categoryRepositoryMock
                .Setup(repo => repo.GetCategoryByIdAsync(It.Is<string>(id => id == "1234")))
                .ReturnsAsync(newCategory);

            //Act

            var result = await _categoryService.GetCategoryByIdAsync(categoryId);

            //Assert

            Assert.False(result.Success);
            Assert.Equal("Category not found.", result.Message);
            Assert.Null(result.Category);
        }
    }
}

