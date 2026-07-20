using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Domain.Entities;
using Shared.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Identity.Application.Features.Users.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public DeleteUserHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }

            // Execute all domain operations across tables
            await _refreshTokenRepository.DeleteAllByUserIdAsync(user.Id, cancellationToken);
            _userRepository.Delete(user);

            // Call SaveChangesAsync exactly once at the end to commit both operations
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new DeleteUserResponse { Id = user.Id };
        }
    }
}
