using FlashDrop.Identity.Application.Abstractions.Persistence;
using FlashDrop.Identity.Domain.Entities;
using Shared.Exceptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FlashDrop.Identity.Application.Features.Users.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User {request.Id} not found");
            }

            user.UpdateProfile(request.FullName);
            user.ChangeRole(request.Role);
            
            if (request.IsActive)
                user.Activate();
            else
                user.Deactivate();

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new UpdateUserResponse { Id = user.Id };
        }
    }
}
