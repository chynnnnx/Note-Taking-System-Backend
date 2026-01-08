using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
using AutoMapper;
using FirstAngular.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using FirstAngular.Application.Features.User.DTOs;

namespace FirstAngular.Application.Features.User.Queries
{
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, Result<UserDTO>>
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetUserInfoQueryHandler(UserManager<AppIdentityUser> userManager, ICurrentUserService currentUserService, IMapper mapper)
        {
            _userManager = userManager;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<UserDTO>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return Result<UserDTO>.Fail("User not found.");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Result<UserDTO>.Fail("User not found.");

            var userDto = _mapper.Map<UserDTO>(user);
            return Result<UserDTO>.Ok(userDto);
        }
    }

}
