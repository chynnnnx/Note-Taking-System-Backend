using MediatR;
using FirstAngular.Application.Common.Results;
using Microsoft.AspNetCore.Identity;
using FirstAngular.Domain.Entities;
using FirstAngular.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirstAngular.Application.Features.Auth.DTOs;

namespace FirstAngular.Application.Features.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public LoginCommandHandler(UserManager<AppIdentityUser> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<LoginResponse>> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(command.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, command.Password))
                return Result<LoginResponse>.Fail("Invalid email or password");

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.GenerateJwtToken(user.Id, user.Email, roles);

            var refreshToken = _tokenService.GenerateRefreshToken();
            var newRefresh = new RefreshToken
            {
                TokenHash = _tokenService.HashToken(refreshToken),
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(7)
            };

            await _unitOfWork.RefreshTokenRepository.AddAsync(newRefresh);
            await _unitOfWork.SaveChangesAsync();

            return Result<LoginResponse>.Ok(
             new LoginResponse(
                 token,
                 refreshToken,
                 DateTime.UtcNow.AddHours(2),
                 roles.FirstOrDefault() ?? string.Empty
             )
         );

        }
    }
}
