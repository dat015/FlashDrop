using FlashDrop.Identity.Application.Abstractions.Authentication;
using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Application.Abstractions.Security;
using FlashDrop.Identity.Domain.Entities;
using MediatR;
using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IRefreshTokenHasher _refreshTokenHasher;

        public RegisterHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator,
            IRefreshTokenRepository refreshTokenRepository,
            IRefreshTokenHasher refreshTokenHasher
            )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
            _refreshTokenHasher = refreshTokenHasher;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken);
            if (existingUser)
            {
                throw new ConflictException("User with this email already exists.");
            }

            var hashedPassword = _passwordHasher.Hash(request.Password);
            var user = new User
            (
                request.Email,
                hashedPassword,
                request.FullName
            );

            await _userRepository.AddAsync(user, cancellationToken);

            var token = _jwtTokenGenerator.Generate(user);
            var refreshToken = await CreateRefreshTokenAsync(user, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
            return new RegisterResponse
            (
                user.Id,
                token,
                refreshToken
            );
        }

        private async Task<string> CreateRefreshTokenAsync(User user, CancellationToken cancellationToken)
        {
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            var refreshTokenEntity = new Domain.Entities.RefreshToken
            (
               _refreshTokenHasher.Hash(refreshToken),
                user.Id,
                DateTimeOffset.UtcNow.AddDays(7)
            );

            await _refreshTokenRepository.AddAsync(refreshTokenEntity, cancellationToken);

            return refreshToken;
        }
    }
}
