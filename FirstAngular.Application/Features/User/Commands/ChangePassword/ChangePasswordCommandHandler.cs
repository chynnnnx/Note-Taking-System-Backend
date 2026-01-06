using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.User.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler: IRequestHandler<ChangePasswordCommand, Result<bool>>
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ICurrentUserService _currentUserService;

        public ChangePasswordCommandHandler(UserManager<AppIdentityUser> userManager, ICurrentUserService currentUserService)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle (ChangePasswordCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrWhiteSpace(userId)) return Result<bool>.Fail("User not logged in.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Result<bool>.Fail("User not found.");

            var result = await _userManager.ChangePasswordAsync(user, command.CurrentPassword, command.NewPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                return Result<bool>.Fail(errors);
            }
            return Result<bool>.Ok(true);
        }
    }
}
