using MediatR;
using FirstAngular.Application.DTOs;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.RefreshTokens.Commands
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppIdentityUser> _userManager;

        public RefreshTokenCommandHandler( IUnitOfWork unitOfWork, ITokenService tokenService, UserManager<AppIdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<LoginResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var hashedToken = _tokenService.HashToken(command.RefreshToken);

             var existingToken = await _unitOfWork.RefreshTokenRepository
                .FirstOrDefaultAsync(t => t.TokenHash == hashedToken
                    && !t.IsRevoked
                    && t.Expiration > DateTime.UtcNow);

            if (existingToken == null)
                throw new Exception("Invalid or expired refresh token");

             var user = await _userManager.FindByIdAsync(existingToken.UserId);
            if (user == null)
                throw new Exception("User not found");

             var roles = await _userManager.GetRolesAsync(user);
            var newToken = _tokenService.GenerateJwtToken(user.Id, user.Email, roles);
            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

             existingToken.IsRevoked = true;
            _unitOfWork.RefreshTokenRepository.Update(existingToken);

             var newRefreshToken = new RefreshToken
            {
                TokenHash = _tokenService.HashToken(newRefreshTokenValue),
                UserId = user.Id,
                Expiration = DateTime.UtcNow.AddDays(7)
            };
            await _unitOfWork.RefreshTokenRepository.AddAsync(newRefreshToken);

            await _unitOfWork.SaveChangesAsync();

            return new LoginResponse
            {
                Token = newToken,
                RefreshToken = newRefreshTokenValue,
                Expiration = DateTime.UtcNow.AddHours(2)
            };
        }
    }
}
