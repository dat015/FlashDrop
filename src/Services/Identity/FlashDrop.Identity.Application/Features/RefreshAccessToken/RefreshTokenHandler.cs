using FlashDrop.Identity.Application.Abstractions.Authentication;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Application.Features.RefreshToken;
using FlashDrop.Identity.Domain.Entities;
using MediatR;
using Shared.Exceptions;

namespace FlashDrop.Identity.Application.Features.RefreshAccessToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRefreshTokenHasher _refreshTokenHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RefreshTokenHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IRefreshTokenHasher refreshTokenHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenHasher = refreshTokenHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<RefreshTokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var existingRefreshToken = await _refreshTokenRepository.GetByTokenHashAsync(_refreshTokenHasher.Hash(request.RefreshToken), cancellationToken);
            if (existingRefreshToken is null)
            {
                throw new NotFoundException("Refresh Token not found.");
            }
            else if (!existingRefreshToken.IsActive)
            {
                throw new ForbiddenException("Refresh Token is inactive.");
            }
            else
            {
                var user = await _userRepository.GetByIdAsync(existingRefreshToken.UserId, cancellationToken);
                if (user is null)
                {
                    throw new NotFoundException("User not found.");
                }
                var newAccessToken = _jwtTokenGenerator.Generate(user);
                var newRefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
                
                existingRefreshToken.Revoke();
                await _refreshTokenRepository.Update(existingRefreshToken, cancellationToken);

                var newRefreshTokenEntity = new Domain.Entities.RefreshToken(
                    _refreshTokenHasher.Hash(newRefreshToken),
                    user.Id,          
                    DateTime.UtcNow.AddDays(7)
                    );
                await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);
                await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

                return new RefreshTokenResponse
                (
                    newAccessToken,
                    newRefreshToken
                );
            }
        }
    }
}
