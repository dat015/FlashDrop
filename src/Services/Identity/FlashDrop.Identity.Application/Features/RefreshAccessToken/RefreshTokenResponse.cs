using System;
using System.Collections.Generic;
using System.Text;

namespace FlashDrop.Identity.Application.Features.RefreshToken
{
    public record RefreshTokenResponse(
        string AccessToken,
        string RefreshToken);

}
