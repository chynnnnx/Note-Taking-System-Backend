using FirstAngular.Application.Features.Auth.Commands;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FirstAngular.Application.UnitTests.Auth
{
    public class LoginCommandHandlerTest
    {
        private readonly Mock<UserManager<AppIdentityUser>> _mockUserManager;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBaseRepository<RefreshToken>> _mockRefreshTokenRepo;

        public LoginCommandHandlerTest()
        {
             var userStoreMock = new Mock<IUserStore<AppIdentityUser>>();
            _mockUserManager = new Mock<UserManager<AppIdentityUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);

             _mockTokenService = new Mock<ITokenService>();

             _mockRefreshTokenRepo = new Mock<IBaseRepository<RefreshToken>>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockUnitOfWork.Setup(u => u.RefreshTokenRepository).Returns(_mockRefreshTokenRepo.Object);
            _mockRefreshTokenRepo.Setup(r => r.AddAsync(It.IsAny<RefreshToken>())).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
        }

        [Fact]
        public async Task Login_ShouldReturnSuccess_WhenCredentialsAreValid()
        {
            var command = new LoginCommand("test@example.com", "P@ssw0rd");

            var user = AppIdentityUser.Create("Test", null, "User", command.Email);
            user.Id = "user1";  

            _mockUserManager.Setup(u => u.FindByEmailAsync(command.Email)).ReturnsAsync(user);
            _mockUserManager.Setup(u => u.CheckPasswordAsync(user, command.Password)).ReturnsAsync(true);
            _mockUserManager.Setup(u => u.GetRolesAsync(user)).ReturnsAsync(new List<string> { "User" });

            _mockTokenService.Setup(t => t.GenerateJwtToken(user.Id, user.Email, It.IsAny<IList<string>>()))
                             .Returns("jwt-token");
            _mockTokenService.Setup(t => t.GenerateRefreshToken()).Returns("refresh-token");
            _mockTokenService.Setup(t => t.HashToken("refresh-token")).Returns("hashed-refresh-token");

            var handler = new LoginCommandHandler(
                _mockUserManager.Object,
                _mockTokenService.Object,
                _mockUnitOfWork.Object);
 
            var result = await handler.Handle(command, CancellationToken.None);
             
            Assert.True(result.Success);
            Assert.Equal("jwt-token", result.Data.Token);
            Assert.Equal("refresh-token", result.Data.RefreshToken);

            _mockRefreshTokenRepo.Verify(r => r.AddAsync(It.IsAny<RefreshToken>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

    }
}
