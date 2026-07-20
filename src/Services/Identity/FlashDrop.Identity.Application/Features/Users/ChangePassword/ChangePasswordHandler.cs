using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Application.Abstractions.Security;
using FlashDrop.Identity.Domain.Entities;
using Shared.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Identity.Application.Features.Users.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public ChangePasswordHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User {request.Id} not found");
            }

            var passwordHash = _passwordHasher.Hash(request.NewPassword);
            user.ChangePassword(passwordHash);

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new ChangePasswordResponse { Id = user.Id };
        }
    }
}
