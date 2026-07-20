using MediatR;
using System;

namespace FlashDrop.Identity.Application.Features.Users.ChangePassword
{
    public class ChangePasswordCommand : IRequest<ChangePasswordResponse>
    {
        public Guid Id { get; set; }
        public string NewPassword { get; set; } = null!;
    }
}
