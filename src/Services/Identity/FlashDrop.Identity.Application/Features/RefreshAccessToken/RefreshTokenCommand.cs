using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResponse>;
    
}
