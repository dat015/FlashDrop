using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Register
{
    public record RegisterCommand(
        string FullName,
        string Email,
        string Password
    ) : IRequest<RegisterResponse>;
}
