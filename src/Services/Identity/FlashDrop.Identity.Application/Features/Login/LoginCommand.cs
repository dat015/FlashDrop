using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Login
{
    public record LoginCommand(
        string Email,
        string Password) : IRequest<LoginResponse>;
}
