using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ServiceLog.Enums;
using ServiceLog.Models.Domain;
using ServiceLog.Models.Dto.CategoryDto;
using ServiceLog.Models.Dto.ServiceHistoryDto;
using ServiceLog.Repositories.DeviceRepository;
using ServiceLog.Repositories.ServiceHistoryRepository;
using ServiceLog.Services;
using ServiceLog.Services.interfaces;

namespace ServiceLog.Tests.Unit.tests.Services.ServiceHistoryServiceTests
{
    public class CreateServiceHistoryAsyncTests
    {
        private readonly Mock<IServiceHistoryRepository> _ServiceHistoryRepositoryMock;
        private readonly IServiceHistoryService _ServiceHistoryService;
        private readonly Mock<ICategoryService> _categoryService;
        private readonly Mock<IDeviceRepository> _deviceRepository;

        public CreateServiceHistoryAsyncTests()
        {
            _ServiceHistoryRepositoryMock = new Mock<IServiceHistoryRepository>();
            _categoryService = new Mock<ICategoryService>();
            _deviceRepository = new Mock<IDeviceRepository>();
            _ServiceHistoryService = new ServiceHistoryService(_ServiceHistoryRepositoryMock.Object, _categoryService.Object, _deviceRepository.Object);
        }

        [Fact]
        public async Task CreateServiceHistoryAsync_Should_Succesfully_Create_ServiceHistory() {
            //arrange

            CreateServiceHistoryRequestDto request = new CreateServiceHistoryRequestDto
            {
                DeviceId = "BC495CEC-BAD1-402E-FAD7-08DDCBC8500B",
                IssueDescription = "Test issue",
                OtherInformations = "Test info",
                TechnicanId = "technician123",
                TicketId = "ticket123",
                PerformedServiceOptions = new List<ServiceOption>
                    {
                        new ServiceOption
                        {
                            Name = "Test Service Option",
                            Description = "Test Description"
                        }
                    }
            };

            _deviceRepository
                .Setup(repo => repo.GetDeviceByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new Device { Id=new Guid("BC495CEC-BAD1-402E-FAD7-08DDCBC8500B"), CategoryId = "412312-123123-123123", Designation="Template", Location="U62", SerialNumber="device123", Short_id="dev123", Status = "Active" });

            _categoryService
                .Setup(repo => repo.GetCategoryByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new GetByIdCategoryResponseDto { Success=true, ErrorCode=CategoryErrorCodes.CategoryErrorCode.None, Message="Success", Category = new Category
                {
                    Id = "412312-123123-123123",
                    Name = "Test Category",
                    Description = "Test Description",
                    ServiceOptions = new List<ServiceOption>
                    {
                            new ServiceOption
                            {
                                Name = "Test Service Option",
                                Description = "Test Description"
                            }
                        }
                    }
                });



            //act

            var result = await _ServiceHistoryService.CreateServiceHistoryAsync(request);

            //assert

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal("Service history created successfully.", result.Message);
        }

    }
}
