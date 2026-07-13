using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Logout
{
    public record LogoutCommand(
        string RefreshToken
    ) : IRequest;
}
