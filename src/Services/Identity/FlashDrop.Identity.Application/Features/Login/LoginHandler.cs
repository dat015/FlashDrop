using FlashDrop.Identity.Application.Abstractions.Authentication;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Application.Abstractions.Security;
using FlashDrop.Identity.Domain.Entities;
using MediatR;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenHasher _refreshTokenHasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _passwordHasher;
        public LoginHandler(
            IUserRepository userRepository,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenHasher refreshTokenHasher,
            IRefreshTokenRepository refreshTokenRepository,
            IPasswordHasher passwordHasher
            )
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenHasher = refreshTokenHasher;
            _refreshTokenRepository = refreshTokenRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<LoginResponse> Handle(
            LoginCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(
                request.Email,
                cancellationToken);

            if (user is null)
            {
                throw new NotFoundException("User not found.");

            }

            if (!_passwordHasher.Verify(
                    request.Password,
                    user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");

            }

            if (!user.IsActive)
            {
                throw new ForbiddenException("User is inactive.");

            }

            var accessToken = _jwtTokenGenerator.Generate(user);

            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();

            var refreshTokenEntity = new Domain.Entities.RefreshToken(
                _refreshTokenHasher.Hash(refreshToken),
                user.Id,
                DateTimeOffset.UtcNow.AddDays(7));

            await _refreshTokenRepository.AddAsync(
                refreshTokenEntity,
                cancellationToken);

            await _userRepository.SaveChangesAsync(cancellationToken);

            return new LoginResponse(
                accessToken,
                refreshToken);
        }
    }
}
