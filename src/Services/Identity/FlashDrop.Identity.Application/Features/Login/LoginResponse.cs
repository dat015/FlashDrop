using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.Login
{
    public sealed record LoginResponse(
        string AccessToken,
        string RefreshToken);
}
