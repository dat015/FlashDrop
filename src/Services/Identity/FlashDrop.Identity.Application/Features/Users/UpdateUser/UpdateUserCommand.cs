using MediatR;
using FlashDrop.Identity.Api.Enums;
using System;

namespace FlashDrop.Identity.Application.Features.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
    }
}
