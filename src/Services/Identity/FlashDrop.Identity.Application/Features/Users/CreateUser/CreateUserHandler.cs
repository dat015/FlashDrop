using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Application.Abstractions.Security;
using FlashDrop.Identity.Domain.Entities;
using Shared.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Identity.Application.Features.Users.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new BadRequestException("Email is already in use.");
            }

            var passwordHash = _passwordHasher.Hash(request.Password);
            var user = new User(request.Email, passwordHash, request.FullName);
            user.ChangeRole(request.Role);

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new CreateUserResponse { Id = user.Id };
        }
    }
}
