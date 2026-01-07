using AutoMapper;
using FirstAngular.Application.Common.Helpers;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.User.DTOs;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.User.Commands.UpdateUser
{
    public class UpdateUserCommandHandler: IRequestHandler<UpdateUserCommand, Result<UpdateUserDTO>>
    {
        private readonly UserManager <AppIdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateUserCommandHandler(UserManager<AppIdentityUser> userManager, IMapper mapper, ICurrentUserService currentUserService)
        {   
            _userManager = userManager;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<UpdateUserDTO>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_currentUserService.UserId);
            if (user == null) return Result<UpdateUserDTO>.Fail("User not found.");

            var hasChanges = user.UpdateProfile(command.FirstName, command.MiddleInitial, command.LastName);

            if (hasChanges)
            {
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return Result<UpdateUserDTO>.Fail($"Failed to update user: {errors}");
                }
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return Result<UpdateUserDTO>.Ok(new UpdateUserDTO(userDto, hasChanges));
        }

    }
}
