using MediatR;
using System;

namespace FlashDrop.Identity.Application.Features.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public Guid Id { get; set; }
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
