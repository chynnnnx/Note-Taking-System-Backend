using FirstAngular.Application.Features.RefreshTokens.Commands;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.UnitTests.RefreshTokens
{
    public class RefreshTokenTest
    {
        private readonly Mock<UserManager<AppIdentityUser>> _mockUserManager;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IBaseRepository<RefreshToken>> _mockRefreshTokenRepo;

        public RefreshTokenTest()
        {
            var store = new Mock<IUserStore<AppIdentityUser>>();
            _mockUserManager = new Mock<UserManager<AppIdentityUser>>(store.Object, null, null, null, null, null, null, null, null);
            _mockTokenService = new Mock<ITokenService>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockRefreshTokenRepo = new Mock<IBaseRepository<RefreshToken>>();
            _mockUnitOfWork.Setup(u => u.RefreshTokenRepository).Returns(_mockRefreshTokenRepo.Object);
        }

        [Fact]
        public async Task RefreshToken_ShouldReturnNewTokens_WhenTokenIsValid()
        {
            var command = new RefreshTokenCommand("refresh-token");

            var hashedToken = "hashed-refresh-token";
            var user = AppIdentityUser.Create("Test", null, "User", "test@example.com");
            user.Id = "user1";

            var existingToken = new RefreshToken
            {
                TokenHash = hashedToken,
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddHours(1),
                IsRevoked = false
            };

            _mockTokenService.Setup(t => t.HashToken(command.RefreshToken))
                             .Returns(hashedToken);

            _mockRefreshTokenRepo
                .Setup(r => r.FirstOrDefaultAsync(It.IsAny<Expression<Func<RefreshToken, bool>>>()))
                .ReturnsAsync(existingToken);

            _mockUserManager.Setup(u => u.FindByIdAsync(user.Id))
                            .ReturnsAsync(user);

            _mockUserManager.Setup(u => u.GetRolesAsync(user))
                            .ReturnsAsync(new List<string> { "User" });

            _mockTokenService.Setup(t =>
                    t.GenerateJwtToken(user.Id, user.Email, It.IsAny<IList<string>>()))
                .Returns("new-jwt-token");

            _mockTokenService.Setup(t => t.GenerateRefreshToken())
                             .Returns("new-refresh-token");

            _mockTokenService.Setup(t => t.HashToken("new-refresh-token"))
                             .Returns("hashed-new-refresh-token");

            var handler = new RefreshTokenCommandHandler(
                _mockUnitOfWork.Object,
                _mockTokenService.Object,
                _mockUserManager.Object
            );

            var result = await handler.Handle(command, CancellationToken.None);

             Assert.Equal("new-refresh-token", result.Data.RefreshToken);
        }

    }
}
