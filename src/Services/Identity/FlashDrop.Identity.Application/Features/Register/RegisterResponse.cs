using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Register
{
    public sealed record RegisterResponse(
        Guid UserId,
        string AccessToken,
        string RefreshToken
    );
}
