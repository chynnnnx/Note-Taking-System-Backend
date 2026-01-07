using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FirstAngular.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<AppIdentityUser>>
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public RegisterCommandHandler(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result<AppIdentityUser>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(command.Email);
            if (existingUser != null)
                return Result<AppIdentityUser>.Fail("Email is already registered.");

            var user = AppIdentityUser.Create(command.FirstName, command.MiddleInitial, command.LastName, command.Email);

            var createResult = await _userManager.CreateAsync(user, command.Password);
            if (!createResult.Succeeded)
            {
                var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                return Result<AppIdentityUser>.Fail(errors);
            }

            await _userManager.AddToRoleAsync(user, "User");

             return Result<AppIdentityUser>.Ok(user);
        }
    }
}
