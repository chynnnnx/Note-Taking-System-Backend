using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FirstAngular.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<string>>
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterCommandHandler(UserManager<AppIdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result<string>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userManager.FindByEmailAsync(command.Email);
            if (existingUser != null)
                return Result<string>.Fail("Email is already registered.");

            var user = new AppIdentityUser
            {
                UserName = command.Email,
                Email = command.Email,
                FirstName = command.FirstName,
                MiddleInitial = command.MiddleInitial,
                LastName = command.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
                return Result<string>.Fail(string.Join(", ", result.Errors.Select(e => e.Description)));

             await _userManager.AddToRoleAsync(user, "User");

            return Result<string>.Ok("Registered successfully");
        }

    }
}
