using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using FirstAngular.Application.Features.Auth.Commands;
using FirstAngular.Application.Common.Results;
using FirstAngular.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace FirstAngular.Application.UnitTests.Auth
{
    public class RegisterCommandHandlerTest
    {
        private readonly Mock<UserManager<AppIdentityUser>> _mockUserManager;
        private readonly Mock<RoleManager<IdentityRole>> _mockRoleManager;

        public RegisterCommandHandlerTest()
        {
            var userStore = new Mock<IUserStore<AppIdentityUser>>();
            _mockUserManager = new Mock<UserManager<AppIdentityUser>>(
                userStore.Object, null, null, null, null, null, null, null, null);

            var roleStore = new Mock<IRoleStore<IdentityRole>>();
            _mockRoleManager = new Mock<RoleManager<IdentityRole>>(
                roleStore.Object, null, null, null, null);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenUserIsRegistered()
        {
             var command = new RegisterCommand
            {
                Email = "test@example.com",
                Password = "P@ssw0rd",
                FirstName = "d",
                MiddleInitial = "D",
                LastName = "d"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync((AppIdentityUser)null);

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppIdentityUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Success);

            _mockUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<AppIdentityUser>(), "User"))
                            .ReturnsAsync(IdentityResult.Success);

            var handler = new RegisterCommandHandler(_mockUserManager.Object);

             var result = await handler.Handle(command, CancellationToken.None);

             Assert.True(result.Success);
         }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenEmailAlreadyExists()
        {
             var command = new RegisterCommand
            {
                Email = "test@example.com",
                Password = "P@ssw0rd"
            };

             var existingUser = AppIdentityUser.Create("Test", null, "User", command.Email);
            existingUser.Id = "user1";

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync(existingUser);

            var handler = new RegisterCommandHandler(_mockUserManager.Object);
 
            var result = await handler.Handle(command, CancellationToken.None);

             Assert.False(result.Success);
            Assert.Equal("Email is already registered.", result.Error);
        }


        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenUserCreationFails()
        {
             var command = new RegisterCommand
            {
                Email = "test@example.com",
                Password = "P@ssw0rd"
            };

            _mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                            .ReturnsAsync((AppIdentityUser)null);

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<AppIdentityUser>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Creation error" }));

            var handler = new RegisterCommandHandler(_mockUserManager.Object);

      
            var result = await handler.Handle(command, CancellationToken.None);

             Assert.False(result.Success);
            Assert.Equal("Creation error", result.Error);
        }
    }
}
