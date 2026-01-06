using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Categories.Commands.CreateCategory;
using FirstAngular.Application.Features.Categories.DTOs;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstAngular.Application.UnitTests.Category
{
    public class CreateCategoryCommandTest
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsCategoryDTO()
        {
             var command = new CreateCategoryCommand { Name = "Work" };

            var mockCategoryRepo = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.CategoryRepository).Returns(mockCategoryRepo.Object);
            mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(u => u.UserId).Returns("user-123");

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCategoryCommand, CategoryEntity>()
                   .ForMember(dest => dest.Id, opt => opt.Ignore())
                   .ForMember(dest => dest.UserId, opt => opt.Ignore());
                cfg.CreateMap<CategoryEntity, CategoryDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var handler = new CreateCategoryCommandHandler(mockUnitOfWork.Object, mapper, mockUserService.Object);

             var result = await handler.Handle(command, CancellationToken.None);

             Assert.True(result.Success);
            Assert.Equal("user-123", result.Data!.UserId);
            Assert.Equal(command.Name, result.Data.Name);

            mockCategoryRepo.Verify(r => r.AddAsync(It.Is<CategoryEntity>(c => c.Name == command.Name)), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
