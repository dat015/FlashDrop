using FlashDrop.Identity.Application.Abstractions.Authentication;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using MediatR;
using Shared.Exceptions;

namespace FlashDrop.Identity.Application.Features.Logout
{
    public class LogoutHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokenHasher _refreshTokenHasher;
        private readonly ICurrentUserService _currentUserService;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        public LogoutHandler(
            IRefreshTokenHasher refreshTokenHasher,
            ICurrentUserService currentUserService,
            IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenHasher = refreshTokenHasher;
            _currentUserService = currentUserService;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(_refreshTokenHasher.Hash(request.RefreshToken), cancellationToken);
            if (refreshToken == null || refreshToken.UserId != _currentUserService.UserId) {
                throw new UnauthorizedException("Invalid or expired refresh token.");
            }
            refreshToken.Revoke();
            await _refreshTokenRepository.Update(refreshToken, cancellationToken);
            return;
        }
    }
}
